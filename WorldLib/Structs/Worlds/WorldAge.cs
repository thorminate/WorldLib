using System.Collections.Generic;
using UnityEngine;
using WorldLib.Utils;

namespace WorldLib.Structs.Worlds;

extern alias GameAsm;

public sealed class WorldAge : AbstractionOf<GameAsm::WorldAgeAsset>
{
    internal WorldAge(GameAsm::WorldAgeAsset asset) : base(asset)
    {
    }

    #region Other

    // TODO: Abstract this
    public GameAsm::WorldAgeAction SpecialEffectAction
    {
        get => Base.special_effect_action;
        set => Base.special_effect_action = value;
    }

    #endregion

    #region Int

    public int YearsMin
    {
        get => Base.years_min;
        set => Base.years_min = value;
    }

    public int YearsMax
    {
        get => Base.years_max;
        set => Base.years_max = value;
    }

    public int BonusLoyalty
    {
        get => Base.bonus_loyalty;
        set => Base.bonus_loyalty = value;
    }

    public int BonusOpinion
    {
        get => Base.bonus_opinion;
        set => Base.bonus_opinion = value;
    }

    public int BonusBiomesGrowth
    {
        get => Base.bonus_biomes_growth;
        set => Base.bonus_biomes_growth = value;
    }

    public int TemperatureDamageBonus
    {
        get => Base.temperature_damage_bonus;
        set => Base.temperature_damage_bonus = value;
    }

    public int Rate
    {
        get => Base.rate;
        set => Base.rate = value;
    }

    #endregion

    #region Float

    public float EraEffectOverlayAlpha
    {
        get => Base.era_effect_overlay_alpha;
        set => Base.era_effect_overlay_alpha = value;
    }

    public float EraEffectLightAlphaGame
    {
        get => Base.era_effect_light_alpha_game;
        set => Base.era_effect_light_alpha_game = value;
    }

    public float EraEffectLightAlphaMinimap
    {
        get => Base.era_effect_light_alpha_minimap;
        set => Base.era_effect_light_alpha_minimap = value;
    }

    public float CloudInterval
    {
        get => Base.cloud_interval;
        set => Base.cloud_interval = value;
    }

    public float RangeWeaponsMultiplier
    {
        get => Base.range_weapons_multiplier;
        set => Base.range_weapons_multiplier = value;
    }

    public float FireElementalSpawnChance
    {
        get => Base.fire_elemental_spawn_chance;
        set => Base.fire_elemental_spawn_chance = value;
    }

    public float FireSpreadRateBonus
    {
        get => Base.fire_spread_rate_bonus;
        set => Base.fire_spread_rate_bonus = value;
    }

    public float SpecialEffectInterval
    {
        get => Base.special_effect_interval;
        set => Base.special_effect_interval = value;
    }

    #endregion

    #region Bool

    public bool OverlayDarkness
    {
        get => Base.overlay_darkness;
        set => Base.overlay_darkness = value;
    }

    public bool ParticlesSnow
    {
        get => Base.particles_snow;
        set => Base.particles_snow = value;
    }

    public bool ParticlesRain
    {
        get => Base.particles_rain;
        set => Base.particles_rain = value;
    }

    public bool ParticlesMagic
    {
        get => Base.particles_magic;
        set => Base.particles_magic = value;
    }

    public bool ParticlesAsh
    {
        get => Base.particles_ash;
        set => Base.particles_ash = value;
    }

    public bool ParticlesSun
    {
        get => Base.particles_sun;
        set => Base.particles_sun = value;
    }

    public bool GlobalFreezeWorld
    {
        get => Base.global_freeze_world;
        set => Base.global_freeze_world = value;
    }

    public bool GlobalUnfreezeWorld
    {
        get => Base.global_unfreeze_world;
        set => Base.global_unfreeze_world = value;
    }

    public bool GlobalUnfreezeWorldMountains
    {
        get => Base.global_unfreeze_world_mountains;
        set => Base.global_unfreeze_world_mountains = value;
    }

    public bool OverlayMagic
    {
        get => Base.overlay_magic;
        set => Base.overlay_magic = value;
    }

    public bool OverlayRainDarkness
    {
        get => Base.overlay_rain_darkness;
        set => Base.overlay_rain_darkness = value;
    }

    public bool OverlayWinter
    {
        get => Base.overlay_winter;
        set => Base.overlay_winter = value;
    }

    public bool OverlayChaos
    {
        get => Base.overlay_chaos;
        set => Base.overlay_chaos = value;
    }

    public bool OverlayMoon
    {
        get => Base.overlay_moon;
        set => Base.overlay_moon = value;
    }

    public bool OverlaySun
    {
        get => Base.overlay_sun;
        set => Base.overlay_sun = value;
    }

    public bool OverlayAsh
    {
        get => Base.overlay_ash;
        set => Base.overlay_ash = value;
    }

    public bool OverlayNight
    {
        get => Base.overlay_night;
        set => Base.overlay_night = value;
    }

    public bool OverlayRain
    {
        get => Base.overlay_rain;
        set => Base.overlay_rain = value;
    }

    public bool FlagCropsGrow
    {
        get => Base.flag_crops_grow;
        set => Base.flag_crops_grow = value;
    }

    public bool EraDisasterSnowTurnsBabiesIntoIceOnes
    {
        get => Base.era_disaster_snow_turns_babies_into_ice_ones;
        set => Base.era_disaster_snow_turns_babies_into_ice_ones = value;
    }

    public bool EraDisasterFireElementalSpawnOnFire
    {
        get => Base.era_disaster_fire_elemental_spawn_on_fire;
        set => Base.era_disaster_fire_elemental_spawn_on_fire = value;
    }

    public bool EraDisasterRageBringsDemons
    {
        get => Base.era_disaster_rage_brings_demons;
        set => Base.era_disaster_rage_brings_demons = value;
    }

    public bool FlagLightAge
    {
        get => Base.flag_light_age;
        set => Base.flag_light_age = value;
    }

    public bool FlagChaos
    {
        get => Base.flag_chaos;
        set => Base.flag_chaos = value;
    }

    public bool FlagWinter
    {
        get => Base.flag_winter;
        set => Base.flag_winter = value;
    }

    public bool FlagMoon
    {
        get => Base.flag_moon;
        set => Base.flag_moon = value;
    }

    public bool FlagNight
    {
        get => Base.flag_night;
        set => Base.flag_night = value;
    }

    public bool FlagLightDamage
    {
        get => Base.flag_light_damage;
        set => Base.flag_light_damage = value;
    }

    #endregion

    #region String

    public string ForceNext
    {
        get => Base.force_next;
        set => Base.force_next = value;
    }

    public string PathIcon
    {
        get => Base.path_icon;
        set => Base.path_icon = value;
    }

    public string PathBackground
    {
        get => Base.path_background;
        set => Base.path_background = value;
    }

    #endregion

    #region Collections

    public List<string> Clouds
    {
        get => Base.clouds;
        set => Base.clouds = value;
    }

    public HashSet<string> Biomes
    {
        get => Base.biomes;
        set => Base.biomes = value;
    }

    public string[] Conditions
    {
        get => Base.conditions;
        set => Base.conditions = value;
    }

    public List<int> DefaultSlots
    {
        get => Base.default_slots;
        set => Base.default_slots = value;
    }

    #endregion

    #region Unity Objects

    public Sprite CachedSprite
    {
        get => Base._cached_sprite;
        set => Base._cached_sprite = value;
    }

    public Sprite CachedBackground
    {
        get => Base._cached_background;
        set => Base._cached_background = value;
    }

    public Color LightColor
    {
        get => Base.light_color;
        set => Base.light_color = value;
    }

    public Color TitleColor
    {
        get => Base.title_color;
        set => Base.title_color = value;
    }

    #endregion
}