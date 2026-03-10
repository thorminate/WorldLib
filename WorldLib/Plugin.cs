extern alias GameAsm;
using BepInEx;
using WorldLib.Utils;
using static WorldLib.Info;

namespace WorldLib;

extern alias GameAsm;

/// <summary>
///     The main entrypoint of WorldLib.
/// </summary>
[BepInPlugin(Guid, Name, Version)]
public class Plugin : WorldBoxMod<Plugin>
{
    /// <inheritdoc />
    protected override void Begin()
    {
    }

    /// <inheritdoc />
    protected override void Load()
    {
    }
}