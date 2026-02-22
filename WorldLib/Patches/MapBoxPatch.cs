using HarmonyLib;
using WorldLib.Core;

namespace WorldLib.Patches;

extern alias GameAsm;

[HarmonyPatch]
public static class MapBoxPatch
{
    [HarmonyPatch(typeof(GameAsm::MapBox), "addLastStep")]
    [HarmonyPostfix]
    public static void Postfix_AddLastStep()
    {
        Events.InvokeGameStarted();
    }
}