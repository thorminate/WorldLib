extern alias GameAsm;

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Provides centralized management for materials in the global <see cref="GameAsm::LibraryMaterials"/> cache.
///     This ensures that materials are consistently registered and prevents accidental overwrites
///     of existing materials with the same name.
/// </summary>
public static class MaterialRegistry
{
    private static Dictionary<string, Material> dict => GameAsm::LibraryMaterials.instance.dict;
    /// <summary>
    ///     Registers a material in the global library if it does not already exist.
    ///     If a material with the same name already exists, it validates that the existing material
    ///     is identical to the one being registered.
    /// </summary>
    /// <param name="material">The <see cref="Material"/> to register.</param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="material"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if a material with the same name already exists in the library,
    ///     but it is not the same material instance.
    /// </exception>
    /// <remarks>
    ///     This method is intended to be used when adding new materials dynamically, for example
    ///     when creating status assets or modding content. It centralizes library access
    ///     and prevents accidental duplication of material names.
    /// </remarks>
    public static void Register(Material material)
    {
        if (material == null)
            throw new ArgumentNullException(nameof(material));

        if (dict.TryGetValue(material.name, out var existing))
        {
            if (!existing!.Equals(material))
            {
                throw new InvalidOperationException(
                    $"Material '{material.name}' already exists as a different material. " +
                    "Use a different name.");
            }
        }
        else
        {
            dict.Add(material.name, material);
        }
    }

    /// <summary>
    ///     Checks whether a material with the given name already exists in the global library.
    /// </summary>
    /// <param name="materialName">The name of the material to check.</param>
    /// <returns>
    ///     <c>true</c> if the material exists; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///     Use this method before registering a material if you want to avoid exceptions
    ///     from <see cref="Register(Material)"/> due to name conflicts.
    /// </remarks>
    public static bool Exists(string materialName)
    {
        return !string.IsNullOrWhiteSpace(materialName) &&
               dict.ContainsKey(materialName);
    }
}