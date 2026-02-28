using System;

namespace WorldLib.Models.Options;

public class WorldSavingOptions
{
    private int _slot;

    public int Slot
    {
        get => _slot;
        set
        {
            if (value is < 1 or > 60)
                throw new ArgumentOutOfRangeException(nameof(Slot), "Slot must be between 1 and 60.");
            _slot = value;
        }
    }
}