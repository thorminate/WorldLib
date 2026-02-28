using System.Collections.Generic;
using UnityEngine;
using WorldLib.Utils;

namespace WorldLib.Models.Generic;

extern alias GameAsm;

//TODO: Clean up setters so they update internal caches if any.
public class SimObject : AbstractionOf<GameAsm::BaseSimObject>, INanoObject
{
    internal SimObject(GameAsm::BaseSimObject simObj) : base(simObj)
    {
    }


    public HashSet<long> TargetsToIgnore => Base._targets_to_ignore;

    // TODO: Abstract WorldTile
    public GameAsm::WorldTile Tile
    {
        get => Base.current_tile;
        set => Base.current_tile = value;
    }

    public Vector2 Pos
    {
        get => Base.current_position;
        set => Base.current_position = value;
    }

    public Vector3 Scale
    {
        get => Base.current_scale;
        set => Base.current_scale = value;
    }

    public Vector3 Rotation
    {
        get => Base.current_rotation;
        set => Base.current_rotation = value;
    }

    //TODO: Abstract Kingdom
    public GameAsm::Kingdom Kingdom
    {
        get => Base.kingdom;
        set => Base.kingdom = value;
    }

    //TODO: Abstract BaseStats
    public GameAsm::BaseStats Stats => Base.stats;

    //TODO: Abstract MapObjectType
    public GameAsm::MapObjectType ObjectType => Base._object_type;

    //TODO: Abstract Status
    public IReadOnlyDictionary<string, GameAsm::Status> StatusDict => Base.getStatusesDict();

    public int TotalStatuses => Base.countStatusEffects();

    //TODO: Abstract Status
    public Dictionary<string, GameAsm::Status>.ValueCollection Statuses => Base.getStatuses();

    //TODO: Abstract Status
    public Dictionary<string, GameAsm::Status>.KeyCollection StatusIds => Base.getStatusesIds();

    public float Height
    {
        get => Base.getHeight();
        set => Base.position_height = value;
    }

    public bool Alive
    {
        get => Base.isAlive();
        set => Base.setAlive(value);
    }

    public bool Exists
    {
        get => Base.exists;
        set => Base.exists = value;
    }

    public string Name
    {
        get => Base.name;
        set => Base.setName(value);
    }

    public int Hash
    {
        get => Base.GetHashCode();
        set => Base.setHash(value);
    }


    //TODO: Abstract ColorAsset
    public GameAsm::ColorAsset Color => Base.getColor();

    //TODO: Abstract MetaType
    public GameAsm::MetaType Type => Base.getMetaType();


    public void SetDefaults()
    {
        Base.setDefaultValues();
    }

    public bool HasDied()
    {
        return Base.hasDied();
    }

    //TODO: Abstract StatusAsset
    public bool AddStatus(GameAsm::StatusAsset status)
    {
        return Base.addStatusEffect(status);
    }
}