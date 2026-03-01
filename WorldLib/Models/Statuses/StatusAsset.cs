using System;
using System.Collections.Generic;
using UnityEngine;
using WorldLib.Models.Delegates;
using WorldLib.Models.Generic;
using WorldLib.Utils;

namespace WorldLib.Models.Statuses;

extern alias GameAsm;

/// <summary>
///     Represents a static asset for a type of status. This is global.
/// </summary>
public class StatusAsset : AbstractionOf<GameAsm::StatusAsset>, IAsset
{
    private TransmutableList<string, GameAsm::StatusAsset>? _oppositeStatuses;
    private TransmutableList<string, GameAsm::ActorTrait>? _oppositeTraits;
    private TransmutableList<string, GameAsm::StatusAsset>? _removeStatuses;


    /// <summary>
    ///     Allows overriding the sprite rendered for this effect in world space.
    /// </summary>
    /// <remarks>
    ///     If multiple delegates are registered, all will be invoked and the last return value
    ///     in the multicast invocation chain will be used by the game.
    ///     Returning <c>null</c> will allow the default sprite to be used.
    /// </remarks>
    public DelegateBridge<GetEffectSprite, GameAsm::GetEffectSprite> GetOverrideSprite;


    /// <summary>
    ///     Allows overriding the world-space position of the effect sprite.
    /// </summary>
    /// <remarks>
    ///     If multiple delegates are registered, all will be invoked and the last return value
    ///     in the multicast invocation chain will be used by the game.
    /// </remarks>
    public DelegateBridge<GetEffectSpritePosition, GameAsm::GetEffectSpritePosition> GetOverrideSpritePosition;


    /// <summary>
    ///     Allows overriding the UI-space position of the effect sprite.
    /// </summary>
    /// <remarks>
    ///     This affects rendering inside the UI (e.g., status icons above actors).
    ///     If multiple delegates are registered, all will be invoked and the last return value
    ///     in the multicast invocation chain will be used by the game.
    /// </remarks>
    public DelegateBridge<GetEffectSpritePositionUI, GameAsm::GetEffectSpritePositionUI> GetOverrideSpritePositionUI;


    /// <summary>
    ///     Allows overriding the Z-axis rotation of the effect sprite in world space.
    /// </summary>
    /// <remarks>
    ///     The returned value represents rotation in degrees.
    ///     If multiple delegates are registered, all will be invoked and the last return value
    ///     in the multicast invocation chain will be used by the game.
    /// </remarks>
    public DelegateBridge<GetEffectSpriteRotationZ, GameAsm::GetEffectSpriteRotationZ> GetOverrideSpriteRotationZ;


    /// <summary>
    ///     Allows overriding the Z-axis rotation of the effect sprite in UI space.
    /// </summary>
    /// <remarks>
    ///     The returned value represents rotation in degrees.
    ///     If multiple delegates are registered, all will be invoked and the last return value
    ///     in the multicast invocation chain will be used by the game.
    /// </remarks>
    /// ReSharper disable once InconsistentNaming
    public DelegateBridge<GetEffectSpriteRotationZUI, GameAsm::GetEffectSpriteRotationZUI> GetOverrideSpriteRotationZUI;


    /// <summary>
    ///     Allows overriding the sprite rendered for this effect in UI space.
    /// </summary>
    /// <remarks>
    ///     Returning <c>null</c> will allow the default UI sprite to be used.
    ///     If multiple delegates are registered, all will be invoked and the last return value
    ///     in the multicast invocation chain will be used by the game.
    /// </remarks>
    public DelegateBridge<GetEffectSpriteUI, GameAsm::GetEffectSpriteUI> GetOverrideSpriteUI;


    /// <summary>
    ///     An action that runs at a fixed interval while the status effect remains active.
    /// </summary>
    /// <seealso cref="ActionInterval" />
    /// <remarks>
    ///     Invoked repeatedly according to the effect's configured interval timing.
    /// </remarks>
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnAction;


    /// <summary>
    ///     Invoked when the actor bearing this status effect dies.
    /// </summary>
    /// <remarks>
    ///     Triggered before the status effect is fully cleaned up.
    /// </remarks>
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnDeath;


    /// <summary>
    ///     Invoked when the status effect naturally ends or is removed.
    /// </summary>
    /// <remarks>
    ///     This is called once when the effect finishes, regardless of reason.
    /// </remarks>
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnFinish;


    /// <summary>
    ///     Invoked when the actor bearing this status effect is hit by another actor.
    /// </summary>
    /// <remarks>
    ///     Allows modifying or reacting to incoming hits while the effect is active.
    /// </remarks>
    public DelegateBridge<GetHitAction, GameAsm::GetHitAction> OnHit;


    /// <summary>
    ///     Invoked when this status effect is first applied to an actor.
    /// </summary>
    /// <remarks>
    ///     Triggered immediately after the effect is successfully received.
    /// </remarks>
    public DelegateBridge<WorldAction, GameAsm::WorldAction> OnReceive;

    /// <summary>
    ///     Determines whether this status effect should be visually rendered for a given actor.
    /// </summary>
    /// <remarks>
    ///     This delegate is evaluated before any visual effect instance is created.
    ///     If multiple delegates are registered, all will be invoked and the last return value
    ///     in the multicast invocation chain will be used by the game.
    ///     Returning <c>false</c> prevents the effect from being visually rendered,
    ///     but does not remove or disable the gameplay effect itself.
    /// </remarks>
    public DelegateBridge<RenderEffectCheck, GameAsm::RenderEffectCheck> RenderCheck;

    internal StatusAsset(GameAsm::StatusAsset asset) : base(asset)
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

        GetOverrideSprite = Tooling.Memoized(Base.id + "_get_override_sprite", () =>
            new DelegateBridge<GetEffectSprite, GameAsm::GetEffectSprite>(
                DelegateAdapter.GetEffectSpriteToGame,
                wrapped => { Base.get_override_sprite += wrapped; },
                wrapped => { Base.get_override_sprite -= wrapped; })
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSprite DelegateBridge for Base id {Base.id}");

        GetOverrideSpriteUI = Tooling.Memoized(Base.id + "_get_override_sprite_ui", () =>
            new DelegateBridge<GetEffectSpriteUI, GameAsm::GetEffectSpriteUI>(
                DelegateAdapter.GetEffectSpriteUIToGame,
                wrapped => { Base.get_override_sprite_ui += wrapped; },
                wrapped => { Base.get_override_sprite_ui -= wrapped; })
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpriteUI DelegateBridge for Base id {Base.id}");

        GetOverrideSpritePosition = Tooling.Memoized(Base.id + "_get_override_sprite_position", () =>
            new DelegateBridge<GetEffectSpritePosition, GameAsm::GetEffectSpritePosition>(
                DelegateAdapter.GetEffectSpritePositionToGame,
                wrapped => { Base.get_override_sprite_position += wrapped; },
                wrapped => { Base.get_override_sprite_position -= wrapped; })
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpritePosition DelegateBridge for Base id {Base.id}");

        GetOverrideSpritePositionUI = Tooling.Memoized(Base.id + "_get_override_sprite_position_ui", () =>
            new DelegateBridge<GetEffectSpritePositionUI, GameAsm::GetEffectSpritePositionUI>(
                DelegateAdapter.GetEffectSpritePositionUIToGame,
                wrapped => { Base.get_override_sprite_position_ui += wrapped; },
                wrapped => { Base.get_override_sprite_position_ui -= wrapped; })
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpritePositionUI DelegateBridge for Base id {Base.id}");

        GetOverrideSpriteRotationZ = Tooling.Memoized(Base.id + "_get_override_sprite_rotation_z", () =>
            new DelegateBridge<GetEffectSpriteRotationZ, GameAsm::GetEffectSpriteRotationZ>(
                DelegateAdapter.GetEffectSpriteRotationZToGame,
                wrapped => { Base.get_override_sprite_rotation_z += wrapped; },
                wrapped => { Base.get_override_sprite_rotation_z -= wrapped; })
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpriteRotationZ DelegateBridge for Base id {Base.id}");

        GetOverrideSpriteRotationZUI = Tooling.Memoized(Base.id + "_get_override_sprite_rotation_z_ui", () =>
            new DelegateBridge<GetEffectSpriteRotationZUI, GameAsm::GetEffectSpriteRotationZUI>(
                DelegateAdapter.GetEffectSpriteRotationZUIToGame,
                wrapped => { Base.get_override_sprite_rotation_z_ui += wrapped; },
                wrapped => { Base.get_override_sprite_rotation_z_ui -= wrapped; })
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpriteRotationZUI DelegateBridge for Base id {Base.id}");

        RenderCheck = Tooling.Memoized(Base.id + "_render_check", () =>
            new DelegateBridge<RenderEffectCheck, GameAsm::RenderEffectCheck>(
                DelegateAdapter.RenderEffectCheckToGame,
                wrapped => { Base.render_check += wrapped; },
                wrapped => { Base.render_check -= wrapped; })
        ) ?? throw new InvalidOperationException(
            $"Failed to create RenderCheck DelegateBridge for Base id {Base.id}");
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

    /// <summary>
    ///     The speed of which the game advances through the animation. In seconds.
    /// </summary>
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

    /// <summary>
    ///     Whether to cancel all actor behaviors when the effect is applied.
    /// </summary>
    public bool CancelActorJob
    {
        get => Base.cancel_actor_job;
        set => Base.cancel_actor_job = value;
    }

    /// <summary>
    ///     Whether to not allow getting this status effect if the actor bears the StrongMind trait.
    /// </summary>
    public bool AffectsMind
    {
        get => Base.affects_mind;
        set => Base.affects_mind = value;
    }

    /// <summary>
    ///     The material of the rendered sprite in the world.
    /// </summary>
    public Material Material
    {
        get => Base.material;
        set
        {
            Base.material_id = value.name;
            Base.material = value;
            MaterialRegistry.Register(Base.material);
        }
    }

    /// <summary>
    ///     Whether to illuminate the surrounding area when the status effect is active.
    /// </summary>
    /// <seealso cref="IlluminationSize" />
    public bool IlluminateArea
    {
        get => Base.draw_light_area;
        set => Base.draw_light_area = value;
    }

    /// <summary>
    ///     How large of an area to illuminate. Is a scale and essentially a multiplier of the illuminated area.
    /// </summary>
    /// <seealso cref="IlluminateArea" />
    public float IlluminationSize
    {
        get => Base.draw_light_size;
        set => Base.draw_light_size = value;
    }

    /// <summary>
    ///     Bonus stats the gets applied on top of a sim objects base stats.
    /// </summary>
    public GameAsm::BaseStats Stats
    {
        get => Base.base_stats;
        set => Base.base_stats = value;
    }

    /// <summary>
    ///     Represents the collection of traits that prevent this status from being applied to an actor.
    /// </summary>
    /// <remarks>
    ///     If an actor possesses any trait in this list while this status is being applied,
    ///     the status application will fail.
    /// </remarks>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a trait ID stored internally cannot be resolved in the actor trait library.
    /// </exception>
    // TODO: Abstract ActorTrait.
    public TransmutableList<string, GameAsm::ActorTrait> OppositeTraits =>
        _oppositeTraits ??= new TransmutableList<string, GameAsm::ActorTrait>(
            () => Base.opposite_traits ?? Array.Empty<string>(),
            v => Base.opposite_traits = v,
            id => GameAsm::AssetManager.traits.dict.TryGetValue(id, out var trait)
                ? trait
                : throw new KeyNotFoundException(
                    $"Trait with id '{id}' in opposite_traits does not exist in the trait library."),
            trait => trait.id
        );

    /// <summary>
    ///     Gets or sets the collection of tags that prevent this status from being applied.
    /// </summary>
    /// <remarks>
    ///     If an actor possesses any of these tags at the time of status application,
    ///     the status will fail.
    /// </remarks>
    public string[] OppositeTags
    {
        get => Base.opposite_tags;
        set => Base.opposite_tags = value;
    }

    /// <summary>
    ///     Represents the collection of statuses that prevent this status from being applied.
    /// </summary>
    /// <remarks>
    ///     If an actor currently has any status in this list, applying this status will fail.
    /// </remarks>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a status ID stored internally cannot be resolved in the status asset library.
    /// </exception>
    public TransmutableList<string, GameAsm::StatusAsset> OppositeStatuses =>
        _oppositeStatuses ??= new TransmutableList<string, GameAsm::StatusAsset>(
            () => Base.opposite_status ?? Array.Empty<string>(),
            v => Base.opposite_status = v,
            id => GameAsm::AssetManager.status.dict.TryGetValue(id, out var status)
                ? status
                : throw new KeyNotFoundException(
                    $"Status with id '{id}' in opposite_status does not exist in the status library."),
            status => status.id
        );

    /// <summary>
    ///     Represents the collection of statuses that will be removed from the actor
    ///     once this status is applied.
    /// </summary>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a status ID stored internally cannot be resolved in the status asset library.
    /// </exception>
    public TransmutableList<string, GameAsm::StatusAsset> RemoveStatuses =>
        _removeStatuses ??= new TransmutableList<string, GameAsm::StatusAsset>(
            () => Base.remove_status ?? Array.Empty<string>(),
            v => Base.remove_status = v,
            id => GameAsm::AssetManager.status.dict.TryGetValue(id, out var value)
                ? value
                : throw new KeyNotFoundException(
                    $"Status with id '{id}' in remove_status does not exist in the status library."),
            status => status.id
        );

    /// <summary>
    ///     An array of sprites representing each frame of the animation.
    /// </summary>
    public Sprite[] SpriteList => Base.sprite_list;

    /// <summary>
    ///     The static icon of the status effect.
    /// </summary>
    public Sprite Icon => Base.getSprite();

    /// <summary>
    ///     Represents the localized name of this status asset.
    /// </summary>
    public string LocalizedName => GameAsm::StringExtension.Localize(Base.getLocaleID());

    /// <summary>
    ///     Represents the localized description of this status asset.
    /// </summary>
    public string LocalizedDescription => GameAsm::StringExtension.Localize(Base.getLocaleID());

    /// <summary>
    ///     Whether to use the override sprite getter sprite instead of the animations for the sprite. Applies for both UI and
    ///     the world sprite.
    /// </summary>
    public bool HasOverrideSprite
    {
        get => Base.has_override_sprite;
        set => Base.has_override_sprite = value;
    }

    /// <summary>
    ///     Whether to use the override sprite position getter position instead of the normal position. Applies for both UI and
    ///     the world sprite position.
    /// </summary>
    public bool HasOverrideSpritePosition
    {
        get => Base.has_override_sprite_position;
        set => Base.has_override_sprite_position = value;
    }

    /// <summary>
    ///     Whether to use the override sprite rotation getter z value instead of the normal rotation. Applies for both UI and
    ///     the world sprite rotation.
    /// </summary>
    public bool HasOverrideSpriteRotationZ
    {
        get => Base.has_override_sprite_rotation_z;
        set => Base.has_override_sprite_rotation_z = value;
    }

    /// <inheritdoc />
    public string Id
    {
        get => Base.id;
        set => Base.id = value;
    }

    /// <inheritdoc />
    public int Hash
    {
        get => Base.GetHashCode();
        set => Base.setHash(value);
    }
}