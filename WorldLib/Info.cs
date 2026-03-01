namespace WorldLib;

/// <summary>
///     Useful for adding this as a dependency and to prevent spelling mishaps.
/// </summary>
public static class Info
{
    /// <summary>
    ///     Represents this mods GUID, useful for BepInEx dependency definitions.
    /// </summary>
    public const string Guid = "Thorminate.WorldLib";

    /// <summary>
    ///     Represents this mods name.
    /// </summary>
    public const string Name = "WorldLib";

    /// <summary>
    ///     Represents the mod version present in this assembly distribution. Useful for making sure the mod at runtime
    ///     matches the one at compile-time and for BepInEx dependency version definitions.
    /// </summary>
    public const string Version = "0.0.1";
}