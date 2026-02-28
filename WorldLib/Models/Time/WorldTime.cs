using System;

namespace WorldLib.Models.Time;

/// <summary>
///     Represents a point of time in a world.
/// </summary>
/// <remarks>
///     In world box, time is stored as a double.
///     It is calculated by taking a year amount and multiplying that by 60 (<c>12 * 5</c>).
/// </remarks>
public class WorldTime
{
    internal readonly Func<double> Getter;

    /// <inheritdoc cref="WorldTime" />
    /// <param name="value">The week <see langword="double" /> ascertaining to the timepoint.</param>
    public WorldTime(double value)
    {
        Getter = () => value;
    }

    /// <inheritdoc cref="WorldTime" />
    /// <param name="getter">A getter that will be used on every access to this object.</param>
    public WorldTime(Func<double> getter)
    {
        Getter = getter;
    }

    private double Cur => Getter();

    /// <summary>
    ///     Returns a <see langword="double" /> for the timepoint converted to years.
    /// </summary>
    /// <remarks>
    ///     Calculated by taking the time and dividing it by 60 (<c>12 * 5</c>).
    /// </remarks>
    public double Years => Cur / 60;

    /// <summary>
    ///     Returns a <see langword="double" /> for the timepoint converted to moons.
    /// </summary>
    /// <remarks>
    ///     Calculated by taking the time and dividing it by 5.
    /// </remarks>
    public double Moons => Cur / 5;

    /// <summary>
    ///     Returns the raw time as a double.
    /// </summary>
    public double Raw => Cur;
}