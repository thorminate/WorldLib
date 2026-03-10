extern alias GameAsm;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorldLib.Registries;

/// <summary>
///     Provides centralized management for sprites registered by mods or patches.
/// </summary>
public static class Sprites
{
    internal static readonly Dictionary<string, Sprite> Dict = new();

    /// <summary>
    ///     Registers a sprite in the registry if it does not already exist.
    ///     If a sprite with the same name already exists, it validates that the existing
    ///     sprite is the same instance.
    /// </summary>
    /// <param name="sprite">The <see cref="Sprite" /> to register.</param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="sprite" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if a sprite with the same name already exists in the registry,
    ///     but it is not the same instance.
    /// </exception>
    public static void Register(Sprite sprite)
    {
        if (sprite == null)
            throw new ArgumentNullException(nameof(sprite));

        if (Dict.TryGetValue(sprite.name, out var existing))
        {
            if (!existing!.Equals(sprite))
                throw new InvalidOperationException(
                    $"Sprite '{sprite.name}' already exists as a different sprite. " +
                    "Use a different name.");
        }
        else
        {
            Dict.Add(sprite.name, sprite);
        }
    }

    /// <summary>
    ///     Retrieves a registered sprite by name.
    /// </summary>
    /// <param name="spriteName">The name of the sprite to retrieve.</param>
    /// <returns>The <see cref="Sprite" /> if found; otherwise, <c>null</c>.</returns>
    public static Sprite? Get(string spriteName)
    {
        if (string.IsNullOrWhiteSpace(spriteName))
            return null;

        Dict.TryGetValue(spriteName, out var sprite);
        return sprite;
    }

    /// <summary>
    ///     Checks whether a sprite with the given name is already registered.
    /// </summary>
    /// <param name="spriteName">The name of the sprite to check.</param>
    /// <returns><c>true</c> if the sprite exists; otherwise, <c>false</c>.</returns>
    public static bool Exists(string spriteName)
    {
        return !string.IsNullOrWhiteSpace(spriteName) &&
               Dict.ContainsKey(spriteName);
    }
}