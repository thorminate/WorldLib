using System;
using UnityEngine;
using WorldLib.Models.Actions;
using WorldLib.Models.Generic;
using WorldLib.Utils;

namespace WorldLib.Models.Status;

extern alias GameAsm;

public class WorldStatusAsset : AbstractionOf<GameAsm::StatusAsset>, IAsset
{
    /// <summary>
    ///     An action that runs at a set interval while the effect is active.
    /// </summary>
    /// <seealso cref="ActionInterval" />
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnAction;

    /// <summary>
    ///     Gets triggered when an actor bearing the effect dies.
    /// </summary>
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnDeath;

    /// <summary>
    ///     Gets triggered when the effect ends.
    /// </summary>
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnFinish;

    /// <summary>
    ///     Gets triggered when someone hits the actor bearing the status effect.
    /// </summary>
    public DelegateBridge<GetHitAction, GameAsm::GetHitAction> OnHit;

    /// <summary>
    ///     Gets triggered when an actor receives this status effect.
    /// </summary>
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnReceive;

    internal WorldStatusAsset(GameAsm::StatusAsset asset) : base(asset)
    {
        OnFinish = Tooling.Memoized(Base.id + "_on_finish", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Base.action_finish += wrapped; },
                wrapped => { Base.action_finish -= wrapped; })
        ) ?? throw new InvalidOperationException($"Failed to create OnFinish DelegateBridge for Base id {Base.id}");

        OnDeath = Tooling.Memoized(Base.id + "_on_death", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Base.action_death += wrapped; },
                wrapped => { Base.action_death -= wrapped; })
        ) ?? throw new InvalidOperationException($"Failed to create OnDeath DelegateBridge for Base id {Base.id}");

        OnAction = Tooling.Memoized(Base.id + "_on_action", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Base.action += wrapped; },
                wrapped => { Base.action -= wrapped; })
        ) ?? throw new InvalidOperationException($"Failed to create OnAction DelegateBridge for Base id {Base.id}");

        OnHit = Tooling.Memoized(Base.id + "_on_hit", () =>
            new DelegateBridge<GetHitAction, GameAsm::GetHitAction>(
                DelegateAdapter.HitActionToGame,
                wrapped => { Base.action_get_hit += wrapped; },
                wrapped => { Base.action_get_hit -= wrapped; })
        ) ?? throw new InvalidOperationException($"Failed to create OnHit DelegateBridge for Base id {Base.id}");

        OnReceive = Tooling.Memoized(Base.id + "_on_receive", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Base.action_on_receive += wrapped; },
                wrapped => { Base.action_on_receive -= wrapped; })
        ) ?? throw new InvalidOperationException($"Failed to create OnReceive DelegateBridge for Base id {Base.id}");
    }

    /// <summary>
    ///     The interval at which the <see cref="OnAction" /> delegate is triggered.
    /// </summary>
    public float ActionInterval
    {
        get => Base.action_interval;
        set => Base.action_interval = value;
    }

    /// <summary>
    ///     The tier of the status effect. Is used with <see cref="ActorAsset.allowed_status_tiers" />
    /// </summary>
    public StatusTier Tier
    {
        get => StatusTierHelper.FromGame(Base.tier);
        set => Base.tier = StatusTierHelper.ToGame(value);
    }

    /// <summary>
    ///     Whether this status is curable
    /// </summary>
    public bool CanBeCured
    {
        get => Base.can_be_cured;
        set => Base.can_be_cured = value;
    }

    /// <summary>
    ///     The duration of this effect.
    /// </summary>
    public float Duration
    {
        get => Base.duration;
        set => Base.duration = value;
    }

    /// <summary>
    ///     Whether to allow resetting the timer when <see cref="SimObject.AddStatus" /> is called.
    /// </summary>
    public bool AllowTimerReset
    {
        get => Base.allow_timer_reset;
        set => Base.allow_timer_reset = value;
    }

    /// <summary>
    ///     The id of the texture of the bubble above the actor.
    /// </summary>
    public string Texture
    {
        get => Base.texture;
        set => Base.texture = value;
    }

    /// <summary>
    ///     Whether to start the animation on a random frame in the sprite pool. If <see cref="Animated" /> is false, it will
    ///     pick a random frame and stay like that.
    /// </summary>
    public bool RandomFrame
    {
        get => Base.random_frame;
        set => Base.random_frame = value;
    }

    /// <summary>
    ///     Whether the sprite can be flipped. If so, it copies the actor flip state. Will not work for non-actors.
    /// </summary>
    public bool CanBeFlipped
    {
        get => Base.can_be_flipped;
        set => Base.can_be_flipped = value;
    }

    /// <summary>
    ///     If the sprite should be animated.
    /// </summary>
    public bool Animated
    {
        get => Base.animated;
        set => Base.animated = value;
    }

    /// <summary>
    ///     Whether to continue animating the sprite even if the world is paused.
    /// </summary>
    public bool IsAnimatedInPause
    {
        get => Base.is_animated_in_pause;
        set => Base.is_animated_in_pause = value;
    }

    /// <summary>
    ///     Whether to loop the animation once the sprite pool index reaches its end.
    /// </summary>
    public bool Loop
    {
        get => Base.loop;
        set => Base.loop = value;
    }

    public float AnimationSpeed
    {
        get => Base.animation_speed;
        set => Base.animation_speed = value;
    }

    /// <summary>
    ///     Used to variate the time between each frame of the animation. Leave at 0 for a fixed interval.
    /// </summary>
    /// <example>
    ///     Putting this on 1 will make the time between each frame <see cref="AnimationSpeed" /> + a range
    ///     between 0 and 1.
    /// </example>
    public float AnimationSpeedRandom
    {
        get => Base.animation_speed_random;
        set => Base.animation_speed_random = value;
    }

    /// <summary>
    ///     The scale of the sprite.
    /// </summary>
    public float Scale
    {
        get => Base.scale;
        set => Base.scale = value;
    }

    /// <summary>
    ///     The X offset of the sprite in world-space.
    /// </summary>
    public float OffsetX
    {
        get => Base.offset_x;
        set => Base.offset_x = value;
    }

    /// <summary>
    ///     The X offset of the sprite in the actor inspector.
    /// </summary>
    public float OffsetXUi
    {
        get => Base.offset_x_ui;
        set => Base.offset_x_ui = value;
    }

    /// <summary>
    ///     The Y offset of the sprite in world-space.
    /// </summary>
    public float OffsetY
    {
        get => Base.offset_y;
        set => Base.offset_y = value;
    }

    /// <summary>
    ///     The Y offset of the sprite in the actor inspector.
    /// </summary>
    public float OffsetYUi
    {
        get => Base.offset_y_ui;
        set => Base.offset_y_ui = value;
    }

    /// <summary>
    ///     Rotation of the sprite in world-space.
    /// </summary>
    public float RotationZ
    {
        get => Base.rotation_z;
        set => Base.rotation_z = value;
    }

    /// <summary>
    ///     Whether to lock and base the rotation on the parent object.
    /// </summary>
    public bool UseParentRotation
    {
        get => Base.use_parent_rotation;
        set => Base.use_parent_rotation = value;
    }

    /// <summary>
    ///     Removes this status effect when the actor is hit.
    /// </summary>
    public bool RemovedOnDamage
    {
        get => Base.removed_on_damage;
        set => Base.removed_on_damage = value;
    }

    /// <summary>
    ///     Z position of the sprite.
    /// </summary>
    public float PositionZ
    {
        get => Base.position_z;
        set => Base.position_z = value;
    }

    public bool RandomFlip
    {
        get => Base.random_flip;
        set => Base.random_flip = value;
    }

    public bool CancelActorJob
    {
        get => Base.cancel_actor_job;
        set => Base.cancel_actor_job = value;
    }

    public bool AffectsMind
    {
        get => Base.affects_mind;
        set => Base.affects_mind = value;
    }

    public Material Material
    {
        get => Base.material;
        set
        {
            Base.material_id = value.name;
            Base.material = value;
        }
    }

    public bool DrawLightArea
    {
        get => Base.draw_light_area;
        set => Base.draw_light_area = value;
    }

    public float DrawLightSize
    {
        get => Base.draw_light_size;
        set => Base.draw_light_size = value;
    }

    public GameAsm::BaseStats Stats
    {
        get => Base.base_stats;
        set => Base.base_stats = value;
    }

    public string Id
    {
        get => Base.id;
        set => Base.id = value;
    }

    public int Hash
    {
        get => Base.GetHashCode();
        set => Base.setHash(value);
    }
}