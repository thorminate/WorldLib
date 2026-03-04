using System;
using System.Collections.Generic;
using UnityEngine;
using WorldLib.Models.Statuses;
using WorldLib.Models.Time;
using WorldLib.Utils;

namespace WorldLib.Models.Generic;

extern alias GameAsm;

//TODO: Clean up setters so they update internal caches if any.
public class SimObject : AbstractionOf<GameAsm::BaseSimObject>, INanoObject, IEquatable<SimObject>
{
    internal SimObject(GameAsm::BaseSimObject simObj) : base(simObj)
    {
    }


    public HashSet<long> TargetsToIgnore => Raw._targets_to_ignore;

    // TODO: Abstract WorldTile
    public GameAsm::WorldTile Tile
    {
        get => Raw.current_tile;
        set => Raw.current_tile = value;
    }

    public Vector2 Pos
    {
        get => Raw.current_position;
        set => Raw.current_position = value;
    }

    public Vector3 Scale
    {
        get => Raw.current_scale;
        set => Raw.current_scale = value;
    }

    public Vector3 Rotation
    {
        get => Raw.current_rotation;
        set => Raw.current_rotation = value;
    }

    //TODO: Abstract Kingdom
    public GameAsm::Kingdom Kingdom
    {
        get => Raw.kingdom;
        set => Raw.kingdom = value;
    }

    //TODO: Abstract BaseStats, link to SimObject to ensure setStatsDirty is properly used.
    public GameAsm::BaseStats Stats => Raw.stats;

    public GameAsm::MapObjectType ObjectType
    {
        get => Raw._object_type;
        set => Raw.setObjectType(value);
    }

    //TODO: Abstract Status
    public IReadOnlyDictionary<string, GameAsm::Status> StatusDict => Raw.getStatusesDict();

    public int TotalStatuses => Raw.countStatusEffects();

    //TODO: Abstract Status
    public Dictionary<string, GameAsm::Status>.ValueCollection Statuses => Raw.getStatuses();

    //TODO: Abstract Status
    public Dictionary<string, GameAsm::Status>.KeyCollection StatusIds => Raw.getStatusesIds();

    public float Height
    {
        get => Raw.getHeight();
        set => Raw.position_height = value;
    }

    public bool StatsDirty
    {
        get => Raw.isStatsDirty();
        set => Raw._stats_dirty = value;
    }

    //TODO: Abstract BaseObjectData
    public GameAsm::BaseObjectData Data => Raw.getData();

    //TODO: Abstract MapChunk
    public GameAsm::MapChunk Chunk => Raw.chunk;

    public bool Equals(SimObject other)
    {
        return Raw.Equals(other.Raw);
    }

    public bool Alive
    {
        get => Raw.isAlive();
        set => Raw.setAlive(value);
    }

    public bool Exists
    {
        get => Raw.exists;
        set => Raw.exists = value;
    }

    public string Name
    {
        get => Raw.name;
        set => Raw.setName(value);
    }

    public int Hash
    {
        get => Raw.GetHashCode();
        set => Raw.setHash(value);
    }


    //TODO: Abstract ColorAsset
    public GameAsm::ColorAsset Color => Raw.getColor();

    //TODO: Abstract MetaType
    public GameAsm::MetaType Type => Raw.getMetaType();


    public void SetDefaults()
    {
        Raw.setDefaultValues();
    }

    public bool HasDied()
    {
        return Raw.hasDied();
    }

    public bool Equals(INanoObject other)
    {
        return Raw.GetHashCode() == other.GetHashCode();
    }

    public bool AddStatus(StatusAsset status, float? overrideTimer, bool? colorEffect)
    {
        return Raw.addStatusEffect(status.Raw, overrideTimer ?? 0f, colorEffect ?? true);
    }

    public void FinishAllStatuses()
    {
        Raw.finishAllStatusEffects();
    }

    public void FinishStatus(StatusAsset status)
    {
        Raw.finishStatusEffect(status.Raw.id);
    }

    public bool HasStatus(StatusAsset status)
    {
        return Raw.hasStatus(status.Raw.id);
    }

    public bool HasAnyStatus()
    {
        return Raw.hasAnyStatusEffect();
    }

    public bool IsActor()
    {
        return Raw.isActor();
    }

    public bool IsBuilding()
    {
        return Raw.isBuilding();
    }

    public bool IsInLiquid()
    {
        return Raw.isInLiquid();
    }

    public bool IsInWater()
    {
        return Raw.isInWater();
    }

    public bool IsTouchingLiquid()
    {
        return Raw.isTouchingLiquid();
    }

    public bool IsInAir()
    {
        return Raw.isInAir();
    }

    public bool IsFlying()
    {
        return Raw.isFlying();
    }

    public void GetHit(float damage, bool flash = true, GameAsm::AttackType attackType = GameAsm::AttackType.Other,
        SimObject? attacker = null, bool skipIfShake = true, bool metallicWeapon = false,
        bool checkDamageReduction = true)
    {
        Raw.getHit(damage, flash, attackType, attacker?.Raw, skipIfShake, metallicWeapon, checkDamageReduction);
    }

    public void GetHitFullHealth(GameAsm::AttackType attackType)
    {
        Raw.getHitFullHealth(attackType);
    }

    public SimObject FindEnemyObjectTarget(bool attackBuildings)
    {
        return new SimObject(Raw.findEnemyObjectTarget(attackBuildings));
    }

    public void IgnoreTarget(SimObject target)
    {
        Raw.ignoreTarget(target.Raw);
    }

    public bool ShouldIgnoreTarget(SimObject target)
    {
        return Raw.shouldIgnoreTarget(target.Raw);
    }

    public void ClearIgnoredTargets()
    {
        Raw.clearIgnoreTargets();
    }

    public bool CanAttackTarget(SimObject target, bool checkFactions = true, bool attackBuildings = true)
    {
        return Raw.canAttackTarget(target.Raw, checkFactions, attackBuildings);
    }

    public bool AreEnemies(SimObject target)
    {
        return Raw.areFoes(target.Raw);
    }

    public void SetHealth(int val, bool clamp = true)
    {
        Raw.setHealth(val, clamp);
    }

    public void SetHealthToMaxHealth()
    {
        Raw.setMaxHealth();
    }

    public void AddHealth(int val)
    {
        Raw.changeHealth(val);
    }

    public int GetHealth()
    {
        return Raw.getHealth();
    }

    public int GetMaxHealth()
    {
        return Raw.getMaxHealth();
    }

    public int GetHealthFromPercentOfMaxHealth(float percent)
    {
        return Raw.getMaxHealthPercent(percent);
    }

    public bool HasHealthAboveZero()
    {
        return Raw.hasHealth();
    }

    public bool HasKingdom()
    {
        return Raw.hasKingdom();
    }

    public WorldTime GetCreatedTimestamp()
    {
        return new WorldTime(Raw.getFoundedTimestamp());
    }

    public Actor.Actor Actor()
    {
        return new Actor.Actor(Raw.a);
    }
    
    //TODO: Abstract Building
    public GameAsm::Building Building()
    {
        return Raw.b;
    }
}