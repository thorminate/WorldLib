using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using WorldLib.Core;
using static WorldLib.Info;

namespace WorldLib;

extern alias GameAsm;

[BepInPlugin(Guid, Name, Version)]
public class Plugin : BaseUnityPlugin
{
    internal static Plugin Instance { get; set; } = null!;
    internal new static ManualLogSource Logger => Instance._logger;
    private Harmony? Harmony { get; set; }

    // ReSharper disable once InconsistentNaming
    private ManualLogSource _logger => base.Logger;

    #region Unity

    private void Awake()
    {
        Instance = this;

        gameObject.transform.parent = null;
        gameObject.hideFlags = HideFlags.HideAndDontSave;

        Patch();

        Events.GameStarted += () => { World.Laws().EvolutionEvents = true; };
    }

    #endregion

    #region Harmony

    private void Patch()
    {
        Harmony ??= new Harmony(Info.Metadata.GUID);

        Harmony.PatchAll();
    }

    #endregion
}