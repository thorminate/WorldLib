using System;
using System.Diagnostics;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedTypeParameter

namespace WorldLib.SourceGen;

[Conditional("WORLDLIB_SOURCEGEN")]
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
internal sealed class LibraryGenAttribute<TKey, TAsset> : Attribute
{
    public Type IdClass { get; set; } = null!;
    public string Docs { get; set; } = null!;
    public string Get { get; set; } = null!;
    public string? Set { get; set; }
    public string? Strip { get; set; }
    public string? Namespace { get; set; }
    public string? TargetClass { get; set; }
    public string[]? Usings { get; set; }
    public bool Static { get; set; } = true;
    public bool StaticMembers { get; set; } = true;
}