using System;
using WorldLib.Utils;

namespace WorldLib.Models.Objects;

extern alias GameAsm;

/// <summary>
///     Represents any physical object in the world.
/// </summary>
/// <typeparam name="TAbstracts">The type of Raw, is used for parent classes</typeparam>
public class NanoObject<TAbstracts> : AbstractionOf<TAbstracts>, IEquatable<NanoObject<TAbstracts>>
    where TAbstracts : GameAsm::NanoObject
{
    /// <summary>
    ///     Creates a NanoObject wrapper.
    /// </summary>
    /// <param name="store">The game-facing nano object.</param>
    public NanoObject(TAbstracts store) : base(store)
    {
    }

    /// <summary>
    ///     Whether this object is alive.
    /// </summary>
    public bool Alive
    {
        get => Raw.isAlive();
        set => Raw.setAlive(value);
    }

    /// <summary>
    ///     Whether this object exists.
    /// </summary>
    public bool Exists
    {
        get => Raw.exists;
        set => Raw.exists = value;
    }

    /// <summary>
    ///     Name of this object.
    /// </summary>
    public string Name
    {
        get => Raw.name;
        set => Raw.setName(value);
    }

    /// <summary>
    ///     Hash of this object. Used for equality checking with other nano objects.
    /// </summary>
    public int Hash
    {
        get => Raw.GetHashCode();
        set => Raw.setHash(value);
    }

    /// <summary>
    ///     Color scheme of this asset, used for things like colored buildings, boats or clothing.
    /// </summary>
    //TODO: Abstract ColorAsset
    public GameAsm::ColorAsset Color => Raw.getColor();

    /// <summary>
    ///     MetaType of the object, defines if the object is an actor, culture, subspecies, etc.
    /// </summary>
    //TODO: Abstract MetaType (using MetaTypeAsset)
    public GameAsm::MetaType Type => Raw.getMetaType();

    /// <summary>
    ///     Whether this nano object represents the same nano object as another nano object wrapper.
    /// </summary>
    /// <param name="other">The other nano object wrapper to check equality for.</param>
    public bool Equals(NanoObject<TAbstracts> other)
    {
        return Hash == other.Hash;
    }

    /// <summary>
    ///     Resets all state to default settings.
    /// </summary>
    public void SetDefaults()
    {
        Raw.setDefaultValues();
    }

    /// <summary>
    ///     Whether this object has died.
    /// </summary>
    public bool HasDied()
    {
        return Raw.hasDied();
    }

    /// <summary>
    ///     Whether this nano object is null or otherwise dead.
    /// </summary>
    public bool IsRekt()
    {
        return GameAsm::NanoObjectExtensions.isRekt(Raw);
    }
}