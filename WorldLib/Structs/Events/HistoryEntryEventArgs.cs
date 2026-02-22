using System;

namespace WorldLib.Structs.Events;

extern alias GameAsm;

public class HistoryEntryEventArgs(string text, GameAsm::WorldLogMessage message) : EventArgs
{
    public string Text { get; } = text;

    // TODO: Abstract WorldLogMessage in a separate class.
    public GameAsm::WorldLogMessage Message { get; } = message;
}