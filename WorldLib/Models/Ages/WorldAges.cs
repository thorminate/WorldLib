using System;
using System.Runtime.CompilerServices;
using WorldLib.Models.Time;
using WorldLib.Utils;

namespace WorldLib.Models.Ages;

extern alias GameAsm;

/// <summary>
///     Provides access to all world ages in the game.
/// </summary>
public sealed class WorldAges : AbstractionOf<GameAsm::WorldAgeManager>
{
    internal WorldAges() : base(() => GameAsm::World.world.era_manager)
    {
    }

    /// <summary>
    ///     Represents the Age of Hope.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A time of promise and optimism where the world feels vibrant and full of possibility.
    ///         Life flourishes, and societies look toward the future with confidence.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Hope era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Hope => GetAge(Keys.Hope);

    /// <summary>
    ///     Represents the Age of Sun.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A period of intense heat and relentless sunlight.
    ///         Survival becomes difficult as inhabitants struggle against harsh environmental conditions.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Sun era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Sun => GetAge(Keys.Sun);

    /// <summary>
    ///     Represents the Age of Dark.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A time of overwhelming darkness where shadows dominate the world.
    ///         Dangerous creatures and nightmarish illusions thrive, testing the resilience of all who endure it.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Dark era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Dark => GetAge(Keys.Dark);

    /// <summary>
    ///     Represents the Age of Tears.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         An era of ceaseless rain and pervasive sorrow.
    ///         Though hope is difficult to sustain, some remain steadfast in their belief that brighter days will return.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Tears era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Tears => GetAge(Keys.Tears);

    /// <summary>
    ///     Represents the Age of Moon.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A luminous era bathed in radiant moonlight.
    ///         The world feels enchanted, and arcane wonder fills the night as civilizations look skyward in awe.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Moon era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Moon => GetAge(Keys.Moon);

    /// <summary>
    ///     Represents the Age of Chaos.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A violent era defined by instability, bloodshed, and power struggles.
    ///         War and destruction are commonplace, and survival becomes a daily challenge.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Chaos era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Chaos => GetAge(Keys.Chaos);

    /// <summary>
    ///     Represents the Age of Wonders.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A mystical period saturated with arcane energy.
    ///         Magic flourishes, and extraordinary feats reshape the boundaries of what is possible.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Wonders era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Wonders => GetAge(Keys.Wonders);

    /// <summary>
    ///     Represents the Age of Ice.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A frigid era where ice and snow dominate the landscape.
    ///         The world endures relentless winter, and warmth becomes a scarce and precious resource.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Ice era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Ice => GetAge(Keys.Ice);

    /// <summary>
    ///     Represents the Age of Ash.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A bleak era filled with smoke and airborne sickness.
    ///         The air carries disease, and survival depends on endurance and fragile hope.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Ash era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Ash => GetAge(Keys.Ash);

    /// <summary>
    ///     Represents the Age of Despair.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A prolonged age of darkness and merciless cold.
    ///         Though life seems on the brink of extinction, resilience and faith persist,
    ///         sustaining the world until renewal becomes possible once more.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the Despair era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Despair => GetAge(Keys.Despair);

    /// <summary>
    ///     Represents an unknown or undiscovered age.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A placeholder era used when the current world age cannot be determined
    ///         or when the underlying asset is unavailable. When this age is active, it selects a random age.
    ///     </para>
    ///     <para>
    ///         This value is memoized for performance. To clear the memoization cache and force a reload
    ///         on next access, invoke <see cref="Tooling.MemoClear{T}()" /> with <see cref="WorldAge" /> as the generic type
    ///         argument.
    ///     </para>
    /// </remarks>
    /// <value>
    ///     A <see cref="WorldAge" /> representing an unspecified era.
    /// </value>
    /// <exception cref="InvalidOperationException">
    ///     If you access this before the age gets initialized.
    /// </exception>
    public static WorldAge Unknown => GetAge(Keys.Unknown);

    /// <summary>
    ///     Gets the index of the upcoming age slot.
    /// </summary>
    /// <value>
    ///     The zero-based index of the next scheduled age slot.
    /// </value>
    public int NextSlot
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Base.getNextSlotIndex();
    }


    /// <summary>
    ///     Gets the upcoming <see cref="WorldAge" />.
    /// </summary>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the next scheduled age.
    /// </value>
    public WorldAge NextAge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Base.getNextAge());
    }

    /// <summary>
    ///     Gets the currently active <see cref="WorldAge" />.
    /// </summary>
    /// <value>
    ///     A <see cref="WorldAge" /> representing the active age.
    /// </value>
    public WorldAge CurrentAge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Base.getCurrentAge());
    }

    /// <summary>
    ///     Gets the index of the currently active age slot.
    /// </summary>
    /// <value>
    ///     The zero-based index of the active age slot.
    /// </value>
    public int CurrentSlot
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Base.getCurrentSlotIndex();
    }

    /// <summary>
    ///     Gets the time remaining until the next age starts, in weeks (years * 12 * 5).
    /// </summary>

    public float TimeTillNextAge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Base.getTimeTillNextAge();
    }

    /// <summary>
    ///     Indicates whether the age progression is currently paused.
    /// </summary>
    public bool IsPaused
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Base.isPaused();
    }

    /// <summary>
    ///     Gets the number of moons remaining until the end of the current cycle.
    /// </summary>
    /// <returns>An integer amount of the remaining moons</returns>
    public int RemainingMoons
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Base.calculateMoonsLeft();
    }

    /// <summary>
    ///     A multiplier to how fast ages pass.
    /// </summary>
    /// <returns>A float ascertaining to the age speed multiplier.</returns>
    public float SpeedMultiplier
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GameAsm::World.world.map_stats.world_ages_speed_multiplier;
    }

    /// <summary>
    ///     Gets the timepoint at which the current world age started.
    /// </summary>
    /// <remarks>
    ///     This value resets whenever the active age slot changes,
    ///     even if the new slot references the same age.
    ///     <para>
    ///         See <see cref="AgeStartedAt" /> for a timepoint that
    ///         persists across slot changes when the referenced age remains the same.
    ///     </para>
    /// </remarks>
    public WorldTime SlotStartedAt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(() => GameAsm::World.world.map_stats.world_age_started_at);
    }

    /// <summary>
    ///     Gets the timepoint at which the current world age originally began.
    /// </summary>
    /// <remarks>
    ///     Unlike <see cref="SlotStartedAt" />, this value persists if the active
    ///     age moves to a different slot, provided the slot still references
    ///     the same age.
    /// </remarks>
    public WorldTime AgeStartedAt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(() => GameAsm::World.world.map_stats.same_world_age_started_at);
    }


    /// <summary>
    ///     Returns the <see cref="WorldAge" /> associated with a given slot index.
    /// </summary>
    /// <param name="slot">The zero-based slot index.</param>
    /// <returns>The <see cref="WorldAge" /> in that slot.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public WorldAge AgeFromSlot(int slot)
    {
        return new WorldAge(Base.getAgeFromSlot(slot));
    }

    /// <summary>
    ///     Returns the slot index for a given <see cref="WorldAge" />, if it exists.
    /// </summary>
    /// <param name="age">The <see cref="WorldAge" /> to search for.</param>
    /// <returns>
    ///     The zero-based slot index, or <see langword="null" /> if no slot contains the age.
    /// </returns>
    public int? SlotFromAge(WorldAge age)
    {
        int firstHitIdx = Array.FindIndex(GameAsm::WorldAgeManager._map_stats.world_ages_slots, str => str == age.Id);
        if (firstHitIdx == -1) return null;
        return firstHitIdx;
    }

    /// <summary>
    ///     Assigns a specific <see cref="WorldAge" /> to a given slot index.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method directly sets the age for the specified slot without changing the currently active age (unless you
    ///         modify the slot that is active).
    ///         Use this when you want to rearrange or predefine age slots.
    ///     </para>
    /// </remarks>
    /// <param name="age">The <see cref="WorldAge" /> to assign to the slot.</param>
    /// <param name="slotIndex">The zero-based index of the slot to set the age for.</param>
    public void SetAgeToSlot(WorldAge age, int slotIndex)
    {
        Base.setAgeToSlot(age.Base, slotIndex);
        RefreshUI();
    }

    /// <summary>
    ///     Pauses or unpauses age progression.
    /// </summary>
    /// <param name="shouldPause"><c>true</c> to pause, <c>false</c> to resume.</param>
    public void TogglePaused(bool shouldPause)
    {
        Base.togglePlay(!shouldPause);
        RefreshUI();
    }

    /// <summary>
    ///     Adjusts the speed multiplier for age progression.
    /// </summary>
    public void SetSpeedMultiplier(float speedMultiplier)
    {
        Base.setAgesSpeedMultiplier(speedMultiplier);
        RefreshUI();
    }

    /// <summary>
    ///     Immediately starts the next scheduled age, optionally at a custom progress point.
    /// </summary>
    /// <param name="startProgress">
    ///     The progress at which to start the age (0.0 = beginning, 1.0 = complete).
    /// </param>
    public void StartNextAge(float startProgress = 0f)
    {
        Base.startNextAge(startProgress);
        RefreshUI();
    }

    /// <summary>
    ///     Sets the active <see cref="WorldAge" />.
    /// </summary>
    /// <remarks>
    ///     When <paramref name="doSlotFinding" /> is enabled, the system attempts to locate
    ///     an existing slot associated with the requested age.
    ///     <para>
    ///         If a matching slot is found, it becomes the active slot.
    ///         If no slot is found, the current slot is updated to represent the requested age.
    ///     </para>
    ///     <para>
    ///         Disabling slot finding skips this synchronization step, which may result in
    ///         inconsistent or unexpected slot state. It is recommended to never turn slot finding off.
    ///     </para>
    /// </remarks>
    /// <param name="age">
    ///     The <see cref="WorldAge" /> that should become the active age.
    /// </param>
    /// <param name="doSlotFinding">
    ///     Whether to attempt locating and synchronizing a matching slot for the age.
    /// </param>
    public void SetAge(WorldAge age, bool doSlotFinding = true)
    {
        Base.setCurrentAge(age.Base);

        if (doSlotFinding)
        {
            if (Tooling.TryRun(() => SlotFromAge(age), out int? result) && result.HasValue)
                Base.setCurrentSlotIndex(result.Value);
            else
                Base.setAgeToSlot(age.Base, CurrentSlot);
        }

        RefreshUI();
    }

    /// <summary>
    ///     Sets the active age to a specific slot.
    /// </summary>
    /// <param name="slotIndex">The zero-based slot index to activate.</param>
    public void SetAge(int slotIndex)
    {
        Base.setCurrentSlotIndex(slotIndex);
        RefreshUI();
    }

    /// <summary>
    ///     Refreshes the active age slots in accordance with age rules.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Uses <see cref="WorldAge.DefaultSlots" /> and <see cref="WorldAge.LinkDefaultSlots" /> to build a new set of
    ///         ages in the slots.
    ///     </para>
    /// </remarks>
    public void SetDefaults()
    {
        Base.setDefaultAges();
        RefreshUI();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void RefreshUI()
    {
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static WorldAge GetAge(string key)
    {
        return Tooling.Memoized(key, () =>
            new WorldAge(
                GameAsm::AssetManager.era_library.get(key)
                ?? throw new InvalidOperationException("World ages have not been initialized.")
            ), false)!;
    }


    private static class Keys
    {
        // ReSharper disable MemberHidesStaticFromOuterClass

        public const string Hope = "age_hope";
        public const string Sun = "age_sun";
        public const string Dark = "age_dark";
        public const string Tears = "age_tears";
        public const string Moon = "age_moon";
        public const string Chaos = "age_chaos";
        public const string Wonders = "age_wonders";
        public const string Ice = "age_ice";
        public const string Ash = "age_ash";
        public const string Despair = "age_despair";
        public const string Unknown = "age_unknown";

        // ReSharper restore MemberHidesStaticFromOuterClass
    }
}