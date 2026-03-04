using UnityEngine;
using WorldLib.Utils;

namespace WorldLib.Models.History;

extern alias GameAsm;

/// <summary>
///     Represents a single entry in the world history log, providing read-only access
///     to the underlying <see cref="GameAsm::WorldLogMessage" /> data.
/// </summary>
public sealed class HistoryEntry : AbstractionOf<GameAsm::WorldLogMessage>
{
    internal HistoryEntry(GameAsm::WorldLogMessage store) : base(store)
    {
    }

    /// <summary>
    ///     Gets the timestamp of when this event occurred.
    /// </summary>
    public int Timestamp => Raw.timestamp;

    /// <summary>
    ///     Gets the first custom special string associated with the log entry.
    ///     This is usually used for names and places.
    /// </summary>
    public string Special1 => Raw.special1;

    /// <summary>
    ///     Gets the second custom special string associated with the log entry.
    ///     This is usually used for names and places.
    /// </summary>
    public string Special2 => Raw.special2;

    /// <summary>
    ///     Gets the third custom special string associated with the log entry.
    ///     This is usually used for names and places.
    /// </summary>
    public string Special3 => Raw.special3;

    /// <summary>
    ///     Gets the color for the first special field.
    /// </summary>
    /// <seealso cref="Special1" />
    public Color ColorSpecial1 => ParseHex(Raw.color_special_1);

    /// <summary>
    ///     Gets the color for the second special field.
    /// </summary>
    /// <seealso cref="Special2" />
    public Color ColorSpecial2 => ParseHex(Raw.color_special_2);

    /// <summary>
    ///     Gets the color for the third special field.
    /// </summary>
    /// <seealso cref="Special3" />
    public Color ColorSpecial3 => ParseHex(Raw.color_special_3);

    /// <summary>
    ///     Gets the location associated with this event.
    /// </summary>
    public Vector2? Location => Raw.location;

    /// <summary>
    ///     Gets the actor associated with this event.
    /// </summary>
    //TODO: Abstract Actor
    public GameAsm::Actor? Actor => Raw.unit;

    /// <summary>
    ///     Gets the kingdom associated with this event.
    /// </summary>
    //TODO: Abstract Kingdom.
    public GameAsm::Kingdom? Kingdom => Raw.kingdom;

    /// <summary>
    ///     Returns true if this log entry has a valid location.
    /// </summary>
    public bool HasLocation => Raw is { x: not null, y: not null };

    /// <summary>
    ///     Returns true if this log entry is associated with a unit.
    /// </summary>
    public bool HasActor => Raw.unit_id != -1;

    /// <summary>
    ///     Returns true if this log entry is associated with a kingdom.
    /// </summary>
    public bool HasKingdom => Raw.kingdom_id != -1;

    private static Color ParseHex(string hex)
    {
        if (!hex.StartsWith("#"))
            hex = "#" + hex;

        ColorUtility.TryParseHtmlString(hex, out var color);
        return color;
    }
}