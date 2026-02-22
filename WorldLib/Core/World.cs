using Newtonsoft.Json;
using WorldLib.Structs.Worlds;

namespace WorldLib.Core;

extern alias GameAsm;

public static class World
{
    public static void Create(WorldCreationOptions options)
    {
        GameAsm::Config.customMapSize = options.Size.ToString().ToLower();

        var values = GameAsm::MapGenerator.template.values;

        Plugin.Logger.LogInfo("Creating world...");
        Plugin.Logger.LogInfo(JsonConvert.SerializeObject(values, Formatting.Indented));
        Plugin.Logger.LogInfo(JsonConvert.SerializeObject(options, Formatting.Indented));

        values.perlin_scale_stage_1 = options.MainNoise;
        values.perlin_scale_stage_2 = options.DetailNoise;
        values.perlin_scale_stage_3 = options.BonusNoise;
        values.random_shapes_amount = options.RandomShapes;

        values.square_edges = options.SquareEdges;
        values.gradient_round_edges = options.GradientRoundEdges;
        values.add_center_gradient_land = options.AddCenterGradientLand;
        values.add_center_lake = options.AddCenterLake;
        values.ring_effect = options.RingTreeEffect;
        values.add_vegetation = options.AddVegetation;
        values.add_resources = options.AddResources;
        values.add_mountain_edges = options.AddMountainEdges;
        values.random_biomes = options.RandomBiomes;
        values.remove_mountains = options.RemoveMountains;
        values.forbidden_knowledge_start = options.ForbiddenKnowledgeStart;
        values.low_ground = options.LowGround;
        values.high_ground = options.HighGround;

        GameAsm::MapBox.instance.clickGenerateNewMap();
    }

    public static void SetSpeed(WorldSetSpeedOptions options)
    {
        if (options.Speed == WorldSetSpeedOptions.WorldSpeeds.Pause)
        {
            GameAsm::Config.paused = true;
            return;
        }

        GameAsm::Config.paused = false;
        GameAsm::Config.setWorldSpeed(options.Speed.ToString().ToLower());
    }

    public static void Save(WorldSavingOptions options)
    {
        GameAsm::SaveManager.setCurrentSlot(options.Slot);

        GameAsm::SaveManager.saveWorldToDirectory(GameAsm::SaveManager.currentSavePath);
    }

    public static WorldInfo Info()
    {
        return new WorldInfo();
    }

    public static WorldLaws Laws()
    {
        return new WorldLaws();
    }

    public static WorldAges Ages()
    {
        return new WorldAges();
    }
}