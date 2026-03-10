using System;
using System.Collections.Generic;
using System.Linq;
using WorldLib.Models.Assets;
using WorldLib.Models.Delegates;
using WorldLib.Utils;

namespace WorldLib.Models.Traits;

extern alias GameAsm;

/// <summary>
///     Represents a generic trait that is used on things like actors, religions and subspecies.
/// </summary>
/// <typeparam name="TThis">The type of whatever is extending this.</typeparam>
/// <typeparam name="TAbstracts">The type of the game-facing trait.</typeparam>
public class Trait<TThis, TAbstracts> : AugmentationAsset<TAbstracts>, IDescription2Asset
    where TAbstracts : GameAsm::BaseTrait<TAbstracts> where TThis : Trait<TThis, TAbstracts>
{
    private TransmutableList<string, Trait<TThis, TAbstracts>>? _oppositeTraits;
    private TransmutableList<string, Trait<TThis, TAbstracts>>? _removeTraits;


    internal Trait(TAbstracts store) : base(store)
    {
    }


    //TODO: Move certain fields to their specific type to avoid confusion.
    //OnGrowth to ActorTrait and such, use dnSpy to check for usages.
    //Also add updating parent super-delegates when modifying any of the delegates.
    //Like MetaObjectWithTraits<TData, TBaseTrait>.all_actions_actor_growth and whatnot

    /// <summary>
    ///     Gets called every time an actor gets birthed within a subspecies.
    /// </summary>
    /// TODO: MOVE TO SubspeciesTrait
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnBirth => Tooling.Memoized(Raw.id + "_on_birth", () =>
        new DelegateBridge<WorldAction, GameAsm::WorldAction>(
            DelegateAdapter.WorldActionToGame,
            action => { Raw.action_birth += action; },
            action => { Raw.action_birth -= action; },
            () => Raw.action_birth?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException($"Failed to create OnBirth DelegateBridge for Trait id {Raw.id}");


    /// <summary>
    ///     Gets called every time an actor dies with this trait on.
    /// </summary>
    /// TODO: MOVE TO ActorTrait, ClanTrait, SubspeciesTrait, and ReligionTrait
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnDeath => Tooling.Memoized(Raw.id + "_on_death", () =>
        new DelegateBridge<WorldAction, GameAsm::WorldAction>(
            DelegateAdapter.WorldActionToGame,
            action => { Raw.action_death += action; },
            action => { Raw.action_death -= action; },
            () => Raw.action_death?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException($"Failed to create OnDeath DelegateBridge for Trait id {Raw.id}");


    /// <summary>
    ///     Gets called every time an actor goes up in age and their subspecies has this trait.
    /// </summary>
    /// TODO: MOVE TO SubspeciesTrait
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnGrowth => Tooling.Memoized(Raw.id + "_on_growth", () =>
        new DelegateBridge<WorldAction, GameAsm::WorldAction>(
            DelegateAdapter.WorldActionToGame,
            action => { Raw.action_growth += action; },
            action => { Raw.action_growth -= action; },
            () => Raw.action_growth?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException($"Failed to create OnGrowth DelegateBridge for Trait id {Raw.id}");


    /// <summary>
    ///     Gets called when an actor bearing the trait gets hit.
    /// </summary>
    /// TODO: MOVE TO ActorTrait
    public DelegateBridge<GetHitAction, GameAsm::GetHitAction> OnHit => Tooling.Memoized(Raw.id + "_on_hit", () =>
        new DelegateBridge<GetHitAction, GameAsm::GetHitAction>(
            DelegateAdapter.HitActionToGame,
            action => { Raw.action_get_hit += action; },
            action => { Raw.action_get_hit -= action; },
            () => Raw.action_get_hit?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException($"Failed to create OnHit DelegateBridge for Trait id {Raw.id}");

    /// <summary>
    ///     The plot asset that this trait unlocks.
    /// </summary>
    //TODO: Abstract PlotAsset, place in ReligionTrait instead of here.
    public GameAsm::PlotAsset Plot => Raw.plot_asset;

    /// <summary>
    ///     A readonly list of all metaobjects (cultures, religions, actors) who have this trait.
    /// </summary>
    public IReadOnlyList<ITraitsOwner<TThis, TAbstracts>> Owners => Raw
        .getOwnersList()
        .Select(TraitUtils.TraitOwnerToPublic<TThis, TAbstracts>).ToList().AsReadOnly();

    /// <summary>
    ///     How many actors have this trait.
    /// </summary>
    public int TotalOwnerAmount => Raw.getRelatedMetaList().Count();

    /// <summary>
    ///     How many sapient beings have this trait.
    /// </summary>
    //TODO: move to SubspeciesTrait and ActorTrait.
    public int SapientOwnerAmount => Raw.countTraitOwnersByCategories().pCivs;

    /// <summary>
    ///     How many mobs have this trait.
    /// </summary>
    //TODO: move to SubspeciesTrait and ActorTrait.
    public int MobOwnerAmount => Raw.countTraitOwnersByCategories().pMobs;

    /// <summary>
    ///     Whether this trait can randomly be applied to actors in the world.
    /// </summary>
    /// <seealso cref="SpawnRate" />
    public bool RandomlySpawnTrait
    {
        get => Raw.spawn_random_trait_allowed;
        set => Raw.spawn_random_trait_allowed = value;
    }

    /// <summary>
    ///     How often this trait appears in actors across the world. Depends on <see cref="RandomlySpawnTrait" />
    /// </summary>
    /// <seealso cref="RandomlySpawnTrait" />
    public int SpawnRate
    {
        get => Raw.spawn_random_rate;
        set => Raw.spawn_random_rate = value;
    }

    /// <summary>
    ///     Stats applied to the object itself, so for clans, LimitClanMembers, would be modifiable.
    ///     <see cref="UnlockableAsset{TAbstraction}.Stats" /> only applies to the actors that have said object.
    /// </summary>
    /// <seealso cref="UnlockableAsset{TAbstraction}.Stats" />
    public GameAsm::BaseStats MetaStats => Raw.base_stats_meta;

    /// <summary>
    ///     Represents the collection of traits that prevent this trait from being applied.
    /// </summary>
    /// <remarks>
    ///     If an actor currently has any trait in this list, applying this trait will fail.
    /// </remarks>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a trait ID stored internally cannot be resolved in the trait asset library.
    /// </exception>
    public TransmutableList<string, Trait<TThis, TAbstracts>> OppositeTraits =>
        _oppositeTraits ??= new TransmutableList<string, Trait<TThis, TAbstracts>>(
            () => Raw.opposite_list?.Count ?? 0,
            i => Raw.opposite_list[i],
            (i, id) => Raw.opposite_list[i] = id,
            (i, id) => Raw.opposite_list.Insert(i, id),
            i => Raw.opposite_list.RemoveAt(i),
            () => Raw.opposite_list = [],
            id => TraitUtils.TraitToLibrary<TAbstracts>().dict.TryGetValue(id, out var status)
                ? TraitUtils.TraitToPublic<TThis, TAbstracts>(status)
                : throw new KeyNotFoundException(
                    $"Status with id '{id}' in opposite_status does not exist in the status library."),
            status => status.Id
        );

    /// <summary>
    ///     Represents the collection of traits that will be removed from the actor
    ///     once this trait is applied.
    /// </summary>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a trait ID stored internally cannot be resolved in the status asset library.
    /// </exception>
    public TransmutableList<string, Trait<TThis, TAbstracts>> RemoveTraits =>
        _removeTraits ??= new TransmutableList<string, Trait<TThis, TAbstracts>>(
            () => Raw.traits_to_remove_ids?.Length ?? 0,
            i => Raw.traits_to_remove_ids[i],
            (i, id) => Raw.traits_to_remove_ids[i] = id,
            (i, id) =>
            {
                List<string> list = Raw.traits_to_remove_ids?.ToList() ?? [];
                list.Insert(i, id);
                Raw.traits_to_remove_ids = list.ToArray();
            },
            i =>
            {
                List<string> list = Raw.traits_to_remove_ids.ToList();
                list.RemoveAt(i);
                Raw.traits_to_remove_ids = list.ToArray();
            },
            () => Raw.traits_to_remove_ids = [],
            id => TraitUtils.TraitToLibrary<TAbstracts>().dict.TryGetValue(id, out var status)
                ? TraitUtils.TraitToPublic<TThis, TAbstracts>(status)
                : throw new KeyNotFoundException(
                    $"Status with id '{id}' in opposite_status does not exist in the status library."),
            status => status.Id
        );

    /// <inheritdoc />
    public string LocalizedDescription => GameAsm::StringExtension.Localize(Raw.getDescriptionID());

    /// <inheritdoc />
    public string LocalizedDescription2 => GameAsm::StringExtension.Localize(Raw.getDescriptionID2());

    //TODO: Move to ReligionTrait
    /// <summary>
    ///     Whether the trait has an associated plot with it. This is used in-game for rites in ReligionTrait.
    /// </summary>
    /// <returns>Whether the trait has an associated plot.</returns>
    public bool HasPlot()
    {
        return Raw.hasPlotAsset();
    }
}