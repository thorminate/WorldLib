using System.Collections.Generic;
using UnityEngine;
using WorldLib.Models.Statuses;
using WorldLib.Models.Time;

namespace WorldLib.Models.Objects;

extern alias GameAsm;

/// <summary>
///     Represents a physical object with special data like positioning and sprites.
/// </summary>
/// <typeparam name="TAbstracts">The type of Raw, is used for parent classes</typeparam>
//TODO: Clean up setters so they update internal caches if any.
public class SimObject<TAbstracts> : NanoObject<TAbstracts>
    where TAbstracts : GameAsm::BaseSimObject
{
    internal SimObject(TAbstracts simObj) : base(simObj)
    {
    }


    /// <summary>
    ///     The current <see cref="WorldTile" /> this object resides on.
    /// </summary>
    // TODO: Abstract WorldTile
    public GameAsm::WorldTile Tile
    {
        get => Raw.current_tile;
        set => Raw.current_tile = value;
    }

    /// <summary>
    ///     The 2-dimensional position on the map.
    /// </summary>
    public Vector2 Pos
    {
        get => Raw.current_position;
        set => Raw.current_position = value;
    }

    /// <summary>
    ///     The scale of the rendered asset.
    /// </summary>
    public Vector3 Scale
    {
        get => Raw.current_scale;
        set => Raw.current_scale = value;
    }

    /// <summary>
    ///     The rotation of the rendered asset.
    /// </summary>
    public Vector3 Rotation
    {
        get => Raw.current_rotation;
        set => Raw.current_rotation = value;
    }

    /// <summary>
    ///     The SimObjects associated Kingdom.
    /// </summary>
    //TODO: Abstract Kingdom
    public GameAsm::Kingdom Kingdom
    {
        get => Raw.kingdom;
        set => Raw.kingdom = value;
    }

    /// <summary>
    ///     Stats of the sim object, like "health" or "lifespan."
    /// </summary>
    //TODO: Abstract BaseStats, link to SimObject to ensure setStatsDirty is properly used.
    public GameAsm::BaseStats Stats => Raw.stats;

    /// <summary>
    ///     The type of the object in map's context. Like a building or actor.
    /// </summary>
    public GameAsm::MapObjectType MapObjectType
    {
        get => Raw._object_type;
        set => Raw.setObjectType(value);
    }

    /// <summary>
    ///     A dictionary of all the statuses in this object.
    /// </summary>
    //TODO: Abstract Status, make TransmutableDictionary and allow for mutability from here.
    public IReadOnlyDictionary<string, GameAsm::Status> StatusDict => Raw.getStatusesDict();

    /// <summary>
    ///     Total amount of statuses in this object.
    /// </summary>
    public int TotalStatuses => Raw.countStatusEffects();

    /// <summary>
    ///     Height of the object on the map, used for things like floating above the ground. 0 by default.
    /// </summary>
    public float Height
    {
        get => Raw.getHeight();
        set => Raw.position_height = value;
    }

    /// <summary>
    ///     Whether the stats have been modified since the last frame.
    /// </summary>
    public bool StatsDirty
    {
        get => Raw.isStatsDirty();
        set => Raw._stats_dirty = value;
    }

    /// <summary>
    ///     The chunk that this object currently resides in.
    /// </summary>
    //TODO: Abstract MapChunk
    public GameAsm::MapChunk Chunk => Raw.chunk;

    /// <summary>
    ///     Whether this sim object wrapper wraps the same sim object as another sim object wrapper.
    /// </summary>
    /// <param name="other">The other sim object wrapper.</param>
    public bool Equals(SimObject<TAbstracts> other)
    {
        return Raw.Equals(other.Raw);
    }

    /// <summary>
    ///     Adds a status effect to the object.
    /// </summary>
    /// <param name="status">The status asset to add.</param>
    /// <param name="resetTimer">Whether to reset the timer if the status effect is already applied.</param>
    /// <param name="colorEffect">Whether to apply a white flash to the object receiving this status effect.</param>
    /// <returns>Whether this operation succeeded.</returns>
    public bool AddStatus(StatusAsset status, float? resetTimer, bool? colorEffect)
    {
        return Raw.addStatusEffect(status.Raw, resetTimer ?? 0f, colorEffect ?? true);
    }

    /// <summary>
    ///     Finishes all statuses at once.
    /// </summary>
    public void FinishAllStatuses()
    {
        Raw.finishAllStatusEffects();
    }

    /// <summary>
    ///     Finishes a status, does nothing if the status asset has no asset represinting it in the object.
    /// </summary>
    /// <param name="status">The status asset to finish the status for.</param>
    public void FinishStatus(StatusAsset status)
    {
        Raw.finishStatusEffect(status.Raw.id);
    }

    /// <summary>
    ///     Whether this object has a status representing that status asset.
    /// </summary>
    /// <param name="status">The status asset to check against.</param>
    public bool HasStatus(StatusAsset status)
    {
        return Raw.hasStatus(status.Raw.id);
    }

    /// <summary>
    ///     Whether this object has any status at all.
    /// </summary>
    public bool HasAnyStatus()
    {
        return Raw.hasAnyStatusEffect();
    }

    /// <summary>
    ///     Whether this sim object is an actor.
    /// </summary>
    public bool IsActor()
    {
        return Raw.isActor();
    }

    /// <summary>
    ///     Whether this sim object is a building.
    /// </summary>
    public bool IsBuilding()
    {
        return Raw.isBuilding();
    }

    /// <summary>
    ///     Whether this object in currently on a liquid tile.
    /// </summary>
    public bool IsOnLiquid()
    {
        return Raw.isInLiquid();
    }

    /// <summary>
    ///     Whether this object is currently on a water tile.
    /// </summary>
    public bool IsOnWater()
    {
        return Raw.isInWater();
    }

    /// <summary>
    ///     Whether this object is currently submerged in a liquid.
    /// </summary>
    public bool IsTouchingLiquid()
    {
        return Raw.isTouchingLiquid();
    }

    /// <summary>
    ///     Whether this object is not on the ground. Like being thrown up in the air.
    /// </summary>
    public bool IsInAir()
    {
        return Raw.isInAir();
    }

    /// <summary>
    ///     Whether this object is actively flying above the ground. Like butterflies.
    /// </summary>
    public bool IsFlying()
    {
        return Raw.isFlying();
    }

    /// <summary>
    ///     Registers a hit to the object.
    /// </summary>
    /// <param name="damage">The damage to apply.</param>
    /// <param name="flash">Whether this attack causes a red flash.</param>
    /// <param name="attackType">How the damage was applied (e.g. weapon, fire, acid)</param>
    /// <param name="attacker">SimObject of the attacker.</param>
    /// <param name="skipIfShake">Whether to prevent any damage if the world is shaking.</param>
    /// <param name="playClang">Whether to play a clang sound on hit, often used for metallic weapons.</param>
    /// <param name="damageReduction">Whether to apply damage reduction from things like stats.</param>
    public void Hit(float damage, bool flash = true, GameAsm::AttackType attackType = GameAsm::AttackType.Other,
        SimObject<TAbstracts>? attacker = null, bool skipIfShake = true, bool playClang = false,
        bool damageReduction = true)
    {
        Raw.getHit(damage, flash, attackType, attacker?.Raw, skipIfShake, playClang, damageReduction);
    }

    /// <summary>
    ///     Runs <see cref="Hit" /> with the current health.
    /// </summary>
    /// <param name="attackType">How the damage was applied (e.g. weapon, fire, acid)</param>
    public void HitFullHealth(GameAsm::AttackType attackType)
    {
        Raw.getHitFullHealth(attackType);
    }

    /// <summary>
    ///     Finds a sim object to target.
    /// </summary>
    /// <param name="attackBuildings">Whether to attack buildings as well.</param>
    public SimObject<GameAsm::BaseSimObject> FindEnemyTarget(bool attackBuildings)
    {
        return new SimObject<GameAsm::BaseSimObject>(Raw.findEnemyObjectTarget(attackBuildings));
    }

    /// <summary>
    ///     Ignores a sim object and prevents them from being targeted.
    /// </summary>
    /// <param name="target">The sim object to ignore.</param>
    public void IgnoreTarget(SimObject<GameAsm::BaseSimObject> target)
    {
        Raw.ignoreTarget(target.Raw);
    }

    /// <summary>
    ///     Whether a specific sim object is ignored and should not be targeted.
    /// </summary>
    /// <param name="target">The sim object to check for.</param>
    public bool ShouldIgnoreTarget(SimObject<TAbstracts> target)
    {
        return Raw.shouldIgnoreTarget(target.Raw);
    }

    /// <summary>
    ///     Removes all ignored targets.
    /// </summary>
    public void ClearIgnoredTargets()
    {
        Raw.clearIgnoreTargets();
    }

    /// <summary>
    ///     Whether the sim object can attack another sim object.
    /// </summary>
    /// <param name="target">The sim object to check for.</param>
    /// <param name="checkFactions">Whether to check if the target is similar in beliefs and not attack them if so.</param>
    /// <param name="attackBuildings">Whether to allow attacking buildings.</param>
    /// <returns></returns>
    public bool CanAttackTarget(SimObject<TAbstracts> target, bool checkFactions = true, bool attackBuildings = true)
    {
        return Raw.canAttackTarget(target.Raw, checkFactions, attackBuildings);
    }

    /// <summary>
    ///     Whether the target object's kingdom and this object's kingdom are at war.
    /// </summary>
    /// <param name="target">The sim object to check for.</param>
    public bool AreFoes(SimObject<TAbstracts> target)
    {
        return Raw.areFoes(target.Raw);
    }

    /// <summary>
    ///     Sets health to an arbitrary value. This does not kill the object if set at 0.
    /// </summary>
    /// <param name="val">The value to set the health to</param>
    /// <param name="clamp">Whether to clamp the health to its upper and lower limits</param>
    public void SetHealth(int val, bool clamp = true)
    {
        Raw.setHealth(val, clamp);
    }

    /// <summary>
    ///     Sets the object's health to max health.
    /// </summary>
    public void SetHealthToMaxHealth()
    {
        Raw.setMaxHealth();
    }

    /// <summary>
    ///     Adds health and clamps it to the maximum.
    /// </summary>
    /// <param name="val">The amount of health to add.</param>
    public void AddHealth(int val)
    {
        Raw.changeHealth(val);
    }

    /// <summary>
    ///     Gets the current health this object is at.
    /// </summary>
    public int GetHealth()
    {
        return Raw.getHealth();
    }

    /// <summary>
    ///     Gets the maximum health of this object.
    /// </summary>
    public int GetMaxHealth()
    {
        return Raw.getMaxHealth();
    }

    /// <summary>
    ///     Provides a health integer based on a percentage from the max health.
    /// </summary>
    /// <param name="percent">The percent amount of the max health.</param>
    public int GetHealthFromPercentOfMaxHealth(float percent)
    {
        return Raw.getMaxHealthPercent(percent);
    }

    /// <summary>
    ///     Whether the actor has health above 0.
    /// </summary>
    public bool HasHealth()
    {
        return Raw.hasHealth();
    }

    /// <summary>
    ///     Whether this object has a kingdom associated with it.
    /// </summary>
    public bool HasKingdom()
    {
        return Raw.hasKingdom();
    }

    /// <summary>
    ///     When this sim object was created.
    /// </summary>
    public WorldTime GetCreatedTimestamp()
    {
        return new WorldTime(Raw.getFoundedTimestamp());
    }
}