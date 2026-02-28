using System;
using WorldLib.Models.Events.History;

namespace WorldLib.Core;

public class Events
{
    public static event Action? GameStarted;

    internal static void InvokeGameStarted()
    {
        GameStarted?.Invoke();
    }

    public static event EventHandler<HistoryEntryEventArgs>? HistoryEntryAdded;

    internal static void InvokeHistoryEntryAdded(HistoryEntryEventArgs e)
    {
        HistoryEntryAdded?.Invoke(null, e);
    }
}