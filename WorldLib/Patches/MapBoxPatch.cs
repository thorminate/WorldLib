using HarmonyLib;
using WorldLib.Core;

namespace WorldLib.Patches;

extern alias GameAsm;

[HarmonyPatch(typeof(GameAsm::MapBox))]
internal static class MapBoxPatch
{
    [HarmonyPatch(nameof(GameAsm::MapBox.addLastStep))]
    [HarmonyPostfix]
    private static void Postfix_AddLastStep()
    {
        Events.InvokeGameStarted();
    }
}