namespace WorldLib.Models.Options;

// ReSharper disable InconsistentNaming
public sealed class WorldSetSpeedOptions
{
    public enum WorldSpeeds
    {
        Pause,
        Slow_Mo, // underscore is necessary!!
        X1,
        X2,
        X3,
        X4,
        X5,
        X10,
        X15,
        X20,
        X40
    }

    public WorldSpeeds Speed { get; set; } = WorldSpeeds.X1;
}