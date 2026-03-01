namespace WorldLib.Models.Generic;

public interface IAsset
{
    /// <summary>
    ///     ID of the asset. Is used as the primary identifier in internal libraries.
    /// </summary>
    string Id { get; set; }

    /// <summary>
    ///     Hash code of the asset. Is used for equality checks with other assets.
    /// </summary>
    int Hash { get; set; }
}