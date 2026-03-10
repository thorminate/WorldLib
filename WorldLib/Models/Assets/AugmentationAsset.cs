extern alias GameAsm;
using System;
using System.Collections.Generic;
using System.Linq;
using WorldLib.Models.Delegates;
using WorldLib.Utils;

namespace WorldLib.Models.Assets;

/// <summary>
///     Represents an asset that augments another asset, like traits.
/// </summary>
/// <typeparam name="TAbstraction">The type of Raw, is used for parent classes</typeparam>
public class AugmentationAsset<TAbstraction> : UnlockableAsset<TAbstraction>
    where TAbstraction : GameAsm::BaseAugmentationAsset
{
    private TransmutableList<string, GameAsm::DecisionAsset>? _decisions;
    private TransmutableList<string, GameAsm::SpellAsset>? _spells;

    internal AugmentationAsset(TAbstraction store) : base(store)
    {
    }

    /// <summary>
    ///     Gets called when the augmentation gets applied to an object.
    /// </summary>
    public DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait> OnAdd => Tooling.Memoized(Raw.id + "_on_add",
        () =>
            new DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait>(
                DelegateAdapter.WorldActionTraitToGame,
                trait => { Raw.action_on_augmentation_add += trait; },
                trait => { Raw.action_on_augmentation_add -= trait; },
                () => Raw.action_on_augmentation_add?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException($"Failed to create OnAdd DelegateBridge for Trait id {Raw.id}");

    /// <summary>
    ///     Gets called when the augmentation gets loaded into the game. This applies to loading worlds as well.
    /// </summary>
    public DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait> OnLoad => Tooling.Memoized(Raw.id + "_on_load",
        () =>
            new DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait>(
                DelegateAdapter.WorldActionTraitToGame,
                trait => { Raw.action_on_augmentation_load += trait; },
                trait => { Raw.action_on_augmentation_load -= trait; },
                () => Raw.action_on_augmentation_load?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException($"Failed to create OnLoad DelegateBridge for Trait id {Raw.id}");

    /// <summary>
    ///     A delegate that gets run when the parent object is deleted, like after an actor dying and so-forth.
    /// </summary>
    public DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait> OnObjectRemove => Tooling.Memoized(
        Raw.id + "_on_object_remove", () =>
            new DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait>(
                DelegateAdapter.WorldActionTraitToGame,
                trait => { Raw.action_on_object_remove += trait; },
                trait => { Raw.action_on_object_remove -= trait; },
                () => Raw.action_on_object_remove?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException(
        $"Failed to create OnObjectRemove DelegateBridge for Trait id {Raw.id}");

    /// <summary>
    ///     A delegate that gets run when the augmentation is removed.
    /// </summary>
    public DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait> OnRemove => Tooling.Memoized(
        Raw.id + "_on_remove", () =>
            new DelegateBridge<WorldActionTrait, GameAsm::WorldActionTrait>(
                DelegateAdapter.WorldActionTraitToGame,
                trait => { Raw.action_on_augmentation_remove += trait; },
                trait => { Raw.action_on_augmentation_remove -= trait; },
                () => Raw.action_on_augmentation_remove?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException($"Failed to create OnRemove DelegateBridge for Trait id {Raw.id}");

    /// <summary>
    ///     A delegate that gets run at a set interval.
    /// </summary>
    /// <seealso cref="SpecialEffectInterval" />
    public DelegateBridge<WorldAction, GameAsm::WorldAction> SpecialEffects => Tooling.Memoized(
        Raw.id + "_special_effects", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                action => { Raw.action_special_effect += action; },
                action => { Raw.action_special_effect -= action; },
                () => Raw.action_special_effect?.GetInvocationList() ?? [])
    ) ?? throw new InvalidOperationException(
        $"Failed to create SpecialEffects DelegateBridge for Trait id {Raw.id}");

    /// <summary>
    ///     The rate at which the <see cref="SpecialEffects" /> delegate is called.
    /// </summary>
    public float SpecialEffectInterval
    {
        get => Raw.special_effect_interval;
        set => Raw.special_effect_interval = value;
    }

    /// <summary>
    ///     Spells that can be cast when this augmentation is active.
    /// </summary>
    /// <exception cref="KeyNotFoundException">When a spell id does not exist in the spell library.</exception>
    /// <seealso cref="AddSpell" />
    //TODO: Abstract SpellAsset
    public TransmutableList<string, GameAsm::SpellAsset> Spells =>
        _spells ??= new TransmutableList<string, GameAsm::SpellAsset>(
            () => Raw.spells_ids.Count,
            i => Raw.spells_ids[i],
            (i, id) =>
            {
                if (!GameAsm::AssetManager.spells.dict.TryGetValue(id, out var spell))
                    throw new KeyNotFoundException($"Spell with id '{id}' does not exist in the spell library.");
                Raw.spells_ids[i] = id;
                Raw.spells[i] = spell;
            },
            (i, id) =>
            {
                if (!GameAsm::AssetManager.spells.dict.TryGetValue(id, out var spell))
                    throw new KeyNotFoundException($"Spell with id '{id}' does not exist in the spell library.");
                Raw.spells_ids.Insert(i, id);
                Raw.spells.Insert(i, spell);
            },
            i =>
            {
                Raw.spells_ids.RemoveAt(i);
                Raw.spells.RemoveAt(i);
            },
            () =>
            {
                Raw.spells_ids.Clear();
                Raw.spells.Clear();
            },
            id => GameAsm::AssetManager.spells.dict.TryGetValue(id, out var spell)
                ? spell
                : throw new KeyNotFoundException($"Spell with id '{id}' does not exist in the spell library."),
            asset => asset.id);

    /// <summary>
    ///     Allows actors to have a special decision when this augmentation is active.
    /// </summary>
    /// <exception cref="KeyNotFoundException">When a decision id does not exist in the decision library.</exception>
    /// <seealso cref="AddDecision" />
    //TODO: Abstract DecisionAsset
    public TransmutableList<string, GameAsm::DecisionAsset> Decisions =>
        _decisions ??= new TransmutableList<string, GameAsm::DecisionAsset>(
            () => Raw.decision_ids?.Count ?? 0,
            i => Raw.decision_ids[i],
            (i, id) =>
            {
                if (!GameAsm::AssetManager.decisions_library.dict.TryGetValue(id, out var decision))
                    throw new KeyNotFoundException($"Decision with id '{id}' does not exist in the decision library.");
                Raw.decision_ids[i] = id;
                Raw.decisions_assets[i] = decision;
            },
            (i, id) =>
            {
                if (!GameAsm::AssetManager.decisions_library.dict.TryGetValue(id, out var decision))
                    throw new KeyNotFoundException($"Decision with id '{id}' does not exist in the decision library.");
                List<GameAsm::DecisionAsset> assets = Raw.decisions_assets?.ToList() ?? [];
                assets.Insert(i, decision);
                Raw.decision_ids.Insert(i, decision.id);
                Raw.decisions_assets = assets.ToArray();
            },
            i =>
            {
                List<GameAsm::DecisionAsset> assets = Raw.decisions_assets.ToList();
                assets.RemoveAt(i);
                Raw.decision_ids.RemoveAt(i);
                Raw.decisions_assets = assets.ToArray();
            },
            () =>
            {
                Raw.decision_ids = [];
                Raw.decisions_assets = [];
            },
            id => GameAsm::AssetManager.decisions_library.dict.TryGetValue(id, out var decision)
                ? decision
                : throw new KeyNotFoundException($"Decision with id '{id}' does not exist in the decision library."),
            asset => asset.id);

    /// <summary>
    ///     Allows actors to perform special combat actions when this augmentation is active.
    /// </summary>
    /// <seealso cref="AddCombatAction" />
    //TODO: Abstract CombatActionHolder, sync with combat_actions_ids
    public GameAsm::CombatActionHolder CombatActions => Raw.combat_actions;

    /// <summary>
    ///     Adds a decision to the decision pool.
    /// </summary>
    /// <param name="decision">The decision to add.</param>
    //TODO: Abstract DecisionAsset
    public void AddDecision(GameAsm::DecisionAsset decision)
    {
        Raw.addDecision(decision.id);
    }

    /// <summary>
    ///     Adds a combat action to the combat action pool.
    /// </summary>
    /// <param name="combatAction">The combat action to add.</param>
    //TODO: Abstract CombatActionAsset
    public void AddCombatAction(GameAsm::CombatActionAsset combatAction)
    {
        Raw.addCombatAction(combatAction.id);
    }

    /// <summary>
    ///     Adds a spell to the spell pool.
    /// </summary>
    /// <param name="spell">The spell to add.</param>
    //TODO: Abstract SpellAsset
    public void AddSpell(GameAsm::SpellAsset spell)
    {
        Raw.addSpell(spell.id);
    }
}