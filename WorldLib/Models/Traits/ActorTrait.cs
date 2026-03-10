extern alias GameAsm;
using WorldLib.Models.Assets;

namespace WorldLib.Models.Traits;

/// <summary>
///     Represents a trait that can be directly applied to actors.
/// </summary>
public class ActorTrait : Trait<ActorTrait, GameAsm::ActorTrait>
{
    internal ActorTrait(GameAsm::ActorTrait store) : base(store)
    {
    }

    /// <summary>
    ///     The category group this trait belongs to.
    /// </summary>
    public CategoryAsset Group => new(Raw.getGroup());

    /// <summary>
    ///     The rate at which this trait is inherited at birth.
    /// </summary>
    public int BirthRate
    {
        get => Raw.rate_birth;
        set => Raw.rate_birth = value;
    }

    /// <summary>
    ///     The rate at which this trait can be acquired during grow-up simulation.
    /// </summary>
    public int GrowUpAcquireRate
    {
        get => Raw.rate_acquire_grow_up;
        set => Raw.rate_acquire_grow_up = value;
    }

    /// <summary>
    ///     Whether this trait can only be acquired during grow-up by sapient actors.
    /// </summary>
    public bool GrowUpSapientOnly
    {
        get => Raw.acquire_grow_up_sapient_only;
        set => Raw.acquire_grow_up_sapient_only = value;
    }

    /// <summary>
    ///     The rate at which this trait is passed down through inheritance.
    /// </summary>
    public int InheritRate
    {
        get => Raw.rate_inherit;
        set => Raw.rate_inherit = value;
    }

    /// <summary>
    ///     Whether this trait can appear in the mutation box.
    /// </summary>
    public bool IsMutationBoxAllowed
    {
        get => Raw.is_mutation_box_allowed;
        set => Raw.is_mutation_box_allowed = value;
    }

    /// <summary>
    ///     Modifier applied when an actor already has this trait, affecting acquisition or stacking behavior.
    /// </summary>
    public int SameTraitMod
    {
        get => Raw.same_trait_mod;
        set => Raw.same_trait_mod = value;
    }

    /// <summary>
    ///     Modifier applied when an actor has a trait considered opposite to this one.
    /// </summary>
    public int OppositeTraitMod
    {
        get => Raw.opposite_trait_mod;
        set => Raw.opposite_trait_mod = value;
    }

    /// <summary>
    ///     Whether this trait is only active when a specific era flag is set.
    /// </summary>
    public bool OnlyActiveOnEraFlag
    {
        get => Raw.only_active_on_era_flag;
        set => Raw.only_active_on_era_flag = value;
    }

    /// <summary>
    ///     Whether this trait is only active during the moon era.
    /// </summary>
    public bool EraActiveMoon
    {
        get => Raw.era_active_moon;
        set => Raw.era_active_moon = value;
    }

    /// <summary>
    ///     Whether this trait is only active during the night era.
    /// </summary>
    public bool EraActiveNight
    {
        get => Raw.era_active_night;
        set => Raw.era_active_night = value;
    }

    /// <summary>
    ///     The general classification of this trait.
    /// </summary>
    public GameAsm::TraitType Type
    {
        get => Raw.type;
        set => Raw.type = value;
    }

    /// <summary>
    ///     Whether this trait is automatically removed when the actor becomes a zombie.
    /// </summary>
    public bool RemoveForZombieActor
    {
        get => Raw.remove_for_zombie_actor_asset;
        set => Raw.remove_for_zombie_actor_asset = value;
    }

    /// <summary>
    ///     Whether this trait can be cured through gameplay mechanics.
    /// </summary>
    public bool CanBeCured
    {
        get => Raw.can_be_cured;
        set => Raw.can_be_cured = value;
    }

    //TODO: Add CREF to StrongMinded and StrongMind
    /// <summary>
    ///     Whether this trait has effects on the actor's mind or mental state. If true, this trait cannot be applied to an
    ///     actor with the StrongMind tag (often acquired through the StrongMinded trait).
    /// </summary>
    public bool AffectsMind
    {
        get => Raw.affects_mind;
        set => Raw.affects_mind = value;
    }

    //TODO: Explain WildKingdoms through a <seealso /> annotation.
    /// <summary>
    ///     The wild kingdom this trait forces the actor to be associated with, if any.
    /// </summary>
    //TODO: Abstract Kingdom.
    public GameAsm::Kingdom ForcedKingdom => Raw.getForcedKingdom();

    //TODO: Add CREF to DivineLight
    /// <summary>
    ///     Whether this trait can be removed by the DivineLight power.
    /// </summary>
    public bool CanBeRemovedByDivineLight
    {
        get => Raw.can_be_removed_by_divine_light;
        set => Raw.can_be_removed_by_divine_light = value;
    }

    //TODO: Add CREF to AcceleratedHealing
    /// <summary>
    ///     Whether this trait can be removed when an actor grows up and their subspecies has the AcceleratedHealing trait.
    /// </summary>
    public bool CanBeRemovedByAcceleratedHealing
    {
        get => Raw.can_be_removed_by_accelerated_healing;
        set => Raw.can_be_removed_by_accelerated_healing = value;
    }

    /// <summary>
    ///     A modifier to an actors likability when they have this trait.
    ///     Is applied in conjunction with <see cref="SameTraitMod" /> and <see cref="OppositeTraitMod" />
    /// </summary>
    public float Likeability
    {
        get => Raw.likeability;
        set => Raw.likeability = value;
    }

    //TODO: Add CREF to MasterOfCombat
    /// <summary>
    ///     Whether this trait contributes to the MasterOfCombat achievement checker.
    ///     This is usually only true on skill traits like dash and dodge.
    /// </summary>
    /// <remarks>
    ///     If an actor has 5 traits where this bool is true, you will receive the MasterOfCombat achievement.
    /// </remarks>
    public bool InTrainingDummyCombatPot
    {
        get => Raw.in_training_dummy_combat_pot;
        set => Raw.in_training_dummy_combat_pot = value;
    }
}