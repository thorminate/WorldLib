extern alias GameAsm;
using System;
using System.Linq;
using WorldLib.Models.Objects;
using WorldLib.Models.Traits;
using WorldLib.Utils;

namespace WorldLib.Models.Actors;

/// <summary>
///     Represents a living object in the world, like a human.
/// </summary>
public class Actor : SimObject<GameAsm::Actor>, ITraitsOwner<ActorTrait, GameAsm::ActorTrait>
{
    internal Actor(GameAsm::Actor actor) : base(actor)
    {
    }

    /// <summary>
    ///     A hash set of all actors to ignore in combat.
    /// </summary>
    public TransmutableHashSet<long, Actor> TargetsToIgnore => Tooling.Memoized(Raw.id.ToString(), () =>
        new TransmutableHashSet<long, Actor>(
            () => Raw._targets_to_ignore.Count,
            () => Raw._targets_to_ignore,
            v => Raw._targets_to_ignore.Add(v),
            v => Raw._targets_to_ignore.Remove(v),
            () => Raw._targets_to_ignore.Clear(),
            v => new Actor(GameAsm::World.world.units.dict[v]),
            v => v.Raw.id
        )) ?? throw new InvalidOperationException("Failed to create TransmutableHashSet TargetsToIgnore with id " +
                                                  Raw.id);

    //TODO

    /// <inheritdoc />
    public TransmutableHashSet<GameAsm::ActorTrait, ActorTrait> Traits =>
        Tooling.Memoized(Raw.id + "_traits", () =>
            new TransmutableHashSet<GameAsm::ActorTrait, ActorTrait>(
                () => Raw.traits.Count,
                () => Raw.traits,
                v => Raw.addTrait(v),
                v => Raw.removeTrait(v),
                () =>
                {
                    foreach (var trait in Raw.traits.ToArray()) Raw.removeTrait(trait);
                },
                v => new ActorTrait(v),
                v => v.Raw
            )) ?? throw new InvalidOperationException("Failed to create Trait TransmutableHashSet for id " + Raw.id);

    /// <inheritdoc />
    public bool HasAnyTrait()
    {
        return Raw.hasTraits();
    }

    /// <inheritdoc />
    public bool HasTrait(ActorTrait trait)
    {
        return Raw.hasTrait(trait.Raw);
    }

    /// <inheritdoc />
    public bool AddTrait(ActorTrait trait, bool removeOpposites = false)
    {
        return Raw.addTrait(trait.Raw, removeOpposites);
    }

    /// <inheritdoc />
    public bool RemoveTrait(ActorTrait trait)
    {
        return Raw.removeTrait(trait.Raw);
    }
}