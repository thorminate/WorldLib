using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using WorldLib.Core;

namespace WorldLib.Utils;

/// <summary>
///     A simple and opinionated way to instantiate a mod.
///     You must still apply your own BepInEx attributes above the plugin definition.
/// </summary>
/// <typeparam name="TSelf">The type of the class that is extending this</typeparam>
public abstract class WorldBoxMod<TSelf>
    : BaseUnityPlugin
    where TSelf : WorldBoxMod<TSelf>
{
    /// <summary>
    ///     A singleton instance referencing to the plugins active object.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public static TSelf Instance = null!;

    private Harmony? Harmony { get; set; }

    /// <summary>
    ///     The public logger for this mod, should be used over <see cref="Debug.Log(object)" />.
    /// </summary>
    public new static ManualLogSource Logger => Instance._logger;

    // ReSharper disable once InconsistentNaming
    private ManualLogSource _logger => base.Logger;

    /// <inheritdoc cref="Awake" />
    public void Awake()
    {
        Instance = (TSelf)this;

        gameObject.transform.parent = null;
        gameObject.hideFlags = HideFlags.HideAndDontSave;

        Patch(Instance.Info.Metadata.GUID);

        Load();

        Events.GameStarted += Begin;

        Logger.LogInfo($"{Instance.Info.Metadata.Name} loaded!");
    }

    /// <summary>
    ///     This runs after the game is set up and initialized.
    /// </summary>
    protected abstract void Begin();

    /// <summary>
    ///     This runs directly after the mod is initially picked up by BepInEx,
    ///     world state is incomplete at this stage.
    /// </summary>
    protected abstract void Load();

    private void Patch(string guid)
    {
        Harmony ??= new Harmony(guid);
        Harmony.PatchAll(typeof(TSelf).Assembly);
    }
}