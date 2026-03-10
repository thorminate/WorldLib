namespace WorldLib.Models.Assets;

/// <summary>
///     A bearer of 2 descriptions.
/// </summary>
public interface IDescription2Asset : IDescriptionAsset
{
    /// <summary>
    ///     A second description, localized for your convenience.
    /// </summary>
    public string LocalizedDescription2 { get; }
}