extern alias GameAsm;
using System.Collections.Generic;
using WorldLib.Utils;

namespace WorldLib.Models.Statuses;

/// <summary>
///     A static class for easy access to the game's statuses.
/// </summary>
public partial class Statuses
{
    /// <inheritdoc cref="AbstractionOf{TStore}.Raw" />
    public static GameAsm::StatusLibrary Raw => GameAsm::AssetManager.status;

    private static void Set(string key, StatusAsset asset)
    {
        Raw.dict[key] = asset.Raw;
    }

    private static StatusAsset Get(string key)
    {
        return Raw.dict.TryGetValue(key, out var asset)
            ? new StatusAsset(asset)
            : throw new KeyNotFoundException($"World law '{key}' not found.");
    }
}