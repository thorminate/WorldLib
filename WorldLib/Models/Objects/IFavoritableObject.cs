namespace WorldLib.Models.Objects;

/// <summary>
///     Represents an object that can be favorited
/// </summary>
public interface IFavoritableObject
{
    /// <summary>
    ///     Whether this object is a favourite.
    /// </summary>
    bool IsFavorite();
}