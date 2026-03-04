namespace WorldLib.Models.Options;

/// <summary>
///     An enum for all normal game speeds in the game.
/// </summary>
public enum WorldSpeeds
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    Pause,

    // ReSharper disable once InconsistentNaming
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}