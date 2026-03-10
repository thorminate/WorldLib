using System;
using WorldLib.Models.Events.History;
using WorldLib.Models.History;

namespace WorldLib.Core;

/// <summary>
///     Events that mods can hook into to add functionality at specific moments.
/// </summary>
public class Events
{
    /// <summary>
    ///     An event that is called when the game initially starts.
    /// </summary>
    public static event Action? GameStarted;

    internal static void InvokeGameStarted()
    {
        GameStarted?.Invoke();
    }

    /// <summary>
    ///     Called when a new <see cref="HistoryEntry" /> is added.
    /// </summary>
    public static event EventHandler<HistoryEntryEventArgs>? HistoryEntryAdded;

    internal static void InvokeHistoryEntryAdded(HistoryEntryEventArgs e)
    {
        HistoryEntryAdded?.Invoke(null, e);
    }
}