// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace WorldLib.SourceGen;

public sealed class DocEntry
{
    public string? Summary { get; set; }
    public string[]? Remarks { get; set; }
    public string? Value { get; set; }
    public string? PropertyName { get; set; }
    public DocException[]? Exceptions { get; set; }
}

public sealed class DocException
{
    public string? Type { get; set; }
    public string? Description { get; set; }
}