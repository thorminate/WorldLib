extern alias GameAsm;
using System.Collections.Generic;
using GameAsm::strings;
using WorldLib.Models.Laws;
using WorldLib.Utils;

namespace WorldLib.Models.Statuses;

/// <summary>
///     A static class for easy access to the game's statuses.
/// </summary>
public class Statuses
{
    /// <inheritdoc cref="AbstractionOf{TStore}.Raw" />
    public static GameAsm::StatusLibrary Raw => GameAsm::AssetManager.status;

    private static void Set(string key, StatusAsset asset)
    {
        Raw.dict[key] = asset.Raw;
    }

    private static StatusAsset Get(string key)
    {
        return Raw.dict.TryGetValue(key, out var asset)
            ? new StatusAsset(asset)
            : throw new KeyNotFoundException($"World law '{key}' not found.");
    }

    #region Statuses

    /// <summary>
    ///     Represents an unusually attractive migrant who may appear near bonfires when the population drops.
    ///     These migrants only appear naturally if the world law <see cref="Laws.HandsomeMigrants" /> is enabled.
    /// </summary>
    public static StatusAsset HandsomeMigrant
    {
        get => Get(S_Status.handsome_migrant);
        set => Set(S_Status.handsome_migrant, value);
    }

    /// <summary>
    ///     This status is given after an actor finishes a plot.
    ///     Works as a cooldown for plots and plots cannot
    ///     naturally get created when this status effect is active.
    /// </summary>
    public static StatusAsset RecoveryPlot
    {
        get => Get(S_Status.recovery_plot);
        set => Set(S_Status.recovery_plot, value);
    }

    /// <summary>
    ///     This is used on actors who have been directly modified by the player recently,
    ///     such as modifying a kings plots or giving a country inspiration.
    /// </summary>
    public static StatusAsset VoicesInMyHead
    {
        get => Get(S_Status.voices_in_my_head);
        set => Set(S_Status.voices_in_my_head, value);
    }

    /// <summary>
    ///     This status is given after an actor finishes casting a spell.
    ///     Works as a cooldown for spells and spells cannot
    ///     be cast when this effect is active.
    /// </summary>
    public static StatusAsset RecoverySpell
    {
        get => Get(S_Status.recovery_spell);
        set => Set(S_Status.recovery_spell, value);
    }

    /// <summary>
    ///     This status is given after an actor finishes socializing.
    ///     Works as a cooldown for socializing and actors cannot
    ///     socialize when this effect is active.
    /// </summary>
    public static StatusAsset RecoverySocial
    {
        get => Get(S_Status.recovery_social);
        set => Set(S_Status.recovery_social, value);
    }

    /// <summary>
    ///     This status is given after an actor finishes using an advanced combat action.
    ///     Works as a cooldown for combat actions and actors cannot
    ///     attack while this effect is active.
    /// </summary>
    public static StatusAsset RecoveryCombatAction
    {
        get => Get(S_Status.recovery_combat_action);
        set => Set(S_Status.recovery_combat_action, value);
    }

    /// <summary>
    ///     Indicates severe hunger.
    /// </summary>
    public static StatusAsset Starving
    {
        get => Get(S_Status.starving);
        set => Set(S_Status.starving, value);
    }

    /// <summary>
    ///     Indicates the actor is drowning.
    ///     Causes health loss over time through <see cref="StatusAsset.OnAction" />.
    ///     If an actor dies while having this effect, a drowning fx will appear where they died.
    /// </summary>
    public static StatusAsset Drowning
    {
        get => Get(S_Status.drowning);
        set => Set(S_Status.drowning, value);
    }

    /// <summary>
    ///     Indicates the actor is asleep.
    /// </summary>
    public static StatusAsset Sleeping
    {
        get => Get(S_Status.sleeping);
        set => Set(S_Status.sleeping, value);
    }

    /// <summary>
    ///     The actor is laughing.
    /// </summary>
    public static StatusAsset Laughing
    {
        get => Get(S_Status.laughing);
        set => Set(S_Status.laughing, value);
    }

    /// <summary>
    ///     The actor is singing.
    /// </summary>
    public static StatusAsset Singing
    {
        get => Get(S_Status.singing);
        set => Set(S_Status.singing, value);
    }

    /// <summary>
    ///     The actor is swearing.
    /// </summary>
    public static StatusAsset Swearing
    {
        get => Get(S_Status.swearing);
        set => Set(S_Status.swearing, value);
    }

    /// <summary>
    ///     The actor is possessed.
    /// </summary>
    public static StatusAsset Possessed
    {
        get => Get(S_Status.possessed);
        set => Set(S_Status.possessed, value);
    }

    /// <summary>
    ///     A follower is possessed.
    /// </summary>
    public static StatusAsset PossessedFollower
    {
        get => Get(S_Status.possessed_follower);
        set => Set(S_Status.possessed_follower, value);
    }

    /// <summary>
    ///     The actor feels a strange urge, compelling them to take unpredictable or irrational actions.
    /// </summary>
    public static StatusAsset StrangeUrge
    {
        get => Get(S_Status.strange_urge);
        set => Set(S_Status.strange_urge, value);
    }

    /// <summary>
    ///     The actor is having a tantrum.
    /// </summary>
    public static StatusAsset Tantrum
    {
        get => Get(S_Status.tantrum);
        set => Set(S_Status.tantrum, value);
    }

    /// <summary>
    ///     The actor is in an egg state (e.g., for creatures that hatch). May be immobile and protected until hatched.
    /// </summary>
    public static StatusAsset Egg
    {
        get => Get(S_Status.egg);
        set => Set(S_Status.egg, value);
    }

    /// <summary>
    ///     The actor is cursed.
    /// </summary>
    public static StatusAsset Cursed
    {
        get => Get(S_Status.cursed);
        set => Set(S_Status.cursed, value);
    }

    /// <summary>
    ///     The actor cannot cast spells temporarily.
    /// </summary>
    public static StatusAsset SpellSilence
    {
        get => Get(S_Status.spell_silence);
        set => Set(S_Status.spell_silence, value);
    }

    /// <summary>
    ///     The actor has a temporary spell boost.
    /// </summary>
    public static StatusAsset SpellBoost
    {
        get => Get(S_Status.spell_boost);
        set => Set(S_Status.spell_boost, value);
    }

    /// <summary>
    ///     The actor is inspired.
    /// </summary>
    public static StatusAsset Inspired
    {
        get => Get(S_Status.inspired);
        set => Set(S_Status.inspired, value);
    }

    /// <summary>
    ///     The actor is confused.
    /// </summary>
    public static StatusAsset Confused
    {
        get => Get(S_Status.confused);
        set => Set(S_Status.confused, value);
    }

    /// <summary>
    ///     The actor's soul has been harvested.
    /// </summary>
    public static StatusAsset SoulHarvested
    {
        get => Get(S_Status.soul_harvested);
        set => Set(S_Status.soul_harvested, value);
    }

    /// <summary>
    ///     The actor is magnetized.
    /// </summary>
    public static StatusAsset Magnetized
    {
        get => Get(S_Status.magnetized);
        set => Set(S_Status.magnetized, value);
    }

    /// <summary>
    ///     The actor is in a rage.
    /// </summary>
    public static StatusAsset Rage
    {
        get => Get(S_Status.rage);
        set => Set(S_Status.rage, value);
    }

    /// <summary>
    ///     The actor is surprised.
    /// </summary>
    public static StatusAsset Surprised
    {
        get => Get(S_Status.surprised);
        set => Set(S_Status.surprised, value);
    }

    /// <summary>
    ///     The actor is on guard.
    /// </summary>
    public static StatusAsset OnGuard
    {
        get => Get(S_Status.on_guard);
        set => Set(S_Status.on_guard, value);
    }

    /// <summary>
    ///     The actor is angry.
    /// </summary>
    public static StatusAsset Angry
    {
        get => Get(S_Status.angry);
        set => Set(S_Status.angry, value);
    }

    /// <summary>
    ///     The actor has just eaten.
    /// </summary>
    public static StatusAsset JustAte
    {
        get => Get(S_Status.just_ate);
        set => Set(S_Status.just_ate, value);
    }

    /// <summary>
    ///     The actor has festive spirit.
    /// </summary>
    public static StatusAsset FestiveSpirit
    {
        get => Get(S_Status.festive_spirit);
        set => Set(S_Status.festive_spirit, value);
    }

    /// <summary>
    ///     The actor is suspicious.
    /// </summary>
    public static StatusAsset BeingSuspicious
    {
        get => Get(S_Status.being_suspicious);
        set => Set(S_Status.being_suspicious, value);
    }

    /// <summary>
    ///     The actor had a good dream.
    /// </summary>
    public static StatusAsset HadGoodDream
    {
        get => Get(S_Status.had_good_dream);
        set => Set(S_Status.had_good_dream, value);
    }

    /// <summary>
    ///     The actor had a bad dream.
    /// </summary>
    public static StatusAsset HadBadDream
    {
        get => Get(S_Status.had_bad_dream);
        set => Set(S_Status.had_bad_dream, value);
    }

    /// <summary>
    ///     The actor had a nightmare.
    /// </summary>
    public static StatusAsset HadNightmare
    {
        get => Get(S_Status.had_nightmare);
        set => Set(S_Status.had_nightmare, value);
    }

    /// <summary>
    ///     The actor is stunned.
    /// </summary>
    public static StatusAsset Stunned
    {
        get => Get(S_Status.stunned);
        set => Set(S_Status.stunned, value);
    }

    /// <summary>
    ///     The actor experiences afterglow.
    /// </summary>
    public static StatusAsset Afterglow
    {
        get => Get(S_Status.afterglow);
        set => Set(S_Status.afterglow, value);
    }

    /// <summary>
    ///     The actor has fallen in love.
    /// </summary>
    public static StatusAsset FellInLove
    {
        get => Get(S_Status.fell_in_love);
        set => Set(S_Status.fell_in_love, value);
    }

    /// <summary>
    ///     The actor is pregnant.
    /// </summary>
    public static StatusAsset Pregnant
    {
        get => Get(S_Status.pregnant);
        set => Set(S_Status.pregnant, value);
    }

    /// <summary>
    ///     The actor is pregnant via parthenogenesis.
    /// </summary>
    public static StatusAsset PregnantParthenogenesis
    {
        get => Get(S_Status.pregnant_parthenogenesis);
        set => Set(S_Status.pregnant_parthenogenesis, value);
    }

    /// <summary>
    ///     The actor has a temporary powerup.
    /// </summary>
    public static StatusAsset Powerup
    {
        get => Get(S_Status.powerup);
        set => Set(S_Status.powerup, value);
    }

    /// <summary>
    ///     The actor is enchanted.
    /// </summary>
    public static StatusAsset Enchanted
    {
        get => Get(S_Status.enchanted);
        set => Set(S_Status.enchanted, value);
    }

    /// <summary>
    ///     The actor is slowed.
    /// </summary>
    public static StatusAsset Slowness
    {
        get => Get(S_Status.slowness);
        set => Set(S_Status.slowness, value);
    }

    /// <summary>
    ///     The actor is motivated.
    /// </summary>
    public static StatusAsset Motivated
    {
        get => Get(S_Status.motivated);
        set => Set(S_Status.motivated, value);
    }

    /// <summary>
    ///     The actor is coughing.
    /// </summary>
    public static StatusAsset Cough
    {
        get => Get(S_Status.cough);
        set => Set(S_Status.cough, value);
    }

    /// <summary>
    ///     The actor has ash fever.
    /// </summary>
    public static StatusAsset AshFever
    {
        get => Get(S_Status.ash_fever);
        set => Set(S_Status.ash_fever, value);
    }

    /// <summary>
    ///     The actor is caffeinated.
    /// </summary>
    public static StatusAsset Caffeinated
    {
        get => Get(S_Status.caffeinated);
        set => Set(S_Status.caffeinated, value);
    }

    /// <summary>
    ///     The actor is frozen.
    /// </summary>
    public static StatusAsset Frozen
    {
        get => Get(S_Status.frozen);
        set => Set(S_Status.frozen, value);
    }

    /// <summary>
    ///     The actor is shielded.
    /// </summary>
    public static StatusAsset Shield
    {
        get => Get(S_Status.shield);
        set => Set(S_Status.shield, value);
    }

    /// <summary>
    ///     The actor is burning.
    /// </summary>
    public static StatusAsset Burning
    {
        get => Get(S_Status.burning);
        set => Set(S_Status.burning, value);
    }

    /// <summary>
    ///     The actor is poisoned.
    /// </summary>
    public static StatusAsset Poisoned
    {
        get => Get(S_Status.poisoned);
        set => Set(S_Status.poisoned, value);
    }

    /// <summary>
    ///     The actor is invincible.
    /// </summary>
    public static StatusAsset Invincible
    {
        get => Get(S_Status.invincible);
        set => Set(S_Status.invincible, value);
    }

    /// <summary>
    ///     The actor can dodge attacks.
    /// </summary>
    public static StatusAsset Dodge
    {
        get => Get(S_Status.dodge);
        set => Set(S_Status.dodge, value);
    }

    /// <summary>
    ///     The actor can dash.
    /// </summary>
    public static StatusAsset Dash
    {
        get => Get(S_Status.dash);
        set => Set(S_Status.dash, value);
    }

    /// <summary>
    ///     The actor is taking roots.
    /// </summary>
    public static StatusAsset TakingRoots
    {
        get => Get(S_Status.taking_roots);
        set => Set(S_Status.taking_roots, value);
    }

    /// <summary>
    ///     The actor is uprooting.
    /// </summary>
    public static StatusAsset Uprooting
    {
        get => Get(S_Status.uprooting);
        set => Set(S_Status.uprooting, value);
    }

    #endregion
}