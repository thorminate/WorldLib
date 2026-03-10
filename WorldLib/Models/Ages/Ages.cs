using System;
using System.Runtime.CompilerServices;
using WorldLib.Models.Time;
using WorldLib.Utils;

namespace WorldLib.Models.Ages;

extern alias GameAsm;

// Docs are from the generator, so we leave this empty.
///
public partial class Ages : AbstractionOf<GameAsm::WorldAgeManager>
{
    internal Ages() : base(() => GameAsm::World.world.era_manager)
    {
    }

    /// <summary>
    ///     Gets the index of the upcoming age slot.
    /// </summary>
    /// <value>
    ///     The zero-based index of the next scheduled age slot.
    /// </value>
    public int NextSlot
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Raw.getNextSlotIndex();
    }


    /// <summary>
    ///     Gets the upcoming <see cref="Age" />.
    /// </summary>
    /// <value>
    ///     A <see cref="Age" /> representing the next scheduled age.
    /// </value>
    public Age NextAge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Raw.getNextAge());
    }

    /// <summary>
    ///     Gets the currently active <see cref="Age" />.
    /// </summary>
    /// <value>
    ///     A <see cref="Age" /> representing the active age.
    /// </value>
    public Age CurrentAge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Raw.getCurrentAge());
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
        get => Raw.getCurrentSlotIndex();
    }

    /// <summary>
    ///     Gets the time remaining until the next age starts, in weeks (years * 12 * 5).
    /// </summary>

    public float TimeTillNextAge
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Raw.getTimeTillNextAge();
    }

    /// <summary>
    ///     Indicates whether the age progression is currently paused.
    /// </summary>
    public bool IsPaused
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Raw.isPaused();
    }

    /// <summary>
    ///     Gets the number of moons remaining until the end of the current cycle.
    /// </summary>
    /// <returns>An integer amount of the remaining moons</returns>
    public int RemainingMoons
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Raw.calculateMoonsLeft();
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
    ///     Returns the <see cref="Age" /> associated with a given slot index.
    /// </summary>
    /// <param name="slot">The zero-based slot index.</param>
    /// <returns>The <see cref="Age" /> in that slot.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Age AgeFromSlot(int slot)
    {
        return new Age(Raw.getAgeFromSlot(slot));
    }

    /// <summary>
    ///     Returns the slot index for a given <see cref="Age" />, if it exists.
    /// </summary>
    /// <param name="age">The <see cref="Age" /> to search for.</param>
    /// <returns>
    ///     The zero-based slot index, or <see langword="null" /> if no slot contains the age.
    /// </returns>
    public int? SlotFromAge(Age age)
    {
        int firstHitIdx = Array.FindIndex(GameAsm::WorldAgeManager._map_stats.world_ages_slots, str => str == age.Id);
        if (firstHitIdx == -1) return null;
        return firstHitIdx;
    }

    /// <summary>
    ///     Assigns a specific <see cref="Age" /> to a given slot index.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method directly sets the age for the specified slot without changing the currently active age (unless you
    ///         modify the slot that is active).
    ///         Use this when you want to rearrange or predefine age slots.
    ///     </para>
    /// </remarks>
    /// <param name="age">The <see cref="Age" /> to assign to the slot.</param>
    /// <param name="slotIndex">The zero-based index of the slot to set the age for.</param>
    public void SetAgeToSlot(Age age, int slotIndex)
    {
        Raw.setAgeToSlot(age.Raw, slotIndex);
        RefreshUI();
    }

    /// <summary>
    ///     Pauses or unpauses age progression.
    /// </summary>
    /// <param name="shouldPause"><c>true</c> to pause, <c>false</c> to resume.</param>
    public void TogglePaused(bool shouldPause)
    {
        Raw.togglePlay(!shouldPause);
        RefreshUI();
    }

    /// <summary>
    ///     Adjusts the speed multiplier for age progression.
    /// </summary>
    public void SetSpeedMultiplier(float speedMultiplier)
    {
        Raw.setAgesSpeedMultiplier(speedMultiplier);
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
        Raw.startNextAge(startProgress);
        RefreshUI();
    }

    /// <summary>
    ///     Sets the active <see cref="Age" />.
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
    ///     The <see cref="Age" /> that should become the active age.
    /// </param>
    /// <param name="doSlotFinding">
    ///     Whether to attempt locating and synchronizing a matching slot for the age.
    /// </param>
    public void SetAge(Age age, bool doSlotFinding = true)
    {
        Raw.setCurrentAge(age.Raw);

        if (doSlotFinding)
        {
            if (Tooling.TryRun(() => SlotFromAge(age), out int? result) && result.HasValue)
                Raw.setCurrentSlotIndex(result.Value);
            else
                Raw.setAgeToSlot(age.Raw, CurrentSlot);
        }

        RefreshUI();
    }

    /// <summary>
    ///     Sets the active age to a specific slot.
    /// </summary>
    /// <param name="slotIndex">The zero-based slot index to activate.</param>
    public void SetAge(int slotIndex)
    {
        Raw.setCurrentSlotIndex(slotIndex);
        RefreshUI();
    }

    /// <summary>
    ///     Refreshes the active age slots in accordance with age rules.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Uses <see cref="Age.DefaultSlots" /> and <see cref="Age.LinkDefaultSlots" /> to build a new set of
    ///         ages in the slots.
    ///     </para>
    /// </remarks>
    public void SetDefaults()
    {
        Raw.setDefaultAges();
        RefreshUI();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void RefreshUI()
    {
        GameAsm::WorldAgesWindow._instance?.updateElements();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once MemberCanBePrivate.Global
    internal static Age GetAge(string key)
    {
        return Tooling.Memoized(key, () =>
            new Age(
                GameAsm::AssetManager.era_library.get(key)
                ?? throw new InvalidOperationException("World ages have not been initialized.")
            ), false)!;
    }


    // ReSharper disable once InconsistentNaming
    internal static class S_Ages
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