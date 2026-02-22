using System;
using System.Collections.Generic;
using System.Reflection;
using WorldLib.Utils;

namespace WorldLib.Structs.Worlds;

extern alias GameAsm;

public sealed class WorldLaws
    : AbstractionOf<GameAsm::WorldLaws>
{
    private static readonly Type LawLibraryType = typeof(GameAsm::WorldLawLibrary);
    private static readonly Dictionary<string, FieldInfo> LawLibraryFieldCache = new();

    internal WorldLaws() : base(GameAsm::World.world.world_laws)
    {
    }

    #region Other

    /**
     * Monoliths can evolve and mutate nearby creatures.
     */
    public bool EvolutionEvents
    {
        get => Get("world_law_evolution_events");
        set => Set("world_law_evolution_events", value);
    }

    #endregion

    private bool Get(string k)
    {
        return Base.dict.TryGetValue(k, out var option)
            ? option.boolVal
            : throw new KeyNotFoundException($"World law '{k}' not found.");
    }

    private void Set(string k, bool value)
    {
        if (!Base.dict.TryGetValue(k, out var option))
            throw new KeyNotFoundException($"World law '{k}' not found.");

        var lastValue = option.boolVal;
        if (value == lastValue)
            return;

        option.boolVal = value;

        if (value)
        {
            var asset = GetAsset(k);
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

    /**
     * Limit maximum population in settlements to 100
     */
    public bool OneHundredPeople
    {
        get => Get("world_law_civ_limit_population_100");
        set => Set("world_law_civ_limit_population_100", value);
    }

    /**
     * Damage to world from explosions, fires and disasters is disabled.
     */
    public bool GaiasCovenant
    {
        get => Get("world_law_gaias_covenant");
        set => Set("world_law_gaias_covenant", value);
    }

    #endregion

    #region Diplomacy

    /**
     * Alliances forged, betrayals planned, and wars will erupt.
     */
    public bool Diplomacy
    {
        get => Get("world_law_diplomacy");
        set => Set("world_law_diplomacy", value);
    }

    /**
     * Clan members can start rites based on their religion.
     */
    public bool Rites
    {
        get => Get("world_law_rites");
        set => Set("world_law_rites", value);
    }

    /**
     * Cities can rebel when they have low loyalty. Only works if diplomacy is enabled.
     */
    public bool Rebellions
    {
        get => Get("world_law_rebellions");
        set => Set("world_law_rebellions", value);
    }

    /**
     * Rulers can steal the borders of other cities.
     */
    public bool BorderStealing
    {
        get => Get("world_law_border_stealing");
        set => Set("world_law_border_stealing", value);
    }

    #endregion

    #region Civilisations

    /**
     * Religions, languages and cultures go off the rails with random traits.
     */
    public bool GlitchedNoosphere
    {
        get => Get("world_law_glitched_noosphere");
        set => Set("world_law_glitched_noosphere", value);
    }

    /**
     * Structures spread their species' natural biome over time.
     */
    public bool Terramorphing
    {
        get => Get("world_law_terramorphing");
        set => Set("world_law_terramorphing", value);
    }

    /**
     * Kingdoms will send settlers to establish new villagers.
     */
    public bool KingdomExpansion
    {
        get => Get("world_law_kingdom_expansion");
        set => Set("world_law_kingdom_expansion", value);
    }

    /**
     * Villagers will also participate in same-race wars.
     */
    public bool AngryVillagers
    {
        get => Get("world_law_angry_civilians");
        set => Set("world_law_angry_civilians", value);
    }

    /**
     * Children will be born in villages.
     */
    public bool CivBabies
    {
        get => Get("world_law_civ_babies");
        set => Set("world_law_civ_babies", value);
    }

    /**
     * Unreasonably attractive migrants may appear near bonfires when the population drops.
     */
    public bool HandsomeMigrants
    {
        get => Get("world_law_civ_migrants");
        set => Set("world_law_civ_migrants", value);
    }

    /**
     * Civilisations have armies.
     */
    public bool Armies
    {
        get => Get("world_law_civ_army");
        set => Set("world_law_civ_army", value);
    }

    #endregion

    #region Units

    /**
     * New subspecies are born with a tangled mess of random genes.
     */
    public bool GeneSpaghetti
    {
        get => Get("world_law_gene_spaghetti");
        set => Set("world_law_gene_spaghetti", value);
    }

    /**
     * New subspecies are created with a mix of random traits.
     */
    public bool MutantBox
    {
        get => Get("world_law_mutant_box");
        set => Set("world_law_mutant_box", value);
    }

    /**
     * Everyone needs food to live.
     */
    public bool Hunger
    {
        get => Get("world_law_hunger");
        set => Set("world_law_hunger", value);
    }

    /**
     * Creatures can die from old age.
     */
    public bool OldAge
    {
        get => Get("world_law_old_age");
        set => Set("world_law_old_age", value);
    }

    #endregion

    #region Mobs

    /**
     * Animals and other creatures will not attack anyone.
     */
    public bool PeacefulMonsters
    {
        get => Get("world_law_peaceful_monsters");
        set => Set("world_law_peaceful_monsters", value);
    }

    /**
     * Baby animals will be born.
     */
    public bool AnimalBabies
    {
        get => Get("world_law_animals_babies");
        set => Set("world_law_animals_babies", value);
    }

    /**
     * Creep tiles like tumor, pumpkin, biomass, etc - will stay even after base structure is gone.
     */
    public bool ForeverCreep
    {
        get => Get("world_law_forever_tumor_creep");
        set => Set("world_law_forever_tumor_creep", value);
    }

    #endregion

    #region Spawn

    /**
     * Sapient beings now fall gently via the Clouds of Life.
     */
    public bool DropOfThoughts
    {
        get => Get("world_law_drop_of_thoughts");
        set => Set("world_law_drop_of_thoughts", value);
    }

    /**
     * Animals can randomly appear in your world.
     */
    public bool AnimalSpawn
    {
        get => Get("world_law_animals_spawn");
        set => Set("world_law_animals_spawn", value);
    }

    /**
     * Clouds carry pollen, spores and aeroplankton, seeding life across the land.
     */
    public bool CloudsOfLife
    {
        get => Get("world_law_clouds");
        set => Set("world_law_clouds", value);
    }

    #endregion

    #region Nature

    /**
     * The world is densely packed with trees, plants and fungi.
     */
    public bool HighFloraDensity
    {
        get => Get("world_law_spread_density_high");
        set => Set("world_law_spread_density_high", value);
    }

    /**
     * Trees and plants will appear randomly in biomes.
     */
    public bool RandomSeeds
    {
        get => Get("world_law_vegetation_random_seeds");
        set => Set("world_law_vegetation_random_seeds", value);
    }

    /**
     * Plants and trees can spread in any biome.
     */
    public bool RootsWithoutBorders
    {
        get => Get("world_law_roots_without_borders");
        set => Set("world_law_roots_without_borders", value);
    }

    /**
     * Minerals will spawn in biomes randomly from the underground.
     */
    public bool Minerals
    {
        get => Get("world_law_grow_minerals");
        set => Set("world_law_grow_minerals", value);
    }

    /**
     * Tiles have a chance to turn into sand when near water.
     */
    public bool Erosion
    {
        get => Get("world_law_erosion");
        set => Set("world_law_erosion", value);
    }

    #endregion

    #region Trees

    /**
     * Allow trees to naturally grow and spread.
     */
    public bool TreeGrowth
    {
        get => Get("world_law_spread_trees");
        set => Set("world_law_spread_trees", value);
    }

    /**
     * Trees grow and spread at increased speeds.
     */
    public bool FastTreeGrowth
    {
        get => Get("world_law_spread_fast_trees");
        set => Set("world_law_spread_fast_trees", value);
    }

    /**
     * Trees reduce the movement speed of nearby creatures.
     */
    public bool Entanglewood
    {
        get => Get("world_law_entanglewood");
        set => Set("world_law_entanglewood", value);
    }

    /**
     * Trees have a change to retaliate when damaged.
     */
    public bool BarkBitesBack
    {
        get => Get("world_law_bark_bites_back");
        set => Set("world_law_bark_bites_back", value);
    }

    #endregion

    #region Plants

    /**
     * Allow plants to naturally grow and spread.
     */
    public bool PlantGrowth
    {
        get => Get("world_law_spread_plants");
        set => Set("world_law_spread_plants", value);
    }

    /**
     * Plants multiply and expand at maximum speed.
     */
    public bool FastPlantGrowth
    {
        get => Get("world_law_spread_fast_plants");
        set => Set("world_law_spread_fast_plants", value);
    }

    /**
     * Plants will irritate or distract nearby creatures.
     */
    public bool PlantsTickles
    {
        get => Get("world_law_plants_tickles");
        set => Set("world_law_plants_tickles", value);
    }

    /**
     * Roots occasionally trip passing creatures.
     */
    public bool RootPranks
    {
        get => Get("world_law_root_pranks");
        set => Set("world_law_root_pranks", value);
    }

    /**
     * Flowers release sleep-inducing pollen when disturbed.
     */
    public bool NectarNap
    {
        get => Get("world_law_nectar_nap");
        set => Set("world_law_nectar_nap", value);
    }

    #endregion

    #region Fungi

    /**
     * Allow fungal growth in this world
     */
    public bool FungiGrowth
    {
        get => Get("world_law_spread_fungi");
        set => Set("world_law_spread_fungi", value);
    }

    /**
     * Fungi grow at an incredible rate.
     */
    public bool FastFungiGrowth
    {
        get => Get("world_law_spread_fast_fungi");
        set => Set("world_law_spread_fast_fungi", value);
    }

    /**
     * Mushrooms explode when disturbed.
     */
    public bool ExplodingMushrooms
    {
        get => Get("world_law_exploding_mushrooms");
        set => Set("world_law_exploding_mushrooms", value);
    }

    #endregion

    #region Biomes

    /**
     * Grass will grow by itself.
     */
    public bool GrowGrass
    {
        get => Get("world_law_grow_grass");
        set => Set("world_law_grow_grass", value);
    }

    /**
     * Biomes will try to overgrow other biomes.
     */
    public bool BiomeOvergrowth
    {
        get => Get("world_law_biome_overgrowth");
        set => Set("world_law_biome_overgrowth", value);
    }

    #endregion

    #region Weather

    /**
     * Lava stays forever and does not cool off into rock.
     */
    public bool EternalLava
    {
        get => Get("world_law_forever_lava");
        set => Set("world_law_forever_lava", value);
    }

    /**
     * Ice and snow stay forever and do not melt.
     */
    public bool ForeverCold
    {
        get => Get("world_law_forever_cold");
        set => Set("world_law_forever_cold", value);
    }

    #endregion

    #region Disasters

    /**
     * Natural disasters will happen randomly from time to time.
     */
    public bool NaturalDisasters
    {
        get => Get("world_law_disasters_nature");
        set => Set("world_law_disasters_nature", value);
    }

    /**
     * Invasions and other fun stuff may happen randomly from time to time.
     */
    public bool OtherDisasters
    {
        get => Get("world_law_disasters_other");
        set => Set("world_law_disasters_other", value);
    }

    /**
     * A swarm of rats can spark a devastating plague.
     */
    public bool RatKing
    {
        get => Get("world_law_rat_plague");
        set => Set("world_law_rat_plague", value);
    }

    #endregion
}