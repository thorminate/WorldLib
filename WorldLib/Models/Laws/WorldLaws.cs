using System;
using System.Collections.Generic;
using System.Reflection;
using WorldLib.Utils;

namespace WorldLib.Models.Laws;

extern alias GameAsm;

/// <summary>
///     Provides access to the game's world law system.
/// </summary>
/// <remarks>
///     Changes to properties immediately propagate to the underlying game engine state,
///     so external caching or copying is generally unnecessary.
/// </remarks>
public sealed class WorldLaws
    : AbstractionOf<GameAsm::WorldLaws>
{
    private static readonly Type LawLibraryType = typeof(GameAsm::WorldLawLibrary);
    private static readonly Dictionary<string, FieldInfo> LawLibraryFieldCache = new();

    internal WorldLaws() : base(() => GameAsm::World.world.world_laws)
    {
    }

    #region Other

    /// <summary>
    ///     Allows monoliths to evolve and mutate nearby creatures.
    /// </summary>
    public bool EvolutionEvents
    {
        get => Get("world_law_evolution_events");
        set => Set("world_law_evolution_events", value);
    }

    #endregion

    /// <summary>
    ///     Retrieves the value of a world law using its internal key.
    /// </summary>
    /// <param name="key">The internal world law identifier.</param>
    /// <returns>
    ///     <see langword="true" /> if the law is enabled; otherwise <see langword="false" />.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if the specified world law key does not exist.
    /// </exception>
    private bool Get(string key)
    {
        return Base.dict.TryGetValue(key, out var option)
            ? option.boolVal
            : throw new KeyNotFoundException($"World law '{key}' not found.");
    }

    /// <summary>
    ///     Sets the value of a world law using its internal key.
    /// </summary>
    /// <param name="key">The internal world law identifier.</param>
    /// <param name="value">
    ///     <see langword="true" /> to enable the law;
    ///     <see langword="false" /> to disable it.
    /// </param>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if the specified world law key does not exist.
    /// </exception>
    private void Set(string key, bool value)
    {
        if (!Base.dict.TryGetValue(key, out var option))
            throw new KeyNotFoundException($"World law '{key}' not found.");

        bool lastValue = option.boolVal;
        if (value == lastValue)
            return;

        option.boolVal = value;

        if (value)
        {
            var asset = GetAsset(key);
            asset.on_state_enabled?.Invoke(option);
        }

        Base.updateCaches();
        option.on_switch?.Invoke(option);

        var currentWindow = GameAsm::ScrollWindow.getCurrentWindow();
        if (currentWindow == null) return;

        var editorTransform =
            currentWindow.transform.Find(
                "Background/Scroll View/Viewport/Content/content_main");
        if (editorTransform == null) return;
        var editor = editorTransform.GetComponent<GameAsm::WorldLawsEditor>();
        if (editor == null) return;

        editor.updateButtons();
    }

    private static GameAsm::WorldLawAsset GetAsset(string k)
    {
        if (LawLibraryFieldCache.TryGetValue(k, out var field))
            return (GameAsm::WorldLawAsset)field.GetValue(null);

        field = LawLibraryType.GetField(k, BindingFlags.Static | BindingFlags.Public)
                ?? throw new Exception($"WorldLawLibrary does not contain a field '{k}'");

        if (field.GetValue(null) is not GameAsm::WorldLawAsset asset)
            throw new Exception($"WorldLawLibrary field '{k}' is not a WorldLawAsset");

        LawLibraryFieldCache[k] = field;
        return asset;
    }

    #region Harmony

    /// <summary>
    ///     Limits the maximum population in settlements to 100 inhabitants.
    /// </summary>
    public bool OneHundredPeople
    {
        get => Get("world_law_civ_limit_population_100");
        set => Set("world_law_civ_limit_population_100", value);
    }

    /// <summary>
    ///     Prevents environmental destruction from explosions, fires, and disasters.
    /// </summary>
    public bool GaiasCovenant
    {
        get => Get("world_law_gaias_covenant");
        set => Set("world_law_gaias_covenant", value);
    }

    #endregion

    #region Diplomacy

    /// <summary>
    ///     Alliances forged, betrayals planned, and wars will erupt.
    /// </summary>
    public bool Diplomacy
    {
        get => Get("world_law_diplomacy");
        set => Set("world_law_diplomacy", value);
    }

    /// <summary>
    ///     Clan members can start rites based on their religion.
    /// </summary>
    public bool Rites
    {
        get => Get("world_law_rites");
        set => Set("world_law_rites", value);
    }

    /// <summary>
    ///     Cities can rebel when they have low loyalty. Only works if diplomacy is enabled.
    /// </summary>
    public bool Rebellions
    {
        get => Get("world_law_rebellions");
        set => Set("world_law_rebellions", value);
    }

    /// <summary>
    ///     Rulers can steal the borders of other cities.
    /// </summary>
    public bool BorderStealing
    {
        get => Get("world_law_border_stealing");
        set => Set("world_law_border_stealing", value);
    }

    #endregion

    #region Civilisations

    /// <summary>
    ///     Religions, languages and cultures go off the rails with random traits.
    /// </summary>
    public bool GlitchedNoosphere
    {
        get => Get("world_law_glitched_noosphere");
        set => Set("world_law_glitched_noosphere", value);
    }

    /// <summary>
    ///     Structures spread their species' natural biome over time.
    /// </summary>
    public bool Terramorphing
    {
        get => Get("world_law_terramorphing");
        set => Set("world_law_terramorphing", value);
    }

    /// <summary>
    ///     Kingdoms will send settlers to establish new villagers.
    /// </summary>
    public bool KingdomExpansion
    {
        get => Get("world_law_kingdom_expansion");
        set => Set("world_law_kingdom_expansion", value);
    }

    /// <summary>
    ///     Villagers will also participate in same-race wars.
    /// </summary>
    public bool AngryVillagers
    {
        get => Get("world_law_angry_civilians");
        set => Set("world_law_angry_civilians", value);
    }

    /// <summary>
    ///     Children will be born in villages.
    /// </summary>
    public bool CivBabies
    {
        get => Get("world_law_civ_babies");
        set => Set("world_law_civ_babies", value);
    }

    /// <summary>
    ///     Unreasonably attractive migrants may appear near bonfires when the population drops.
    /// </summary>
    public bool HandsomeMigrants
    {
        get => Get("world_law_civ_migrants");
        set => Set("world_law_civ_migrants", value);
    }

    /// <summary>
    ///     Civilisations have armies.
    /// </summary>
    public bool Armies
    {
        get => Get("world_law_civ_army");
        set => Set("world_law_civ_army", value);
    }

    #endregion

    #region Units

    /// <summary>
    ///     New subspecies are born with a tangled mess of random genes.
    /// </summary>
    public bool GeneSpaghetti
    {
        get => Get("world_law_gene_spaghetti");
        set => Set("world_law_gene_spaghetti", value);
    }

    /// <summary>
    ///     New subspecies are created with a mix of random traits.
    /// </summary>
    public bool MutantBox
    {
        get => Get("world_law_mutant_box");
        set => Set("world_law_mutant_box", value);
    }

    /// <summary>
    ///     Everyone needs food to live.
    /// </summary>
    public bool Hunger
    {
        get => Get("world_law_hunger");
        set => Set("world_law_hunger", value);
    }

    /// <summary>
    ///     Creatures can die from old age.
    /// </summary>
    public bool OldAge
    {
        get => Get("world_law_old_age");
        set => Set("world_law_old_age", value);
    }

    #endregion

    #region Mobs

    /// <summary>
    ///     Animals and other creatures will not attack anyone.
    /// </summary>
    public bool PeacefulMonsters
    {
        get => Get("world_law_peaceful_monsters");
        set => Set("world_law_peaceful_monsters", value);
    }

    /// <summary>
    ///     Baby animals will be born.
    /// </summary>
    public bool AnimalBabies
    {
        get => Get("world_law_animals_babies");
        set => Set("world_law_animals_babies", value);
    }

    /// <summary>
    ///     Creep tiles like tumor, pumpkin, biomass, etc - will stay even after base structure is gone.
    /// </summary>
    public bool ForeverCreep
    {
        get => Get("world_law_forever_tumor_creep");
        set => Set("world_law_forever_tumor_creep", value);
    }

    #endregion

    #region Spawn

    /// <summary>
    ///     Sapient beings now fall gently via the Clouds of Life.
    /// </summary>
    public bool DropOfThoughts
    {
        get => Get("world_law_drop_of_thoughts");
        set => Set("world_law_drop_of_thoughts", value);
    }

    /// <summary>
    ///     Animals can randomly appear in your world.
    /// </summary>
    public bool AnimalSpawn
    {
        get => Get("world_law_animals_spawn");
        set => Set("world_law_animals_spawn", value);
    }

    /// <summary>
    ///     Clouds carry pollen, spores and aeroplankton, seeding life across the land.
    /// </summary>
    public bool CloudsOfLife
    {
        get => Get("world_law_clouds");
        set => Set("world_law_clouds", value);
    }

    #endregion

    #region Nature

    /// <summary>
    ///     The world is densely packed with trees, plants and fungi.
    /// </summary>
    public bool HighFloraDensity
    {
        get => Get("world_law_spread_density_high");
        set => Set("world_law_spread_density_high", value);
    }

    /// <summary>
    ///     Trees and plants will appear randomly in biomes.
    /// </summary>
    public bool RandomSeeds
    {
        get => Get("world_law_vegetation_random_seeds");
        set => Set("world_law_vegetation_random_seeds", value);
    }

    /// <summary>
    ///     Plants and trees can spread in any biome.
    /// </summary>
    public bool RootsWithoutBorders
    {
        get => Get("world_law_roots_without_borders");
        set => Set("world_law_roots_without_borders", value);
    }

    /// <summary>
    ///     Minerals will spawn in biomes randomly from the underground.
    /// </summary>
    public bool Minerals
    {
        get => Get("world_law_grow_minerals");
        set => Set("world_law_grow_minerals", value);
    }

    /// <summary>
    ///     Tiles have a chance to turn into sand when near water.
    /// </summary>
    public bool Erosion
    {
        get => Get("world_law_erosion");
        set => Set("world_law_erosion", value);
    }

    #endregion

    #region Trees

    /// <summary>
    ///     Allow trees to naturally grow and spread.
    /// </summary>
    public bool TreeGrowth
    {
        get => Get("world_law_spread_trees");
        set => Set("world_law_spread_trees", value);
    }

    /// <summary>
    ///     Trees grow and spread at increased speeds.
    /// </summary>
    public bool FastTreeGrowth
    {
        get => Get("world_law_spread_fast_trees");
        set => Set("world_law_spread_fast_trees", value);
    }

    /// <summary>
    ///     Trees reduce the movement speed of nearby creatures.
    /// </summary>
    public bool Entanglewood
    {
        get => Get("world_law_entanglewood");
        set => Set("world_law_entanglewood", value);
    }

    /// <summary>
    ///     Trees have a change to retaliate when damaged.
    /// </summary>
    public bool BarkBitesBack
    {
        get => Get("world_law_bark_bites_back");
        set => Set("world_law_bark_bites_back", value);
    }

    #endregion

    #region Plants

    /// <summary>
    ///     Allow plants to naturally grow and spread.
    /// </summary>
    public bool PlantGrowth
    {
        get => Get("world_law_spread_plants");
        set => Set("world_law_spread_plants", value);
    }

    /// <summary>
    ///     Plants multiply and expand at maximum speed.
    /// </summary>
    public bool FastPlantGrowth
    {
        get => Get("world_law_spread_fast_plants");
        set => Set("world_law_spread_fast_plants", value);
    }

    /// <summary>
    ///     Plants will irritate or distract nearby creatures.
    /// </summary>
    public bool PlantsTickles
    {
        get => Get("world_law_plants_tickles");
        set => Set("world_law_plants_tickles", value);
    }

    /// <summary>
    ///     Roots occasionally trip passing creatures.
    /// </summary>
    public bool RootPranks
    {
        get => Get("world_law_root_pranks");
        set => Set("world_law_root_pranks", value);
    }

    /// <summary>
    ///     Flowers release sleep-inducing pollen when disturbed.
    /// </summary>
    public bool NectarNap
    {
        get => Get("world_law_nectar_nap");
        set => Set("world_law_nectar_nap", value);
    }

    #endregion

    #region Fungi

    /// <summary>
    ///     Allow fungal growth in this world
    /// </summary>
    public bool FungiGrowth
    {
        get => Get("world_law_spread_fungi");
        set => Set("world_law_spread_fungi", value);
    }

    /// <summary>
    ///     Fungi grow at an incredible rate.
    /// </summary>
    public bool FastFungiGrowth
    {
        get => Get("world_law_spread_fast_fungi");
        set => Set("world_law_spread_fast_fungi", value);
    }

    /// <summary>
    ///     Mushrooms explode when disturbed.
    /// </summary>
    public bool ExplodingMushrooms
    {
        get => Get("world_law_exploding_mushrooms");
        set => Set("world_law_exploding_mushrooms", value);
    }

    #endregion

    #region Biomes

    /// <summary>
    ///     Grass will grow by itself.
    /// </summary>
    public bool GrowGrass
    {
        get => Get("world_law_grow_grass");
        set => Set("world_law_grow_grass", value);
    }

    /// <summary>
    ///     Biomes will try to overgrow other biomes.
    /// </summary>
    public bool BiomeOvergrowth
    {
        get => Get("world_law_biome_overgrowth");
        set => Set("world_law_biome_overgrowth", value);
    }

    #endregion

    #region Weather

    /// <summary>
    ///     Lava stays forever and does not cool off into rock.
    /// </summary>
    public bool EternalLava
    {
        get => Get("world_law_forever_lava");
        set => Set("world_law_forever_lava", value);
    }

    /// <summary>
    ///     Ice and snow stay forever and do not melt.
    /// </summary>
    public bool ForeverCold
    {
        get => Get("world_law_forever_cold");
        set => Set("world_law_forever_cold", value);
    }

    #endregion

    #region Disasters

    /// <summary>
    ///     Natural disasters will happen randomly from time to time.
    /// </summary>
    public bool NaturalDisasters
    {
        get => Get("world_law_disasters_nature");
        set => Set("world_law_disasters_nature", value);
    }

    /// <summary>
    ///     Invasions and other fun stuff may happen randomly from time to time.
    /// </summary>
    public bool OtherDisasters
    {
        get => Get("world_law_disasters_other");
        set => Set("world_law_disasters_other", value);
    }

    /// <summary>
    ///     A swarm of rats can spark a devastating plague.
    /// </summary>
    public bool RatKing
    {
        get => Get("world_law_rat_plague");
        set => Set("world_law_rat_plague", value);
    }

    #endregion
}