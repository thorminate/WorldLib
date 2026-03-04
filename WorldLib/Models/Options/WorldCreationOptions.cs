using System;

namespace WorldLib.Models.Options;

/// <summary>
///     Represents a set of options used to configure procedural world generation.
/// </summary>
/// <remarks>
///     This class controls terrain shape, noise layers, biome behavior,
///     environmental features, and initial world modifiers.
/// </remarks>
public struct WorldCreationOptions
{
    /// <inheritdoc cref="WorldCreationOptions" />
    public WorldCreationOptions()
    {
    }

    /// <summary>
    ///     Defines the overall size preset of the generated world.
    /// </summary>
    /// <remarks>
    ///     The numeric values correspond to internal size multipliers used
    ///     during terrain generation.
    /// </remarks>
    public enum WorldSize
    {
        /// <summary>A very small world with minimal terrain area. 128x128 tiles (2x2 chunks)</summary>
        Tiny = 1,

        /// <summary>A small world with limited exploration space. 192x192 tiles (3x3 chunks)</summary>
        Small,

        /// <summary>The default world size with balanced proportions. 256x256 tiles (4x4 chunks)</summary>
        Standard,

        /// <summary>A larger-than-default world. 320x320 tiles (5x5 chunks)</summary>
        Large,

        /// <summary>A very large world. 384x384 tiles (6x6 chunks)</summary>
        Huge,

        /// <summary>An extremely large world. 448x448 tiles (7x7 chunks)</summary>
        Gigantic,

        /// <summary>A massive world with significant terrain volume. 512x512 tiles (8x8 chunks)</summary>
        Titanic,

        /// <summary>The largest available preset. 576x576 tiles (9x9 chunks)</summary>
        Iceberg
    }

    private int _bonusNoise = 5;
    private int _detailNoise = 5;
    private int _mainNoise = 5;

    // See MapGenValues in assembly code.
    private int _randomShapes = 5;


    /// <summary>
    ///     Gets or sets the overall size preset of the world.
    /// </summary>
    /// <value>
    ///     The selected <see cref="WorldSize" />. Defaults to <see cref="WorldSize.Standard" />.
    /// </value>
    public WorldSize Size { get; set; } = WorldSize.Standard;

    /// <summary>
    ///     Gets or sets the amount of random terrain shape variation applied during generation.
    /// </summary>
    /// <remarks>
    ///     Higher values increase irregular terrain formations.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the value is not between 0 and 40.
    /// </exception>
    public int RandomShapes
    {
        get => _randomShapes;
        set
        {
            if (value is < 0 or > 40)
                throw new ArgumentOutOfRangeException(nameof(RandomShapes), "RandomShapes must be between 0 and 40.");
            _randomShapes = value;
        }
    }

    /// <summary>
    ///     Gets or sets the primary terrain noise intensity.
    /// </summary>
    /// <remarks>
    ///     This layer typically defines large-scale elevation features such as
    ///     continents and major landmasses.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the value is not between 0 and 30.
    /// </exception>
    public int MainNoise
    {
        get => _mainNoise;
        set
        {
            if (value is < 0 or > 30)
                throw new ArgumentOutOfRangeException(nameof(MainNoise), "MainNoise must be between 0 and 30.");
            _mainNoise = value;
        }
    }

    /// <summary>
    ///     Gets or sets the secondary detail noise intensity.
    /// </summary>
    /// <remarks>
    ///     This layer adds medium-scale variation to terrain,
    ///     refining the output of <see cref="MainNoise" />.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the value is not between 0 and 30.
    /// </exception>
    public int DetailNoise
    {
        get => _detailNoise;
        set
        {
            if (value is < 0 or > 30)
                throw new ArgumentOutOfRangeException(nameof(DetailNoise), "DetailNoise must be between 0 and 30.");
            _detailNoise = value;
        }
    }

    /// <summary>
    ///     Gets or sets the tertiary noise intensity.
    /// </summary>
    /// <remarks>
    ///     Used for minor terrain variations and subtle irregularities.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the value is not between 0 and 30.
    /// </exception>
    public int BonusNoise
    {
        get => _bonusNoise;
        set
        {
            if (value is < 0 or > 30)
                throw new ArgumentOutOfRangeException(nameof(BonusNoise), "BonusNoise must be between 0 and 30.");
            _bonusNoise = value;
        }
    }

    /// <summary>
    ///     Gets or sets whether biomes should be distributed randomly.
    /// </summary>
    public bool RandomBiomes { get; set; } = true;

    /// <summary>
    ///     Gets or sets whether the edge of the map should have a border of mountains.
    /// </summary>
    public bool AddMountainEdges { get; set; } = false;

    /// <summary>
    ///     Gets or sets whether vegetation (such as trees and plants) should be generated.
    /// </summary>
    public bool AddVegetation { get; set; } = true;

    /// <summary>
    ///     Gets or sets whether resources should be spawned across the world.
    /// </summary>
    public bool AddResources { get; set; } = true;

    /// <summary>
    ///     Gets or sets whether a central lake should be generated.
    /// </summary>
    public bool AddCenterLake { get; set; } = false;

    /// <summary>
    ///     Gets or sets whether land elevation should gradually increase toward the center.
    /// </summary>
    public bool AddCenterGradientLand { get; set; } = true;

    /// <summary>
    ///     Gets or sets whether the world edges should be rounded and become circular.
    /// </summary>
    public bool GradientRoundEdges { get; set; } = true;

    /// <summary>
    ///     Gets or sets whether the world should have more square-shaped borders.
    /// </summary>
    public bool SquareEdges { get; set; } = false;

    /// <summary>
    ///     Gets or sets whether a ring-shaped tree distribution effect should be applied.
    /// </summary>
    public bool RingTreeEffect { get; set; } = true;

    /// <summary>
    ///     Gets or sets whether the terrain should favor lower overall elevation.
    /// </summary>
    public bool LowGround { get; set; } = false;

    /// <summary>
    ///     Gets or sets whether the terrain should favor higher overall elevation.
    /// </summary>
    public bool HighGround { get; set; } = false;

    /// <summary>
    ///     Gets or sets whether mountain generation should be disabled entirely.
    /// </summary>
    public bool RemoveMountains { get; set; } = false;

    /// <summary>
    ///     Gets or sets whether the world should start as cursed with forbidden knowledge.
    /// </summary>
    public bool StartWithForbiddenKnowledge { get; set; } = false;
}