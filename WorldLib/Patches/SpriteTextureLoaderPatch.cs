extern alias GameAsm;
using HarmonyLib;
using UnityEngine;
using WorldLib.Registries;
using SpriteTextureLoader = GameAsm::SpriteTextureLoader;

// ReSharper disable InconsistentNaming

namespace WorldLib.Patches;

[HarmonyPatch(typeof(SpriteTextureLoader))]
internal static class SpriteTextureLoaderPatch
{
    [HarmonyPatch(nameof(SpriteTextureLoader.getSprite))]
    [HarmonyPostfix]
    private static void Postfix_GetSprite(ref Sprite __result, string pPath)
    {
        if (__result != null) return;
        if (!Sprites.Dict.TryGetValue(pPath, out var sprite)) return;

        __result = sprite;
        SpriteTextureLoader._cached_sprites[pPath] = sprite;
    }


    [HarmonyPatch(nameof(SpriteTextureLoader.getSpriteList))]
    [HarmonyPostfix]
    private static void Postfix_GetSpriteList(ref Sprite[] __result, string pPath)
    {
        if (__result is { Length: > 0 }) return;
        if (!Sprites.Dict.TryGetValue(pPath, out var sprite)) return;

        __result = [sprite];
        SpriteTextureLoader._cached_sprite_list[pPath] = __result;
    }
}