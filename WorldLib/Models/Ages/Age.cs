using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldLib.Models.Delegates;
using WorldLib.Utils;

namespace WorldLib.Models.Ages;

extern alias GameAsm;

/// <summary>
///     Represents an age in World Box. Everything is fully mutable and will reflect internally.
/// </summary>
public sealed class Age : AbstractionOf<GameAsm::WorldAgeAsset>
{
    /// <inheritdoc cref="Age" />
    /// <param name="asset">The world age asset to wrap.</param>
    internal Age(GameAsm::WorldAgeAsset asset) : base(asset)
    {
    }

    /// <summary>
    ///     Adds a custom action that runs at a fixed interval while the age is active.
    /// </summary>
    /// <seealso cref="SpecialEffectInterval" />
    /// <seealso cref="ClearSpecialEffects" />
    public void AddSpecialEffect(AgeAction action)
    {
        Raw.special_effect_action += action.Invoke;
    }

    /// <summary>
    ///     Clears all special actions ascertaining to this age.
    /// </summary>
    /// <seealso cref="AddSpecialEffect" />
    /// <seealso cref="SpecialEffectInterval" />
    public void ClearSpecialEffects()
    {
        Raw.special_effect_action = null;
    }

    #region Int

    /// <summary>
    ///     The minimum length of this age, specified in years.
    /// </summary>
    public int MinLength
    {
        get => Raw.years_min;
        set => Raw.years_min = value;
    }

    /// <summary>
    ///     The maximum length of this age, specified in years.
    /// </summary>
    public int MaxLength
    {
        get => Raw.years_max;
        set => Raw.years_max = value;
    }

    /// <summary>
    ///     Provides all actors of the world a loyalty bonus. Can be negative.
    /// </summary>
    public int BonusLoyalty
    {
        get => Raw.bonus_loyalty;
        set => Raw.bonus_loyalty = value;
    }

    /// <summary>
    ///     Provides all actors of the world a bonus in opinion. Can be negative.
    /// </summary>
    public int BonusOpinion
    {
        get => Raw.bonus_opinion;
        set => Raw.bonus_opinion = value;
    }

    /// <summary>
    ///     Provides all biomes of the world a bonus in growth. Can be negative.
    /// </summary>
    public int BonusBiomesGrowth
    {
        get => Raw.bonus_biomes_growth;
        set => Raw.bonus_biomes_growth = value;
    }

    /// <summary>
    ///     Increases damage to temperature related things. Can be negative.
    /// </summary>
    public int TemperatureDamageBonus
    {
        get => Raw.temperature_damage_bonus;
        set => Raw.temperature_damage_bonus = value;
    }

    #endregion

    #region Float

    /// <summary>
    ///     Affects the world overlay effect alpha value (transparency). Should be between 0 and 1. This value is also used in
    ///     conjunction with the player option age_overlay_effect by multiplying the 2 values.
    /// </summary>
    public float EraEffectOverlayAlpha
    {
        get => Raw.era_effect_overlay_alpha;
        set => Raw.era_effect_overlay_alpha = value;
    }

    /// <summary>
    ///     Affects actors' alpha value (transparency). Should be between 0 and 1.
    /// </summary>
    public float EraEffectLightAlphaGame
    {
        get => Raw.era_effect_light_alpha_game;
        set => Raw.era_effect_light_alpha_game = value;
    }

    /// <summary>
    ///     Affects actors' alpha value (transparency) when in minimap mode (zoomed out). Should be between 0 and 1.
    /// </summary>
    public float EraEffectLightAlphaMinimap
    {
        get => Raw.era_effect_light_alpha_minimap;
        set => Raw.era_effect_light_alpha_minimap = value;
    }

    /// <summary>
    ///     How often clouds appear. Lower is faster.
    /// </summary>
    public float CloudInterval
    {
        get => Raw.cloud_interval;
        set => Raw.cloud_interval = value;
    }

    /// <summary>
    ///     Increases actor range when it is using ranged weapons.
    /// </summary>
    public float RangeWeaponsMultiplier
    {
        get => Raw.range_weapons_multiplier;
        set => Raw.range_weapons_multiplier = value;
    }

    /// <summary>
    ///     Chance for fire elementals to spawn on every frame. Should be between 0 and 1.
    /// </summary>
    public float FireElementalSpawnChance
    {
        get => Raw.fire_elemental_spawn_chance;
        set => Raw.fire_elemental_spawn_chance = value;
    }

    /// <summary>
    ///     Bonus multiplier to fire spread rate.
    /// </summary>
    public float FireSpreadRateBonus
    {
        get => Raw.fire_spread_rate_bonus;
        set => Raw.fire_spread_rate_bonus = value;
    }

    /// <summary>
    ///     Interval at which the special effect action is executed. A lower value results in more frequent execution.
    /// </summary>
    /// <seealso cref="AddSpecialEffect" />
    /// <seealso cref="ClearSpecialEffects" />
    public float SpecialEffectInterval
    {
        get => Raw.special_effect_interval;
        set => Raw.special_effect_interval = value;
    }

    #endregion

    #region Bool

    /// <summary>
    ///     Enables a darkness overlay for this age.
    /// </summary>
    public bool OverlayDarkness
    {
        get => Raw.overlay_darkness;
        set => Raw.overlay_darkness = value;
    }

    /// <summary>
    ///     Enables snowfall particle effects.
    /// </summary>
    public bool ParticlesSnow
    {
        get => Raw.particles_snow;
        set => Raw.particles_snow = value;
    }

    /// <summary>
    ///     Enables rainfall particle effects.
    /// </summary>
    public bool ParticlesRain
    {
        get => Raw.particles_rain;
        set => Raw.particles_rain = value;
    }

    /// <summary>
    ///     Enables magical particle effects.
    /// </summary>
    public bool ParticlesMagic
    {
        get => Raw.particles_magic;
        set => Raw.particles_magic = value;
    }

    /// <summary>
    ///     Enables ash particle effects.
    /// </summary>
    public bool ParticlesAsh
    {
        get => Raw.particles_ash;
        set => Raw.particles_ash = value;
    }

    /// <summary>
    ///     Enables sunray and sun-blob particle effects
    /// </summary>
    public bool ParticlesSun
    {
        get => Raw.particles_sun;
        set => Raw.particles_sun = value;
    }

    /// <summary>
    ///     Covers the entire world in a layer of snow.
    /// </summary>
    public bool GlobalFreezeWorld
    {
        get => Raw.global_freeze_world;
        set => Raw.global_freeze_world = value;
    }

    /// <summary>
    ///     Melts all snow on the world.
    /// </summary>
    public bool GlobalUnfreezeWorld
    {
        get => Raw.global_unfreeze_world;
        set => Raw.global_unfreeze_world = value;
    }

    /// <summary>
    ///     Melts snow that would otherwise appear on mountain tops.
    /// </summary>
    public bool GlobalUnfreezeWorldMountains
    {
        get => Raw.global_unfreeze_world_mountains;
        set => Raw.global_unfreeze_world_mountains = value;
    }

    /// <summary>
    ///     Overlays a pinkish magenta tint. Uses an additive material.
    /// </summary>
    public bool OverlayMagic
    {
        get => Raw.overlay_magic;
        set => Raw.overlay_magic = value;
    }

    /// <summary>
    ///     Overlays a dark purple tint. Uses a color multiplication material.
    /// </summary>
    public bool OverlayRainDarkness
    {
        get => Raw.overlay_rain_darkness;
        set => Raw.overlay_rain_darkness = value;
    }

    /// <summary>
    ///     Does nothing, the sprite renderer it refers to is blank.
    /// </summary>
    public bool OverlayWinter
    {
        get => Raw.overlay_winter;
        set => Raw.overlay_winter = value;
    }

    /// <summary>
    ///     Overlays a red tint. Uses a color multiplication material.
    /// </summary>
    public bool OverlayChaos
    {
        get => Raw.overlay_chaos;
        set => Raw.overlay_chaos = value;
    }

    /// <summary>
    ///     Overlays a cyan tint. Uses a color multiplication material.
    /// </summary>
    public bool OverlayMoon
    {
        get => Raw.overlay_moon;
        set => Raw.overlay_moon = value;
    }

    /// <summary>
    ///     Overlays a bright yellow tint. Uses an additive material.
    /// </summary>
    public bool OverlaySun
    {
        get => Raw.overlay_sun;
        set => Raw.overlay_sun = value;
    }

    /// <summary>
    ///     Overlays a grayish brown tint. Uses the default sprite material.
    /// </summary>
    public bool OverlayAsh
    {
        get => Raw.overlay_ash;
        set => Raw.overlay_ash = value;
    }

    /// <summary>
    ///     Overlays a dark blue tint. Uses a color multiplication material.
    /// </summary>
    public bool OverlayNight
    {
        get => Raw.overlay_night;
        set => Raw.overlay_night = value;
    }

    /// <summary>
    ///     Overlays a blue tint. Uses the default sprite material.
    /// </summary>
    public bool OverlayRain
    {
        get => Raw.overlay_rain;
        set => Raw.overlay_rain = value;
    }

    /// <summary>
    ///     Crops grow in this age.
    /// </summary>
    public bool FlagCropsGrow
    {
        get => Raw.flag_crops_grow;
        set => Raw.flag_crops_grow = value;
    }

    /// <summary>
    ///     Babies become an ice one upon coming into contact with snow.
    /// </summary>
    public bool EraDisasterSnowTurnsBabiesIntoIceOnes
    {
        get => Raw.era_disaster_snow_turns_babies_into_ice_ones;
        set => Raw.era_disaster_snow_turns_babies_into_ice_ones = value;
    }

    /// <summary>
    ///     Fire elementals spawn on tiles with fire when they fizzle out.
    /// </summary>
    public bool EraDisasterFireElementalSpawnOnFire
    {
        get => Raw.era_disaster_fire_elemental_spawn_on_fire;
        set => Raw.era_disaster_fire_elemental_spawn_on_fire = value;
    }

    /// <summary>
    ///     Actors can turn into demons if they have the rage status effect and kill someone.
    /// </summary>
    /// <remarks>
    ///     Other disasters have to be enabled, the actor has to be demonizable, they cannot be blessed and must not be
    ///     favorited by the player for an actor to be converted.
    /// </remarks>
    public bool EraDisasterRageBringsDemons
    {
        get => Raw.era_disaster_rage_brings_demons;
        set => Raw.era_disaster_rage_brings_demons = value;
    }

    /// <summary>
    ///     Flags to the game that this age is a light age. This only affects the sleeping patterns of actors who
    ///     posses the "circadian rhythm" trait.
    /// </summary>
    public bool FlagLightAge
    {
        get => Raw.flag_light_age;
        set => Raw.flag_light_age = value;
    }

    /// <summary>
    ///     Flags to the game that this age is a chaos age. This only affects the sleeping patterns of actors who
    ///     posses the "chaos driven" trait.
    /// </summary>
    public bool FlagChaos
    {
        get => Raw.flag_chaos;
        set => Raw.flag_chaos = value;
    }

    /// <summary>
    ///     Flags to the game that this age is a winter age. This affects the sleeping pattern of actors who posses the "winter
    ///     slumberers" trait and halts biome growth.
    /// </summary>
    public bool FlagWinter
    {
        get => Raw.flag_winter;
        set => Raw.flag_winter = value;
    }

    /// <summary>
    ///     Flags to the game that this age is a moon age. This affects traits that can only be active during a moon age.
    /// </summary>
    public bool FlagMoon
    {
        get => Raw.flag_moon;
        set => Raw.flag_moon = value;
    }

    /// <summary>
    ///     Flags to the game that this age is a night age. This disables the effects of the subspecies trait "photosynthetic
    ///     skin", removes the "sunblessed" effect, and affects the sleeping patterns of actors who posses "nocturnal dormancy"
    ///     trait.
    /// </summary>
    public bool FlagNight
    {
        get => Raw.flag_night;
        set => Raw.flag_night = value;
    }

    /// <summary>
    ///     Enables the heliophobia trait effect. Damaging actors who posses said trait in bright areas.
    /// </summary>
    public bool FlagLightDamage
    {
        get => Raw.flag_light_damage;
        set => Raw.flag_light_damage = value;
    }

    /// <summary>
    ///     Allows this age to occupy all its default slots in sequence.
    /// </summary>
    public bool LinkDefaultSlots
    {
        get => Raw.link_default_slots;
        set => Raw.link_default_slots = value;
    }

    #endregion

    #region String

    /// <summary>
    ///     Internal identifier of the age.
    /// </summary>
    public string Id
    {
        get => Raw.id;
        set => Raw.id = value;
    }

    /// <summary>
    ///     The unity asset path to this age's icon.
    /// </summary>
    public string PathIcon
    {
        get => Raw.path_icon;
        set => Raw.path_icon = value;
    }

    /// <summary>
    ///     The unity asset path to this age's background.
    /// </summary>
    public string PathBackground
    {
        get => Raw.path_background;
        set => Raw.path_background = value;
    }

    #endregion

    #region Collections

    /// <summary>
    ///     A list of cloud ids, signifies the types of clouds that appear in this age. If this is left empty, no clouds spawn
    ///     in this age.
    /// </summary>
    public List<string> Clouds
    {
        get => Raw.clouds;
        set => Raw.clouds = value;
    }

    /// <summary>
    ///     Additional biomes that can randomly spawn in the world.
    /// </summary>
    public HashSet<string> Biomes
    {
        get => Raw.biomes;
        set => Raw.biomes = value;
    }


    /// <summary>
    ///     A list of slots this age can occupy upon reshuffle (like world creation or <see cref="Ages.SetDefaults" />).
    /// </summary>
    public MonitoredList<int> DefaultSlots
    {
        get => _defaultSlots ??= new MonitoredList<int>(Raw.default_slots, UpdateDefaultSlotsCache);
        set
        {
            Raw.default_slots = value.ToList();

            _defaultSlots = new MonitoredList<int>(
                Raw.default_slots,
                UpdateDefaultSlotsCache);

            UpdateDefaultSlotsCache(Raw.default_slots);
        }
    }

    private MonitoredList<int>? _defaultSlots;

    private void UpdateDefaultSlotsCache(List<int> slots)
    {
        Dictionary<int, List<GameAsm::WorldAgeAsset>>? pool = GameAsm::AssetManager.era_library.pool_by_slots;

        foreach (KeyValuePair<int, List<GameAsm::WorldAgeAsset>> kvp in pool) kvp.Value.Remove(Raw);

        foreach (int slot in slots)
        {
            if (!pool.TryGetValue(slot, out List<GameAsm::WorldAgeAsset>? list))
            {
                list = new List<GameAsm::WorldAgeAsset>();
                pool[slot] = list;
            }

            if (!list.Contains(Raw)) list.Add(Raw);
        }
    }

    #endregion

    #region Unity Objects

    /// <summary>
    ///     A sprite ascertaining to the ages icon.
    /// </summary>
    public Sprite Icon
    {
        get => Raw._cached_sprite;
        set => Raw._cached_sprite = value;
    }

    /// <summary>
    ///     A sprite ascertaining to the ages background.
    /// </summary>
    public Sprite Background
    {
        get => Raw._cached_background;
        set => Raw._cached_background = value;
    }

    /// <summary>
    ///     The color of light while the age is active.
    /// </summary>
    public Color LightColor
    {
        get => Raw.light_color;
        set => Raw.light_color = value;
    }

    /// <summary>
    ///     The color of the age title.
    /// </summary>
    public Color TitleColor
    {
        get => Raw.title_color;
        set => Raw.title_color = value;
    }

    #endregion
}