extern alias GameAsm;
using System;
using System.Collections.Generic;
using System.Reflection;
using WorldLib.Utils;

namespace WorldLib.Models.Laws;

/// <summary>
///     Provides access to the game's world law system.
/// </summary>
/// <remarks>
///     Changes to properties immediately propagate to the underlying game engine state,
///     so external caching or copying is generally unnecessary.
/// </remarks>
public sealed partial class Laws : AbstractionOf<GameAsm::WorldLaws>
{
    private static readonly Type LawLibraryType = typeof(GameAsm::WorldLawLibrary);
    private static readonly Dictionary<string, FieldInfo> LawLibraryFieldCache = new();

    internal Laws() : base(() => GameAsm::World.world.world_laws)
    {
    }

    /// <summary>
    ///     Retrieves the value of a world law using its internal key.
    /// </summary>
    /// <param name="key">The internal world law identifier.</param>
    /// <returns>
    ///     <see langword="true" /> if the law is enabled; otherwise <see langword="false" />.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if the specified world law key does not exist.
    /// </exception>
    private bool Get(string key)
    {
        return Raw.dict.TryGetValue(key, out var option)
            ? option.boolVal
            : throw new KeyNotFoundException($"World law '{key}' not found.");
    }

    /// <summary>
    ///     Sets the value of a world law using its internal key.
    /// </summary>
    /// <param name="key">The internal world law identifier.</param>
    /// <param name="value">
    ///     <see langword="true" /> to enable the law;
    ///     <see langword="false" /> to disable it.
    /// </param>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if the specified world law key does not exist.
    /// </exception>
    private void Set(string key, bool value)
    {
        if (!Raw.dict.TryGetValue(key, out var option))
            throw new KeyNotFoundException($"World law '{key}' not found.");

        bool lastValue = option.boolVal;
        if (value == lastValue) return;

        option.boolVal = value;

        if (value)
        {
            var asset = GetAsset(key);
            asset.on_state_enabled?.Invoke(option);
        }

        Raw.updateCaches();
        option.on_switch?.Invoke(option);

        var currentWindow = GameAsm::ScrollWindow.getCurrentWindow();
        if (currentWindow == null) return;

        var editorTransform = currentWindow.transform.Find(
            "Background/Scroll View/Viewport/Content/content_main");
        if (editorTransform == null) return;

        var editor = editorTransform.GetComponent<GameAsm::WorldLawsEditor>();
        if (editor == null) return;

        editor.updateButtons();
    }

    private static GameAsm::WorldLawAsset GetAsset(string k)
    {
        if (LawLibraryFieldCache.TryGetValue(k, out var field))
            return (GameAsm::WorldLawAsset)field.GetValue(null);

        field = LawLibraryType.GetField(k, BindingFlags.Static | BindingFlags.Public)
                ?? throw new Exception($"WorldLawLibrary does not contain a field '{k}'");

        if (field.GetValue(null) is not GameAsm::WorldLawAsset asset)
            throw new Exception($"WorldLawLibrary field '{k}' is not a WorldLawAsset");

        LawLibraryFieldCache[k] = field;
        return asset;
    }
}