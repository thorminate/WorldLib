extern alias GameAsm;
using System;

namespace WorldLib.Models.Statuses;

/// <summary>
///     Represents the tier of a status effect, used to filter which statuses can be applied to a sim object.
/// </summary>
public enum StatusTier
{
    /// <summary>
    ///     No tier, the sim object doesn't accept any status effects.
    /// </summary>
    None,

    /// <summary>
    ///     A basic-tier status. General stuff like <see cref="Statuses.Magnetized" />
    /// </summary>
    Basic,

    /// <summary>
    ///     An advanced-tier status. Stuff like <see cref="Statuses.Starving" /> and other actor-like needs.
    /// </summary>
    Advanced
}

internal static class StatusTierHelper
{
    internal static StatusTier FromGame(GameAsm::StatusTier value)
    {
        return value switch
        {
            GameAsm::StatusTier.None => StatusTier.None,
            GameAsm::StatusTier.Basic => StatusTier.Basic,
            GameAsm::StatusTier.Advanced => StatusTier.Advanced,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    internal static GameAsm::StatusTier ToGame(StatusTier value)
    {
        return value switch
        {
            StatusTier.None => GameAsm::StatusTier.None,
            StatusTier.Basic => GameAsm::StatusTier.Basic,
            StatusTier.Advanced => GameAsm::StatusTier.Advanced,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}