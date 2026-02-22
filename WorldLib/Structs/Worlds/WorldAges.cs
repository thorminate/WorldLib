using WorldLib.Utils;

namespace WorldLib.Structs.Worlds;

extern alias GameAsm;

public sealed class WorldAges : AbstractionOf<GameAsm::WorldAgeManager>
{
    internal WorldAges() : base(GameAsm::World.world.era_manager)
    {
    }

    public static WorldAge Hope => Tooling.Memoized("Hope",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_hope")));

    public static WorldAge Sun => Tooling.Memoized("Sun",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_sun")));

    public static WorldAge Dark => Tooling.Memoized("Dark",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_dark")));

    public static WorldAge Tears => Tooling.Memoized("Tears",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_tears")));

    public static WorldAge Moon => Tooling.Memoized("Moon",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_moon")));

    public static WorldAge Chaos => Tooling.Memoized("Chaos",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_chaos")));

    public static WorldAge Wonders => Tooling.Memoized("Wonders",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_wonders")));

    public static WorldAge Ice => Tooling.Memoized("Ice",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_ice")));

    public static WorldAge Ash => Tooling.Memoized("Ash",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_ash")));

    public static WorldAge Despair => Tooling.Memoized("Despair",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_despair")));

    public static WorldAge Unknown => Tooling.Memoized("Unknown",
        () => new WorldAge(GameAsm::AssetManager.era_library.get("age_unknown")));

    public WorldAge CurrentAge => new(Base.getCurrentAge());

    public WorldAge NextAge => new(Base.getNextAge());


    public int SlotIndex => Base.getCurrentSlotIndex();

    public int NextSlotIndex => Base.getNextSlotIndex();

    public float NightMod
        => Base.getNightMod();

    public bool ShowLights => Base.shouldShowLights();

    public float TimeTillNextAge => Base.getTimeTillNextAge();

    public bool IsPaused => Base.isPaused();

    public bool IsWinter => Base.isWinter();

    public bool IsNight => Base.isNight();

    public bool IsChaosAge => Base.isChaosAge();

    public bool IsLightAge => Base.isLightAge();

    public int RemainingMoons => Base.calculateMoonsLeft();

    public WorldAge AgeFromSlot(int slot)
    {
        return new WorldAge(Base.getAgeFromSlot(slot));
    }

    public void SetAgeToSlot(WorldAge age, int slotIndex)
    {
        Base.setAgeToSlot(age.Base, slotIndex);
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    public void TogglePaused(bool shouldPause)
    {
        Base.togglePlay(!shouldPause);
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    public void SetSpeedMultiplier(float speedMultiplier)
    {
        Base.setAgesSpeedMultiplier(speedMultiplier);
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    public void StartNextAge(float startProgress)
    {
        Base.startNextAge(startProgress);
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    public void SetCurrentSlotIndex(int slotIndex)
    {
        Base.setCurrentSlotIndex(slotIndex);
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    public void SetAge(WorldAge age)
    {
        Base.setCurrentAge(age.Base);
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    public void SetAge(int slot)
    {
        var age = AgeFromSlot(slot);
        SetAge(age);
    }

    // TODO: Add more fields from WorldAgeManager.
}