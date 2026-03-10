namespace WorldLib.Models.Options;

/// <summary>
///     An enum for all normal game speeds in the game.
/// </summary>
public enum WorldSpeeds
{
    /// <summary>
    ///     The game is paused and nothing happens.
    /// </summary>
    Pause,

    // ReSharper disable once InconsistentNaming
    /// <summary>
    ///     Half speed (0.5x).
    /// </summary>
    Slow_Mo, // underscore is necessary!!

    /// <summary>
    ///     Normal speed (1x).
    /// </summary>
    X1,

    /// <summary>
    ///     Double speed (2x).
    /// </summary>
    X2,

    /// <summary>
    ///     Triple speed (3x).
    /// </summary>
    X3,

    /// <summary>
    ///     Quadruple speed (4x).
    /// </summary>
    X4,

    /// <summary>
    ///     5x speed.
    /// </summary>
    X5,

    /// <summary>
    ///     10x speed.
    /// </summary>
    X10,

    /// <summary>
    ///     15x speed.
    /// </summary>
    X15,

    /// <summary>
    ///     20x speed.
    /// </summary>
    X20,

    /// <summary>
    ///     40x speed. The fastest available game speed.
    /// </summary>
    X40
}