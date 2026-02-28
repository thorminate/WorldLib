using Newtonsoft.Json;
using WorldLib.Models.Ages;
using WorldLib.Models.History;
using WorldLib.Models.Laws;
using WorldLib.Models.Options;
using WorldLib.Models.Time;

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

    /// <summary>
    ///     Saves the game into a slot as if you
    /// </summary>
    /// <param name="options"></param>
    public static void Save(WorldSavingOptions options)
    {
        GameAsm::SaveManager.setCurrentSlot(options.Slot);

        GameAsm::SaveManager.saveWorldToDirectory(GameAsm::SaveManager.currentSavePath);
    }

    /// <inheritdoc cref="WorldLaws" />
    /// <returns>
    ///     A <see cref="WorldLaws" /> instance.
    /// </returns>
    public static WorldLaws Laws()
    {
        return new WorldLaws();
    }

    /// <summary>
    ///     Provides access to all world ages in the game.
    /// </summary>
    /// <returns>
    ///     A <see cref="WorldAges" /> instance.
    /// </returns>
    public static WorldAges Ages()
    {
        return new WorldAges();
    }

    /// <summary>
    ///     Provides access to the history log of the game (otherwise known as the WorldLog)
    /// </summary>
    /// <returns>
    ///     A <see cref="WorldHistory" /> instance.
    /// </returns>
    public static WorldHistory History()
    {
        return new WorldHistory();
    }

    #region Info

    private static GameAsm::MapStats MapStats => GameAsm::World.world.map_stats;

    public static double WorldTimeRaw
    {
        get => MapStats.world_time;
        set => MapStats.world_time = value;
    }

    public static double ExplodingMushroomsEnabledAtRaw
    {
        get => MapStats.exploding_mushrooms_enabled_at;
        set => MapStats.exploding_mushrooms_enabled_at = value;
    }

    /// <summary>
    ///     Represents the mood that the player is in. It is mostly used to give a color to certain map-specific
    ///     elements.
    /// </summary>
    // TODO: Abstract ArchitectMood to a separate class. Rename ArchitectMood to PlayerMood.
    public static GameAsm::ArchitectMood PlayerMood => MapStats.getArchitectMood();

    /// <summary>
    ///     Represents the current time of the world.
    /// </summary>
    /// <remarks>
    ///     This object updates dynamically when the time changes.
    /// </remarks>
    public static WorldTime WorldTime => new(() => MapStats.world_time);

    public static WorldTime ExplodingMushroomsEnabledAt => new(() => MapStats.exploding_mushrooms_enabled_at);

    public static string Name
    {
        get => MapStats.name;
        set => MapStats.name = value;
    }

    public static string Description
    {
        get => MapStats.description;
        set => MapStats.description = value;
    }

    public static string PlayerName
    {
        get => MapStats.player_name;
        set => MapStats.player_name = value;
    }

    public static string PlayerMoodId
    {
        get => MapStats.player_mood;
        set => MapStats.player_mood = value;
    }

    public static long AlliancesDissolved
    {
        get => MapStats.alliancesDissolved;
        set => MapStats.alliancesDissolved = value;
    }

    public static long AlliancesMade
    {
        get => MapStats.alliancesMade;
        set => MapStats.alliancesMade = value;
    }

    public static long ArmiesCreated
    {
        get => MapStats.armiesCreated;
        set => MapStats.armiesCreated = value;
    }

    public static long ArmiesDestroyed
    {
        get => MapStats.armiesDestroyed;
        set => MapStats.armiesDestroyed = value;
    }

    public static long BooksBurnt
    {
        get => MapStats.booksBurnt;
        set => MapStats.booksBurnt = value;
    }

    public static long BooksRead
    {
        get => MapStats.booksRead;
        set => MapStats.booksRead = value;
    }

    public static long BooksWritten
    {
        get => MapStats.booksWritten;
        set => MapStats.booksWritten = value;
    }

    public static long CitiesConquered
    {
        get => MapStats.citiesConquered;
        set => MapStats.citiesConquered = value;
    }

    public static long CitiesCreated
    {
        get => MapStats.citiesCreated;
        set => MapStats.citiesCreated = value;
    }

    public static long CitiesDestroyed
    {
        get => MapStats.citiesDestroyed;
        set => MapStats.citiesDestroyed = value;
    }

    public static long CitiesRebelled
    {
        get => MapStats.citiesRebelled;
        set => MapStats.citiesRebelled = value;
    }

    public static long ClansCreated
    {
        get => MapStats.clansCreated;
        set => MapStats.clansCreated = value;
    }

    public static long ClansDestroyed
    {
        get => MapStats.clansDestroyed;
        set => MapStats.clansDestroyed = value;
    }

    public static long CreaturesBorn
    {
        get => MapStats.creaturesBorn;
        set => MapStats.creaturesBorn = value;
    }

    public static long CreaturesCreated
    {
        get => MapStats.creaturesCreated;
        set => MapStats.creaturesCreated = value;
    }

    public static long CulturesCreated
    {
        get => MapStats.culturesCreated;
        set => MapStats.culturesCreated = value;
    }

    public static long CulturesForgotten
    {
        get => MapStats.culturesForgotten;
        set => MapStats.culturesForgotten = value;
    }

    public static long CurrentHouses
    {
        get => MapStats.current_houses;
        set => MapStats.current_houses = value;
    }

    public static long CurrentInfected
    {
        get => MapStats.current_infected;
        set => MapStats.current_infected = value;
    }

    public static long CurrentInfectedPlague
    {
        get => MapStats.current_infected_plague;
        set => MapStats.current_infected_plague = value;
    }

    public static long CurrentMobs
    {
        get => MapStats.current_mobs;
        set => MapStats.current_mobs = value;
    }

    public static long CurrentVegetation
    {
        get => MapStats.current_vegetation;
        set => MapStats.current_vegetation = value;
    }

    public static long Deaths
    {
        get => MapStats.deaths;
        set => MapStats.deaths = value;
    }

    public static long DeathsAcid
    {
        get => MapStats.deaths_acid;
        set => MapStats.deaths_acid = value;
    }

    public static long DeathsAge
    {
        get => MapStats.deaths_age;
        set => MapStats.deaths_age = value;
    }

    public static long DeathsDivine
    {
        get => MapStats.deaths_divine;
        set => MapStats.deaths_divine = value;
    }

    public static long DeathsDrowning
    {
        get => MapStats.deaths_drowning;
        set => MapStats.deaths_drowning = value;
    }

    public static long DeathsEaten
    {
        get => MapStats.deaths_eaten;
        set => MapStats.deaths_eaten = value;
    }

    public static long DeathsExplosion
    {
        get => MapStats.deaths_explosion;
        set => MapStats.deaths_explosion = value;
    }

    public static long DeathsFire
    {
        get => MapStats.deaths_fire;
        set => MapStats.deaths_fire = value;
    }

    public static long DeathsGravity
    {
        get => MapStats.deaths_gravity;
        set => MapStats.deaths_gravity = value;
    }

    public static long DeathsHunger
    {
        get => MapStats.deaths_hunger;
        set => MapStats.deaths_hunger = value;
    }

    public static long DeathsInfection
    {
        get => MapStats.deaths_infection;
        set => MapStats.deaths_infection = value;
    }

    public static long DeathsOther
    {
        get => MapStats.deaths_other;
        set => MapStats.deaths_other = value;
    }

    public static long DeathsPlague
    {
        get => MapStats.deaths_plague;
        set => MapStats.deaths_plague = value;
    }

    public static long DeathsPoison
    {
        get => MapStats.deaths_poison;
        set => MapStats.deaths_poison = value;
    }

    public static long DeathsSmile
    {
        get => MapStats.deaths_smile;
        set => MapStats.deaths_smile = value;
    }

    public static long DeathsTumor
    {
        get => MapStats.deaths_tumor;
        set => MapStats.deaths_tumor = value;
    }

    public static long DeathsWater
    {
        get => MapStats.deaths_water;
        set => MapStats.deaths_water = value;
    }

    public static long DeathsWeapon
    {
        get => MapStats.deaths_weapon;
        set => MapStats.deaths_weapon = value;
    }

    public static long Evolutions
    {
        get => MapStats.evolutions;
        set => MapStats.evolutions = value;
    }

    public static long FamiliesCreated
    {
        get => MapStats.familiesCreated;
        set => MapStats.familiesCreated = value;
    }

    public static long FamiliesDestroyed
    {
        get => MapStats.familiesDestroyed;
        set => MapStats.familiesDestroyed = value;
    }

    public static long HousesBuilt
    {
        get => MapStats.housesBuilt;
        set => MapStats.housesBuilt = value;
    }

    public static long HousesDestroyed
    {
        get => MapStats.housesDestroyed;
        set => MapStats.housesDestroyed = value;
    }

    public static long KingdomsCreated
    {
        get => MapStats.kingdomsCreated;
        set => MapStats.kingdomsCreated = value;
    }

    public static long KingdomsDestroyed
    {
        get => MapStats.kingdomsDestroyed;
        set => MapStats.kingdomsDestroyed = value;
    }

    public static long LanguagesCreated
    {
        get => MapStats.languagesCreated;
        set => MapStats.languagesCreated = value;
    }

    public static long LanguagesForgotten
    {
        get => MapStats.languagesForgotten;
        set => MapStats.languagesForgotten = value;
    }

    public static long LifeDna
    {
        get => MapStats.life_dna;
        set => MapStats.life_dna = value;
    }

    public static long Metamorphosis
    {
        get => MapStats.metamorphosis;
        set => MapStats.metamorphosis = value;
    }

    public static long PeacesMade
    {
        get => MapStats.peacesMade;
        set => MapStats.peacesMade = value;
    }

    public static long PlotsForgotten
    {
        get => MapStats.plotsForgotten;
        set => MapStats.plotsForgotten = value;
    }

    public static long PlotsStarted
    {
        get => MapStats.plotsStarted;
        set => MapStats.plotsStarted = value;
    }

    public static long PlotsSucceeded
    {
        get => MapStats.plotsSucceeded;
        set => MapStats.plotsSucceeded = value;
    }

    public static long Population
    {
        get => MapStats.population;
        set => MapStats.population = value;
    }

    public static long ReligionsCreated
    {
        get => MapStats.religionsCreated;
        set => MapStats.religionsCreated = value;
    }

    public static long ReligionsForgotten
    {
        get => MapStats.religionsForgotten;
        set => MapStats.religionsForgotten = value;
    }

    public static long SubspeciesCreated
    {
        get => MapStats.subspeciesCreated;
        set => MapStats.subspeciesCreated = value;
    }

    public static long SubspeciesExtinct
    {
        get => MapStats.subspeciesExtinct;
        set => MapStats.subspeciesExtinct = value;
    }

    public static long WarsStarted
    {
        get => MapStats.warsStarted;
        set => MapStats.warsStarted = value;
    }

    #endregion
}