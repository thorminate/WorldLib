using System;

namespace WorldLib.Structs.Worlds;

public class WorldCreationOptions
{
    public enum WorldSize
    {
        Tiny = 1,
        Small,
        Standard,
        Large,
        Huge,
        Gigantic,
        Titanic,
        Iceberg
    }

    private int _bonusNoise = 5;

    private int _detailNoise = 5;

    private int _mainNoise = 5;

    // see MapGenValues in assembly code
    private int _randomShapes = 5;

    // Basic world setup
    public WorldSize Size { get; set; } = WorldSize.Standard;

    public int RandomShapes
    {
        get => _randomShapes;
        set
        {
            if (value < 0 || value > 40)
                throw new ArgumentOutOfRangeException(nameof(RandomShapes), "RandomShapes must be between 0 and 40.");
            _randomShapes = value;
        }
    }

    public int MainNoise
    {
        get => _mainNoise;
        set
        {
            if (value < 0 || value > 30)
                throw new ArgumentOutOfRangeException(nameof(MainNoise), "MainNoise must be between 0 and 30.");
            _mainNoise = value;
        }
    }

    public int DetailNoise
    {
        get => _detailNoise;
        set
        {
            if (value < 0 || value > 30)
                throw new ArgumentOutOfRangeException(nameof(DetailNoise), "DetailNoise must be between 0 and 30.");
            _detailNoise = value;
        }
    }

    public int BonusNoise
    {
        get => _bonusNoise;
        set
        {
            if (value < 0 || value > 30)
                throw new ArgumentOutOfRangeException(nameof(BonusNoise), "BonusNoise must be between 0 and 30.");
            _bonusNoise = value;
        }
    }


    public bool RandomBiomes { get; set; } = true;
    public bool AddMountainEdges { get; set; } = false;
    public bool AddVegetation { get; set; } = true;
    public bool AddResources { get; set; } = true;
    public bool AddCenterLake { get; set; } = false;
    public bool AddCenterGradientLand { get; set; } = true;
    public bool GradientRoundEdges { get; set; } = true;
    public bool SquareEdges { get; set; } = false;
    public bool RingTreeEffect { get; set; } = true;
    public bool LowGround { get; set; } = false;
    public bool HighGround { get; set; } = false;
    public bool RemoveMountains { get; set; } = false;
    public bool ForbiddenKnowledgeStart { get; set; } = false;
}