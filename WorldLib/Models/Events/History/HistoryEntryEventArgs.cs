using System;
using WorldLib.Core;

namespace WorldLib.Models.Events.History;

extern alias GameAsm;

/// <summary>
///     Event args for the <see cref="Events.HistoryEntryAdded" /> event.
/// </summary>
public class HistoryEntryEventArgs(string text, GameAsm::WorldLogMessage message) : EventArgs
{
    /// <summary>
    ///     A cleansed text derived from the message contents.
    /// </summary>
    public string Text { get; } = text;

    /// <summary>
    ///     Represents the message object.
    /// </summary>
    // TODO: Abstract WorldLogMessage in a separate class.
    public GameAsm::WorldLogMessage Message { get; } = message;
}