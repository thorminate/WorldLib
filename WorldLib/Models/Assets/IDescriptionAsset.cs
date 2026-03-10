namespace WorldLib.Models.Assets;

/// <summary>
///     A bearer of a description.
/// </summary>
public interface IDescriptionAsset : ILocalizedAsset
{
    /// <summary>
    ///     A description, localized for your convenience.
    /// </summary>
    public string LocalizedDescription { get; }
}