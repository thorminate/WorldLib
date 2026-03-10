namespace WorldLib.Models.Assets;

/// <summary>
///     A bearer of something localized.
/// </summary>
public interface ILocalizedAsset
{
    /// <summary>
    ///     A localized text. Oftentimes used for primary names of stuff.
    /// </summary>
    string Localized { get; }
}