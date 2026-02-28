extern alias GameAsm;
using System;

namespace WorldLib.Models.Status;

public enum StatusTier
{
    None,
    Basic,
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