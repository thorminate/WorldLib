using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldLib.Models.Assets;
using WorldLib.Models.Delegates;
using WorldLib.Models.Objects;
using WorldLib.Registries;
using WorldLib.Utils;

namespace WorldLib.Models.Statuses;

extern alias GameAsm;

/// <summary>
///     Represents a static asset for a type of status. This is global.
/// </summary>
public class StatusAsset : Asset<GameAsm::StatusAsset>
{
    private TransmutableList<string, StatusAsset>? _oppositeStatuses;
    private TransmutableList<string, GameAsm::ActorTrait>? _oppositeTraits;
    private TransmutableList<string, StatusAsset>? _removeStatuses;


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
        OnFinish = Tooling.Memoized(Raw.id + "_on_finish", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Raw.action_finish += wrapped; },
                wrapped => { Raw.action_finish -= wrapped; },
                () => Raw.action_finish?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException($"Failed to create OnFinish DelegateBridge for Base id {Raw.id}");

        OnDeath = Tooling.Memoized(Raw.id + "_on_death", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Raw.action_death += wrapped; },
                wrapped => { Raw.action_death -= wrapped; },
                () => Raw.action_death?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException($"Failed to create OnDeath DelegateBridge for Base id {Raw.id}");

        OnAction = Tooling.Memoized(Raw.id + "_on_action", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Raw.action += wrapped; },
                wrapped => { Raw.action -= wrapped; },
                () => Raw.action?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException($"Failed to create OnAction DelegateBridge for Base id {Raw.id}");

        OnHit = Tooling.Memoized(Raw.id + "_on_hit", () =>
            new DelegateBridge<GetHitAction, GameAsm::GetHitAction>(
                DelegateAdapter.HitActionToGame,
                wrapped => { Raw.action_get_hit += wrapped; },
                wrapped => { Raw.action_get_hit -= wrapped; },
                () => Raw.action_get_hit?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException($"Failed to create OnHit DelegateBridge for Base id {Raw.id}");

        OnReceive = Tooling.Memoized(Raw.id + "_on_receive", () =>
            new DelegateBridge<WorldAction, GameAsm::WorldAction>(
                DelegateAdapter.WorldActionToGame,
                wrapped => { Raw.action_on_receive += wrapped; },
                wrapped => { Raw.action_on_receive -= wrapped; },
                () => Raw.action_on_receive?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException($"Failed to create OnReceive DelegateBridge for Base id {Raw.id}");

        GetOverrideSprite = Tooling.Memoized(Raw.id + "_get_override_sprite", () =>
            new DelegateBridge<GetEffectSprite, GameAsm::GetEffectSprite>(
                DelegateAdapter.GetEffectSpriteToGame,
                wrapped => { Raw.get_override_sprite += wrapped; },
                wrapped => { Raw.get_override_sprite -= wrapped; },
                () => Raw.get_override_sprite?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSprite DelegateBridge for Base id {Raw.id}");

        GetOverrideSpriteUI = Tooling.Memoized(Raw.id + "_get_override_sprite_ui", () =>
            new DelegateBridge<GetEffectSpriteUI, GameAsm::GetEffectSpriteUI>(
                DelegateAdapter.GetEffectSpriteUIToGame,
                wrapped => { Raw.get_override_sprite_ui += wrapped; },
                wrapped => { Raw.get_override_sprite_ui -= wrapped; },
                () => Raw.get_override_sprite_ui?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpriteUI DelegateBridge for Base id {Raw.id}");

        GetOverrideSpritePosition = Tooling.Memoized(Raw.id + "_get_override_sprite_position", () =>
            new DelegateBridge<GetEffectSpritePosition, GameAsm::GetEffectSpritePosition>(
                DelegateAdapter.GetEffectSpritePositionToGame,
                wrapped => { Raw.get_override_sprite_position += wrapped; },
                wrapped => { Raw.get_override_sprite_position -= wrapped; },
                () => Raw.get_override_sprite_position?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpritePosition DelegateBridge for Base id {Raw.id}");

        GetOverrideSpritePositionUI = Tooling.Memoized(Raw.id + "_get_override_sprite_position_ui", () =>
            new DelegateBridge<GetEffectSpritePositionUI, GameAsm::GetEffectSpritePositionUI>(
                DelegateAdapter.GetEffectSpritePositionUIToGame,
                wrapped => { Raw.get_override_sprite_position_ui += wrapped; },
                wrapped => { Raw.get_override_sprite_position_ui -= wrapped; },
                () => Raw.get_override_sprite_position_ui?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpritePositionUI DelegateBridge for Base id {Raw.id}");

        GetOverrideSpriteRotationZ = Tooling.Memoized(Raw.id + "_get_override_sprite_rotation_z", () =>
            new DelegateBridge<GetEffectSpriteRotationZ, GameAsm::GetEffectSpriteRotationZ>(
                DelegateAdapter.GetEffectSpriteRotationZToGame,
                wrapped => { Raw.get_override_sprite_rotation_z += wrapped; },
                wrapped => { Raw.get_override_sprite_rotation_z -= wrapped; },
                () => Raw.get_override_sprite_rotation_z?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpriteRotationZ DelegateBridge for Base id {Raw.id}");

        GetOverrideSpriteRotationZUI = Tooling.Memoized(Raw.id + "_get_override_sprite_rotation_z_ui", () =>
            new DelegateBridge<GetEffectSpriteRotationZUI, GameAsm::GetEffectSpriteRotationZUI>(
                DelegateAdapter.GetEffectSpriteRotationZUIToGame,
                wrapped => { Raw.get_override_sprite_rotation_z_ui += wrapped; },
                wrapped => { Raw.get_override_sprite_rotation_z_ui -= wrapped; },
                () => Raw.get_override_sprite_rotation_z_ui?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException(
            $"Failed to create GetOverrideSpriteRotationZUI DelegateBridge for Base id {Raw.id}");

        RenderCheck = Tooling.Memoized(Raw.id + "_render_check", () =>
            new DelegateBridge<RenderEffectCheck, GameAsm::RenderEffectCheck>(
                DelegateAdapter.RenderEffectCheckToGame,
                wrapped => { Raw.render_check += wrapped; },
                wrapped => { Raw.render_check -= wrapped; },
                () => Raw.render_check?.GetInvocationList() ?? Array.Empty<Delegate>())
        ) ?? throw new InvalidOperationException($"Failed to create RenderCheck DelegateBridge for Base id {Raw.id}");
    }

    /// <summary>
    ///     The interval at which the <see cref="OnAction" /> delegate is triggered.
    /// </summary>
    public float ActionInterval
    {
        get => Raw.action_interval;
        set => Raw.action_interval = value;
    }

    /// <summary>
    ///     The tier of the status effect. Is used with <see cref="ActorAsset.allowed_status_tiers" />
    /// </summary>
    public StatusTier Tier
    {
        get => StatusTierHelper.FromGame(Raw.tier);
        set => Raw.tier = StatusTierHelper.ToGame(value);
    }

    /// <summary>
    ///     Whether this status is curable
    /// </summary>
    public bool CanBeCured
    {
        get => Raw.can_be_cured;
        set => Raw.can_be_cured = value;
    }

    /// <summary>
    ///     The duration of this effect.
    /// </summary>
    public float Duration
    {
        get => Raw.duration;
        set => Raw.duration = value;
    }

    /// <summary>
    ///     Whether to allow resetting the expiration timer when <see cref="SimObject{TAbstracts}.AddStatus" /> is called with
    ///     this trait as its parameter.
    /// </summary>
    public bool AllowTimerReset
    {
        get => Raw.allow_timer_reset;
        set => Raw.allow_timer_reset = value;
    }

    /// <summary>
    ///     The id of the texture of the bubble above the actor.
    /// </summary>
    public string Texture
    {
        get => Raw.texture;
        set => Raw.texture = value;
    }

    /// <summary>
    ///     Whether to start the animation on a random frame in the sprite pool. If <see cref="Animated" /> is false, it will
    ///     pick a random frame and stay like that.
    /// </summary>
    public bool RandomFrame
    {
        get => Raw.random_frame;
        set => Raw.random_frame = value;
    }

    /// <summary>
    ///     Whether the sprite can be flipped. If so, it copies the actor flip state. Will not work for non-actors.
    /// </summary>
    public bool CanBeFlipped
    {
        get => Raw.can_be_flipped;
        set => Raw.can_be_flipped = value;
    }

    /// <summary>
    ///     If the sprite should be animated.
    /// </summary>
    public bool Animated
    {
        get => Raw.animated;
        set => Raw.animated = value;
    }

    /// <summary>
    ///     Whether to continue animating the sprite even if the world is paused.
    /// </summary>
    public bool IsAnimatedInPause
    {
        get => Raw.is_animated_in_pause;
        set => Raw.is_animated_in_pause = value;
    }

    /// <summary>
    ///     Whether to loop the animation once the sprite pool index reaches its end.
    /// </summary>
    public bool Loop
    {
        get => Raw.loop;
        set => Raw.loop = value;
    }

    /// <summary>
    ///     The speed of which the game advances through the animation. In seconds.
    /// </summary>
    public float AnimationSpeed
    {
        get => Raw.animation_speed;
        set => Raw.animation_speed = value;
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
        get => Raw.animation_speed_random;
        set => Raw.animation_speed_random = value;
    }

    /// <summary>
    ///     The scale of the sprite.
    /// </summary>
    public float Scale
    {
        get => Raw.scale;
        set => Raw.scale = value;
    }

    /// <summary>
    ///     The X offset of the sprite in world-space.
    /// </summary>
    public float OffsetX
    {
        get => Raw.offset_x;
        set => Raw.offset_x = value;
    }

    /// <summary>
    ///     The X offset of the sprite in the actor inspector.
    /// </summary>
    public float OffsetXUi
    {
        get => Raw.offset_x_ui;
        set => Raw.offset_x_ui = value;
    }

    /// <summary>
    ///     The Y offset of the sprite in world-space.
    /// </summary>
    public float OffsetY
    {
        get => Raw.offset_y;
        set => Raw.offset_y = value;
    }

    /// <summary>
    ///     The Y offset of the sprite in the actor inspector.
    /// </summary>
    public float OffsetYUi
    {
        get => Raw.offset_y_ui;
        set => Raw.offset_y_ui = value;
    }

    /// <summary>
    ///     Rotation of the sprite in world-space.
    /// </summary>
    public float RotationZ
    {
        get => Raw.rotation_z;
        set => Raw.rotation_z = value;
    }

    /// <summary>
    ///     Whether to lock and base the rotation on the parent object.
    /// </summary>
    public bool UseParentRotation
    {
        get => Raw.use_parent_rotation;
        set => Raw.use_parent_rotation = value;
    }

    /// <summary>
    ///     Removes this status effect when the actor is hit.
    /// </summary>
    public bool RemovedOnDamage
    {
        get => Raw.removed_on_damage;
        set => Raw.removed_on_damage = value;
    }

    /// <summary>
    ///     Z position of the sprite.
    /// </summary>
    public float PositionZ
    {
        get => Raw.position_z;
        set => Raw.position_z = value;
    }

    /// <summary>
    ///     Whether to cancel all actor behaviors when the effect is applied.
    /// </summary>
    public bool CancelActorJob
    {
        get => Raw.cancel_actor_job;
        set => Raw.cancel_actor_job = value;
    }

    /// <summary>
    ///     Whether to not allow getting this status effect if the actor bears the StrongMind trait.
    /// </summary>
    public bool AffectsMind
    {
        get => Raw.affects_mind;
        set => Raw.affects_mind = value;
    }

    /// <summary>
    ///     The material of the rendered sprite in the world.
    /// </summary>
    public Material Material
    {
        get => Raw.material;
        set
        {
            Raw.material_id = value.name;
            Raw.material = value;
            Materials.Register(Raw.material);
        }
    }

    /// <summary>
    ///     A decision asset to give an actor when they have this status effect active.
    /// </summary>
    //TODO: Abstract DecisionAsset
    public GameAsm::DecisionAsset Decision => Raw.getDecisionAsset();

    /// <summary>
    ///     Whether to illuminate the surrounding area when the status effect is active.
    /// </summary>
    /// <seealso cref="IlluminationSize" />
    public bool IlluminateArea
    {
        get => Raw.draw_light_area;
        set => Raw.draw_light_area = value;
    }

    /// <summary>
    ///     How large of an area to illuminate. Is a scale and essentially a multiplier of the illuminated area.
    /// </summary>
    /// <seealso cref="IlluminateArea" />
    public float IlluminationSize
    {
        get => Raw.draw_light_size;
        set => Raw.draw_light_size = value;
    }

    /// <summary>
    ///     Bonus stats the gets applied on top of a sim objects base stats.
    /// </summary>
    public GameAsm::BaseStats Stats
    {
        get => Raw.base_stats;
        set => Raw.base_stats = value;
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
            () => Raw.opposite_traits?.Length ?? 0,
            i => Raw.opposite_traits[i],
            (i, id) => Raw.opposite_traits[i] = id,
            (i, id) =>
            {
                List<string> list = Raw.opposite_traits?.ToList() ?? [];
                list.Insert(i, id);
                Raw.opposite_traits = list.ToArray();
            },
            i =>
            {
                List<string> list = Raw.opposite_traits.ToList();
                list.RemoveAt(i);
                Raw.opposite_traits = list.ToArray();
            },
            () => Raw.opposite_traits = [],
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
        get => Raw.opposite_tags;
        set => Raw.opposite_tags = value;
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
    public TransmutableList<string, StatusAsset> OppositeStatuses =>
        _oppositeStatuses ??= new TransmutableList<string, StatusAsset>(
            () => Raw.opposite_status?.Length ?? 0,
            i => Raw.opposite_status[i],
            (i, id) => Raw.opposite_status[i] = id,
            (i, id) =>
            {
                List<string> list = Raw.opposite_status?.ToList() ?? new List<string>();
                list.Insert(i, id);
                Raw.opposite_status = list.ToArray();
            },
            i =>
            {
                List<string> list = Raw.opposite_status.ToList();
                list.RemoveAt(i);
                Raw.opposite_status = list.ToArray();
            },
            () => Raw.opposite_status = [],
            id => GameAsm::AssetManager.status.dict.TryGetValue(id, out var status)
                ? new StatusAsset(status)
                : throw new KeyNotFoundException(
                    $"Status with id '{id}' in opposite_status does not exist in the status library."),
            status => status.Id
        );

    /// <summary>
    ///     Represents the collection of statuses that will be removed from the actor
    ///     once this status is applied.
    /// </summary>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a status ID stored internally cannot be resolved in the status asset library.
    /// </exception>
    public TransmutableList<string, StatusAsset> RemoveStatuses =>
        _removeStatuses ??= new TransmutableList<string, StatusAsset>(
            () => Raw.remove_status?.Length ?? 0,
            i => Raw.remove_status[i],
            (i, id) => Raw.remove_status[i] = id,
            (i, id) =>
            {
                List<string> list = Raw.remove_status?.ToList() ?? new List<string>();
                list.Insert(i, id);
                Raw.remove_status = list.ToArray();
            },
            i =>
            {
                List<string> list = Raw.remove_status.ToList();
                list.RemoveAt(i);
                Raw.remove_status = list.ToArray();
            },
            () => Raw.remove_status = [],
            id => GameAsm::AssetManager.status.dict.TryGetValue(id, out var status)
                ? new StatusAsset(status)
                : throw new KeyNotFoundException(
                    $"Status with id '{id}' in remove_status does not exist in the status library."),
            status => status.Id
        );

    /// <summary>
    ///     An array of sprites representing each frame of the animation.
    /// </summary>
    public Sprite[] SpriteList => Raw.sprite_list;

    /// <summary>
    ///     The static icon of the status effect.
    /// </summary>
    public Sprite Icon
    {
        get => Raw.getSprite();
        set
        {
            Raw.cached_sprite = value;
            Raw.path_icon = value.name;
            Sprites.Register(value);
        }
    }

    /// <summary>
    ///     Represents the localized name of this status asset.
    /// </summary>
    public string LocalizedName => GameAsm::StringExtension.Localize(Raw.getLocaleID());

    /// <summary>
    ///     Represents the localized description of this status asset.
    /// </summary>
    public string LocalizedDescription => GameAsm::StringExtension.Localize(Raw.getLocaleID());

    /// <summary>
    ///     Whether to use the override sprite getter sprite instead of the animations for the sprite. Applies for both UI and
    ///     the world sprite.
    /// </summary>
    public bool HasOverrideSprite
    {
        get => Raw.has_override_sprite;
        set => Raw.has_override_sprite = value;
    }

    /// <summary>
    ///     Whether to use the override sprite position getter position instead of the normal position. Applies for both UI and
    ///     the world sprite position.
    /// </summary>
    public bool HasOverrideSpritePosition
    {
        get => Raw.has_override_sprite_position;
        set => Raw.has_override_sprite_position = value;
    }

    /// <summary>
    ///     Whether to use the override sprite rotation getter z value instead of the normal rotation. Applies for both UI and
    ///     the world sprite rotation.
    /// </summary>
    public bool HasOverrideSpriteRotationZ
    {
        get => Raw.has_override_sprite_rotation_z;
        set => Raw.has_override_sprite_rotation_z = value;
    }

    /// <summary>
    ///     Whether to render the status effect at all.
    /// </summary>
    public bool DoVisualRender
    {
        get => Raw.need_visual_render;
        set => Raw.need_visual_render = value;
    }
}