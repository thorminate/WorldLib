using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WorldLib.SourceGen;

[Generator]
public sealed class LibraryGenerator : IIncrementalGenerator
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly DiagnosticDescriptor UndocumentedEntry = new(
        "WLIB001",
        "Undocumented library entry",
        "'{0}' in {1} has no entry in {2}",
        "Documentation",
        DiagnosticSeverity.Warning,
        true);

    private static readonly DiagnosticDescriptor MissingDocsFile = new(
        "WLIB002",
        "Missing documentation file",
        "No AdditionalFile found matching '{0}' for {1}",
        "Documentation",
        DiagnosticSeverity.Warning,
        true);

    private static readonly DiagnosticDescriptor OrphanedDocEntry = new(
        "WLIB003",
        "Orphaned documentation entry",
        "'{0}' in {1} has no matching constant in {2}",
        "Documentation",
        DiagnosticSeverity.Error,
        true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<AttributeData> attributes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "WorldLib.SourceGen.LibraryGenAttribute`2",
                (node, _) => node is CompilationUnitSyntax,
                (ctx, _) => ctx.Attributes.ToImmutableArray())
            .SelectMany((attrs, _) => attrs);

        IncrementalValuesProvider<AdditionalText> jsonFiles = context.AdditionalTextsProvider
            .Where(f => f.Path.EndsWith(".json"));

        context.RegisterSourceOutput(
            attributes.Combine(jsonFiles.Collect()),
            (ctx, pair) => Emit(ctx, pair.Left, pair.Right));
    }

    private static void Emit(
        SourceProductionContext ctx,
        AttributeData attr,
        ImmutableArray<AdditionalText> files)
    {
        ImmutableArray<ITypeSymbol>? typeArgs = attr.AttributeClass?.TypeArguments;
        if (typeArgs is not { Length: 2 }) return;

        string assetType = typeArgs.Value[1].ToDisplayString();

        Dictionary<string, TypedConstant> args = attr.NamedArguments
            .ToDictionary(k => k.Key, v => v.Value);

        if (!args.TryGetValue("IdClass", out var idClassVal)) return;
        if (!args.TryGetValue("Docs", out var docsVal)) return;
        if (!args.TryGetValue("Get", out var getVal)) return;

        args.TryGetValue("Set", out var setVal);
        args.TryGetValue("Strip", out var stripVal);
        args.TryGetValue("Namespace", out var nsVal);
        args.TryGetValue("Usings", out var usingsVal);
        args.TryGetValue("TargetClass", out var targetClassVal);
        args.TryGetValue("Static", out var staticVal);
        args.TryGetValue("StaticMembers", out var staticMembersVal);

        var idClass = (INamedTypeSymbol?)idClassVal.Value;
        string? docsPath = (string?)docsVal.Value;
        string? getExpr = (string?)getVal.Value;
        string? setExpr = (string?)setVal.Value;
        string? strip = (string?)stripVal.Value;
        string? targetClass = (string?)targetClassVal.Value;
        string ns = (string?)nsVal.Value ?? "WorldLib.Generated";
        bool isStatic = staticVal.Value is not bool b || b;
        bool isStaticMembers = staticMembersVal.Value is not bool b2 || b2;

        string[]? usings = usingsVal.Kind == TypedConstantKind.Array
            ? usingsVal.Values
                .Select(v => (string?)v.Value)
                .Where(v => v is not null)
                .Select(v => v!)
                .ToArray()
            : null;

        if (idClass is null || docsPath is null || getExpr is null) return;

        string docsFileName = Path.GetFileName(docsPath);
        var jsonFile = files.FirstOrDefault(f =>
            f.Path.EndsWith(docsPath.Replace('/', Path.DirectorySeparatorChar)));

        if (jsonFile is null)
            ctx.ReportDiagnostic(Diagnostic.Create(
                MissingDocsFile, Location.None, docsPath, idClass.Name));

        Dictionary<string, DocEntry> docs = LoadDocs(jsonFile?.GetText()?.ToString());
        docs.TryGetValue("_class", out var classDoc);

        List<IFieldSymbol> constants = idClass.GetMembers()
            .OfType<IFieldSymbol>()
            .Where(f => f.IsConst && f.Type.SpecialType == SpecialType.System_String)
            .ToList();

        HashSet<string> constantIds = new(
            constants.Select(f => ((string?)f.ConstantValue ?? f.Name).ToLower()));

        foreach (string docKey in docs.Keys.Where(docKey => docKey != "_class" && !constantIds.Contains(docKey)))
            ctx.ReportDiagnostic(Diagnostic.Create(
                OrphanedDocEntry, Location.None,
                docKey, docsFileName, idClass.Name));

        string generatedClassName;
        string generatedNamespace;

        if (targetClass is not null)
        {
            int lastDot = targetClass.LastIndexOf('.');
            generatedClassName = lastDot >= 0
                ? targetClass.Substring(lastDot + 1)
                : targetClass;
            generatedNamespace = lastDot >= 0
                ? targetClass.Substring(0, lastDot)
                : ns;
        }
        else
        {
            string rawClassName = idClass.Name.StartsWith("S_")
                ? idClass.Name.Substring(2)
                : idClass.Name;
            generatedClassName = ToPascalCase(rawClassName) + "Library";
            generatedNamespace = ns;
        }

        string idClassName = idClass.ToDisplayString();
        bool isPartial = targetClass is not null;

        var sb = new StringBuilder();

        sb.AppendLine("// <auto-generated/>");
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine("extern alias GameAsm;");
        sb.AppendLine("using System;");
        sb.AppendLine("using strings = GameAsm::strings;");

        if (usings is not null)
            foreach (string u in usings)
                sb.AppendLine($"using {u};");

        sb.AppendLine();
        sb.AppendLine($"namespace {generatedNamespace};");
        sb.AppendLine();

        EmitDocComment(sb, classDoc, docsFileName, "_class", classDoc is not null, true);

        string classModifiers = $"public{(isStatic ? " static" : "")}{(isPartial ? " partial" : " sealed")}";
        sb.AppendLine($"{classModifiers} class {generatedClassName}");
        sb.AppendLine("{");

        foreach (var field in constants)
        {
            string originalName = field.Name;
            string strippedName = strip is not null
                                  && originalName.StartsWith(strip, StringComparison.OrdinalIgnoreCase)
                ? originalName.Substring(strip.Length)
                : originalName;

            string rawId = ((string?)field.ConstantValue ?? strippedName).ToLower();
            bool hasDoc = docs.TryGetValue(rawId, out var doc);
            string propName = doc?.PropertyName is not null
                ? doc.PropertyName
                : ToPascalCase(strippedName);

            if (!hasDoc)
                ctx.ReportDiagnostic(Diagnostic.Create(
                    UndocumentedEntry, Location.None,
                    rawId, idClass.Name, docsFileName));

            sb.AppendLine();
            EmitDocComment(sb, doc, docsFileName, rawId, hasDoc, false);
            EmitProperty(sb, assetType, propName, idClassName, originalName, getExpr, setExpr, isStaticMembers);
        }

        sb.AppendLine("}");

        ctx.AddSource($"{generatedClassName}.g.cs", sb.ToString());
    }

    private static void EmitDocComment(
        StringBuilder sb,
        DocEntry? doc,
        string docsFileName,
        string rawId,
        bool hasDoc,
        bool isClass)
    {
        string indent = isClass ? "" : "    ";

        sb.AppendLine($"{indent}/// <summary>");
        sb.AppendLine(hasDoc
            ? $"{indent}///     {doc!.Summary}"
            : $"{indent}///     ⚠️ Undocumented. Add \"{rawId}\" to {docsFileName}.");
        sb.AppendLine($"{indent}/// </summary>");

        if (hasDoc && doc!.Remarks is { Length: > 0 })
        {
            sb.AppendLine($"{indent}/// <remarks>");
            foreach (string remark in doc.Remarks)
            {
                sb.AppendLine($"{indent}///     <para>");
                sb.AppendLine($"{indent}///         {remark}");
                sb.AppendLine($"{indent}///     </para>");
            }

            sb.AppendLine($"{indent}/// </remarks>");
        }

        if (hasDoc && doc!.Value is not null)
        {
            sb.AppendLine($"{indent}/// <value>");
            sb.AppendLine($"{indent}///     {doc.Value}");
            sb.AppendLine($"{indent}/// </value>");
        }

        if (hasDoc && doc!.Exceptions is { Length: > 0 })
            foreach (var ex in doc.Exceptions)
            {
                sb.AppendLine($"{indent}/// <exception cref=\"{ex.Type}\">");
                sb.AppendLine($"{indent}///     {ex.Description}");
                sb.AppendLine($"{indent}/// </exception>");
            }
    }

    private static void EmitProperty(
        StringBuilder sb,
        string assetType,
        string propName,
        string idClassName,
        string fieldName,
        string getExpr,
        string? setExpr,
        bool isStatic)
    {
        string modifier = isStatic ? "public static" : "public";
        string getter = getExpr.Replace("key", $"{idClassName}.{fieldName}");

        if (setExpr is null)
        {
            sb.AppendLine($"    {modifier} {assetType} {propName} => {getter};");
        }
        else
        {
            string setter = setExpr.Replace("key", $"{idClassName}.{fieldName}");
            sb.AppendLine($"    {modifier} {assetType} {propName}");
            sb.AppendLine("    {");
            sb.AppendLine($"        get => {getter};");
            sb.AppendLine($"        set => {setter};");
            sb.AppendLine("    }");
        }
    }

    private static Dictionary<string, DocEntry> LoadDocs(string? json)
    {
        if (json is null) return new Dictionary<string, DocEntry>();
        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, DocEntry>>(json, JsonOptions)
                   ?? new Dictionary<string, DocEntry>();
        }
        catch
        {
            return new Dictionary<string, DocEntry>();
        }
    }

    private static string ToPascalCase(string id)
    {
        return string.Concat(id.Split('_')
            .Where(s => s.Length > 0)
            .Select(s => char.ToUpper(s[0]) + s.Substring(1).ToLower()));
    }
}