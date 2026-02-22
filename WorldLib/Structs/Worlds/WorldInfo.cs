using WorldLib.Utils;

namespace WorldLib.Structs.Worlds;

extern alias GameAsm;

public sealed class WorldInfo
    : AbstractionOf<GameAsm::MapStats>
{
    internal WorldInfo() : base(GameAsm::World.world.map_stats)
    {
    }

    #region Float

    public float WorldAgesSpeedMultiplier
    {
        get => Base.world_ages_speed_multiplier;
        set => Base.world_ages_speed_multiplier = value;
    }

    #endregion

    #region Bool

    public bool IsWorldAgesPaused
    {
        get => Base.is_world_ages_paused;
        set => Base.is_world_ages_paused = value;
    }

    #endregion

    #region Other

    // TODO: Abstract ArchitectMood to a separate class.
    public GameAsm::ArchitectMood ArchitectMood
    {
        get => Base.getArchitectMood();
        set => Base.player_mood = value.id;
    }

    #endregion

    #region Collection

    public string[] WorldAgesSlots
    {
        get => Base.world_ages_slots;
        set => Base.world_ages_slots = value;
    }

    #endregion

    #region Int

    public int HistoryCurrentYear
    {
        get => Base.history_current_year;
        set => Base.history_current_year = value;
    }

    public int WorldAgeSlotIndex
    {
        get => Base.world_age_slot_index;
        set => Base.world_age_slot_index = value;
    }

    #endregion

    #region String

    public string Name
    {
        get => Base.name;
        set => Base.name = value;
    }

    public string Description
    {
        get => Base.description;
        set => Base.description = value;
    }

    public string ArchitectName
    {
        get => Base.player_name;
        set => Base.player_name = value;
    }

    public string ArchitectMoodId
    {
        get => Base.player_mood;
        set => Base.player_mood = value;
    }

    public string WorldAgeId
    {
        get => Base.world_age_id;
        set => Base.world_age_id = value;
    }

    #endregion

    #region Long

    public long AlliancesDissolved
    {
        get => Base.alliancesDissolved;
        set => Base.alliancesDissolved = value;
    }

    public long AlliancesMade
    {
        get => Base.alliancesMade;
        set => Base.alliancesMade = value;
    }

    public long ArmiesCreated
    {
        get => Base.armiesCreated;
        set => Base.armiesCreated = value;
    }

    public long ArmiesDestroyed
    {
        get => Base.armiesDestroyed;
        set => Base.armiesDestroyed = value;
    }

    public long BooksBurnt
    {
        get => Base.booksBurnt;
        set => Base.booksBurnt = value;
    }

    public long BooksRead
    {
        get => Base.booksRead;
        set => Base.booksRead = value;
    }

    public long BooksWritten
    {
        get => Base.booksWritten;
        set => Base.booksWritten = value;
    }

    public long CitiesConquered
    {
        get => Base.citiesConquered;
        set => Base.citiesConquered = value;
    }

    public long CitiesCreated
    {
        get => Base.citiesCreated;
        set => Base.citiesCreated = value;
    }

    public long CitiesDestroyed
    {
        get => Base.citiesDestroyed;
        set => Base.citiesDestroyed = value;
    }

    public long CitiesRebelled
    {
        get => Base.citiesRebelled;
        set => Base.citiesRebelled = value;
    }

    public long ClansCreated
    {
        get => Base.clansCreated;
        set => Base.clansCreated = value;
    }

    public long ClansDestroyed
    {
        get => Base.clansDestroyed;
        set => Base.clansDestroyed = value;
    }

    public long CreaturesBorn
    {
        get => Base.creaturesBorn;
        set => Base.creaturesBorn = value;
    }

    public long CreaturesCreated
    {
        get => Base.creaturesCreated;
        set => Base.creaturesCreated = value;
    }

    public long CulturesCreated
    {
        get => Base.culturesCreated;
        set => Base.culturesCreated = value;
    }

    public long CulturesForgotten
    {
        get => Base.culturesForgotten;
        set => Base.culturesForgotten = value;
    }

    public long CurrentHouses
    {
        get => Base.current_houses;
        set => Base.current_houses = value;
    }

    public long CurrentInfected
    {
        get => Base.current_infected;
        set => Base.current_infected = value;
    }

    public long CurrentInfectedPlague
    {
        get => Base.current_infected_plague;
        set => Base.current_infected_plague = value;
    }

    public long CurrentMobs
    {
        get => Base.current_mobs;
        set => Base.current_mobs = value;
    }

    public long CurrentVegetation
    {
        get => Base.current_vegetation;
        set => Base.current_vegetation = value;
    }

    public long Deaths
    {
        get => Base.deaths;
        set => Base.deaths = value;
    }

    public long DeathsAcid
    {
        get => Base.deaths_acid;
        set => Base.deaths_acid = value;
    }

    public long DeathsAge
    {
        get => Base.deaths_age;
        set => Base.deaths_age = value;
    }

    public long DeathsDivine
    {
        get => Base.deaths_divine;
        set => Base.deaths_divine = value;
    }

    public long DeathsDrowning
    {
        get => Base.deaths_drowning;
        set => Base.deaths_drowning = value;
    }

    public long DeathsEaten
    {
        get => Base.deaths_eaten;
        set => Base.deaths_eaten = value;
    }

    public long DeathsExplosion
    {
        get => Base.deaths_explosion;
        set => Base.deaths_explosion = value;
    }

    public long DeathsFire
    {
        get => Base.deaths_fire;
        set => Base.deaths_fire = value;
    }

    public long DeathsGravity
    {
        get => Base.deaths_gravity;
        set => Base.deaths_gravity = value;
    }

    public long DeathsHunger
    {
        get => Base.deaths_hunger;
        set => Base.deaths_hunger = value;
    }

    public long DeathsInfection
    {
        get => Base.deaths_infection;
        set => Base.deaths_infection = value;
    }

    public long DeathsOther
    {
        get => Base.deaths_other;
        set => Base.deaths_other = value;
    }

    public long DeathsPlague
    {
        get => Base.deaths_plague;
        set => Base.deaths_plague = value;
    }

    public long DeathsPoison
    {
        get => Base.deaths_poison;
        set => Base.deaths_poison = value;
    }

    public long DeathsSmile
    {
        get => Base.deaths_smile;
        set => Base.deaths_smile = value;
    }

    public long DeathsTumor
    {
        get => Base.deaths_tumor;
        set => Base.deaths_tumor = value;
    }

    public long DeathsWater
    {
        get => Base.deaths_water;
        set => Base.deaths_water = value;
    }

    public long DeathsWeapon
    {
        get => Base.deaths_weapon;
        set => Base.deaths_weapon = value;
    }

    public long Evolutions
    {
        get => Base.evolutions;
        set => Base.evolutions = value;
    }

    public long FamiliesCreated
    {
        get => Base.familiesCreated;
        set => Base.familiesCreated = value;
    }

    public long FamiliesDestroyed
    {
        get => Base.familiesDestroyed;
        set => Base.familiesDestroyed = value;
    }

    public long HousesBuilt
    {
        get => Base.housesBuilt;
        set => Base.housesBuilt = value;
    }

    public long HousesDestroyed
    {
        get => Base.housesDestroyed;
        set => Base.housesDestroyed = value;
    }

    public long IDAlliance
    {
        get => Base.id_alliance;
        set => Base.id_alliance = value;
    }

    public long IDArmy
    {
        get => Base.id_army;
        set => Base.id_army = value;
    }

    public long IDBook
    {
        get => Base.id_book;
        set => Base.id_book = value;
    }

    public long IDBuilding
    {
        get => Base.id_building;
        set => Base.id_building = value;
    }

    public long IDCity
    {
        get => Base.id_city;
        set => Base.id_city = value;
    }

    public long IDClan
    {
        get => Base.id_clan;
        set => Base.id_clan = value;
    }

    public long IDCulture
    {
        get => Base.id_culture;
        set => Base.id_culture = value;
    }

    public long IDDiplomacy
    {
        get => Base.id_diplomacy;
        set => Base.id_diplomacy = value;
    }

    public long IDFamily
    {
        get => Base.id_family;
        set => Base.id_family = value;
    }

    public long IDItem
    {
        get => Base.id_item;
        set => Base.id_item = value;
    }

    public long IDKingdom
    {
        get => Base.id_kingdom;
        set => Base.id_kingdom = value;
    }

    public long IDLanguage
    {
        get => Base.id_language;
        set => Base.id_language = value;
    }

    public long IDPlot
    {
        get => Base.id_plot;
        set => Base.id_plot = value;
    }

    public long IDProjectile
    {
        get => Base.id_projectile;
        set => Base.id_projectile = value;
    }

    public long IDReligion
    {
        get => Base.id_religion;
        set => Base.id_religion = value;
    }

    public long IDStatus
    {
        get => Base.id_status;
        set => Base.id_status = value;
    }

    public long IDSubspecies
    {
        get => Base.id_subspecies;
        set => Base.id_subspecies = value;
    }

    public long IDUnit
    {
        get => Base.id_unit;
        set => Base.id_unit = value;
    }

    public long IDWar
    {
        get => Base.id_war;
        set => Base.id_war = value;
    }

    public long KingdomsCreated
    {
        get => Base.kingdomsCreated;
        set => Base.kingdomsCreated = value;
    }

    public long KingdomsDestroyed
    {
        get => Base.kingdomsDestroyed;
        set => Base.kingdomsDestroyed = value;
    }

    public long LanguagesCreated
    {
        get => Base.languagesCreated;
        set => Base.languagesCreated = value;
    }

    public long LanguagesForgotten
    {
        get => Base.languagesForgotten;
        set => Base.languagesForgotten = value;
    }

    public long LifeDna
    {
        get => Base.life_dna;
        set => Base.life_dna = value;
    }

    public long Metamorphosis
    {
        get => Base.metamorphosis;
        set => Base.metamorphosis = value;
    }

    public long PeacesMade
    {
        get => Base.peacesMade;
        set => Base.peacesMade = value;
    }

    public long PlotsForgotten
    {
        get => Base.plotsForgotten;
        set => Base.plotsForgotten = value;
    }

    public long PlotsStarted
    {
        get => Base.plotsStarted;
        set => Base.plotsStarted = value;
    }

    public long PlotsSucceeded
    {
        get => Base.plotsSucceeded;
        set => Base.plotsSucceeded = value;
    }

    public long Population
    {
        get => Base.population;
        set => Base.population = value;
    }

    public long ReligionsCreated
    {
        get => Base.religionsCreated;
        set => Base.religionsCreated = value;
    }

    public long ReligionsForgotten
    {
        get => Base.religionsForgotten;
        set => Base.religionsForgotten = value;
    }

    public long SubspeciesCreated
    {
        get => Base.subspeciesCreated;
        set => Base.subspeciesCreated = value;
    }

    public long SubspeciesExtinct
    {
        get => Base.subspeciesExtinct;
        set => Base.subspeciesExtinct = value;
    }

    public long WarsStarted
    {
        get => Base.warsStarted;
        set => Base.warsStarted = value;
    }

    #endregion

    #region Double

    public double WorldTime
    {
        get => Base.world_time;
        set => Base.world_time = value;
    }

    public double ExplodingMushroomsEnabledAt
    {
        get => Base.exploding_mushrooms_enabled_at;
        set => Base.exploding_mushrooms_enabled_at = value;
    }

    public double WorldAgeStartedAt
    {
        get => Base.world_age_started_at;
        set => Base.world_age_started_at = value;
    }

    public double SameWorldAgeStartedAt
    {
        get => Base.same_world_age_started_at;
        set => Base.same_world_age_started_at = value;
    }

    #endregion
}