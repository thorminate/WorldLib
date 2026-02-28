using System.Text.RegularExpressions;
using HarmonyLib;
using WorldLib.Core;
using WorldLib.Models.Events.History;

namespace WorldLib.Patches;

extern alias GameAsm;

[HarmonyPatch]
public static class HistoryHudPatch
{
    [HarmonyPatch(typeof(GameAsm::HistoryHud), "newText")]
    [HarmonyPostfix]

    // ReSharper disable once InconsistentNaming
    public static void Postfix_NewText(object __instance, GameAsm::WorldLogMessage? pMessage)
    {
        if (pMessage == null) return;

        var asset = GameAsm::WorldLogMessageExtensions.getAsset(pMessage);
        string localeId;

        if (asset.random_ids > 0)
        {
            int pIndex = pMessage.timestamp % asset.random_ids + 1;
            localeId = asset.getLocaleID(pIndex);
        }
        else
        {
            localeId = asset.getLocaleID();
        }

        string? messageText = GameAsm::LocalizedTextManager.getText(localeId, pForceEnglish: true);
        asset.text_replacer?.Invoke(pMessage, ref messageText);

        messageText = Regex.Replace(messageText, "<.*?>", string.Empty);

        Events.InvokeHistoryEntryAdded(new HistoryEntryEventArgs(messageText, pMessage));
    }
}