using UnityEngine;
using WorldLib.Utils;

namespace WorldLib.Models.History;

extern alias GameAsm;

/// <summary>
///     Represents a single entry in the world history log, providing read-only access
///     to the underlying <see cref="GameAsm::WorldLogMessage" /> data.
/// </summary>
public sealed class WorldHistoryEntry : AbstractionOf<GameAsm::WorldLogMessage>
{
    internal WorldHistoryEntry(GameAsm::WorldLogMessage store) : base(store)
    {
    }

    /// <summary>
    ///     Gets the timestamp of when this event occurred.
    /// </summary>
    public int Timestamp => Base.timestamp;

    /// <summary>
    ///     Gets the first custom special string associated with the log entry.
    ///     This is usually used for names and places.
    /// </summary>
    public string Special1 => Base.special1;

    /// <summary>
    ///     Gets the second custom special string associated with the log entry.
    ///     This is usually used for names and places.
    /// </summary>
    public string Special2 => Base.special2;

    /// <summary>
    ///     Gets the third custom special string associated with the log entry.
    ///     This is usually used for names and places.
    /// </summary>
    public string Special3 => Base.special3;

    /// <summary>
    ///     Gets the color for the first special field.
    /// </summary>
    /// <seealso cref="Special1" />
    public Color ColorSpecial1 => ParseHex(Base.color_special_1);

    /// <summary>
    ///     Gets the color for the second special field.
    /// </summary>
    /// <seealso cref="Special2" />
    public Color ColorSpecial2 => ParseHex(Base.color_special_2);

    /// <summary>
    ///     Gets the color for the third special field.
    /// </summary>
    /// <seealso cref="Special3" />
    public Color ColorSpecial3 => ParseHex(Base.color_special_3);

    /// <summary>
    ///     Gets the location associated with this event.
    /// </summary>
    public Vector2? Location => Base.location;

    /// <summary>
    ///     Gets the actor associated with this event.
    /// </summary>
    //TODO: Abstract Actor
    public GameAsm::Actor? Actor => Base.unit;

    /// <summary>
    ///     Gets the kingdom associated with this event.
    /// </summary>
    //TODO: Abstract Kingdom.
    public GameAsm::Kingdom? Kingdom => Base.kingdom;

    /// <summary>
    ///     Returns true if this log entry has a valid location.
    /// </summary>
    public bool HasLocation => Base is { x: not null, y: not null };

    /// <summary>
    ///     Returns true if this log entry is associated with a unit.
    /// </summary>
    public bool HasActor => Base.unit_id != -1;

    /// <summary>
    ///     Returns true if this log entry is associated with a kingdom.
    /// </summary>
    public bool HasKingdom => Base.kingdom_id != -1;

    private static Color ParseHex(string hex)
    {
        if (!hex.StartsWith("#"))
            hex = "#" + hex;

        ColorUtility.TryParseHtmlString(hex, out var color);
        return color;
    }
}