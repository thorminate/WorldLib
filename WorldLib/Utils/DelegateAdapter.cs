extern alias GameAsm;
using WorldLib.Models.Assets;
using WorldLib.Models.Delegates;
using WorldLib.Models.Objects;
using BaseAugmentationAsset = GameAsm::BaseAugmentationAsset;
using BaseSimObject = GameAsm::BaseSimObject;

namespace WorldLib.Utils;

internal static class DelegateAdapter
{
    internal static GameAsm::WorldAction WorldActionToGame(WorldAction action)
    {
        return (target, tile) =>
        {
            SimObject<BaseSimObject>? wrapped = target != null ? new SimObject<BaseSimObject>(target) : null;
            return wrapped != null
                   && action(wrapped, tile);
        };
    }

    internal static WorldAction WorldActionToLib(GameAsm::WorldAction action)
    {
        return (target, tile) => action(target.Raw, tile);
    }

    internal static GameAsm::WorldActionTrait WorldActionTraitToGame(WorldActionTrait action)
    {
        return (target, augmentation) =>
        {
            NanoObject<GameAsm::NanoObject>? wrappedTarget =
                target != null ? new NanoObject<GameAsm::NanoObject>(target) : null;
            AugmentationAsset<BaseAugmentationAsset>? wrappedAsset = augmentation != null
                ? new AugmentationAsset<BaseAugmentationAsset>(augmentation)
                : null;
            return wrappedTarget != null && wrappedAsset != null
                                         && action(wrappedTarget, wrappedAsset);
        };
    }

    internal static WorldActionTrait WorldActionTraitToLib(GameAsm::WorldActionTrait action)
    {
        return (target, augmentation) => action(target.Raw, augmentation.Raw);
    }

    internal static GameAsm::GetHitAction HitActionToGame(GetHitAction action)
    {
        return (target, attacker, tile) =>
        {
            SimObject<BaseSimObject>? wrappedTarget = target != null ? new SimObject<BaseSimObject>(target) : null;
            SimObject<BaseSimObject>? wrappedAttacker =
                attacker != null ? new SimObject<BaseSimObject>(attacker) : null;
            return
                wrappedAttacker != null
                && wrappedTarget != null
                && action(wrappedTarget, wrappedAttacker, tile);
        };
    }

    internal static GetHitAction HitActionToLib(GameAsm::GetHitAction action)
    {
        return (target, attacker, tile) => action(target.Raw, attacker.Raw, tile);
    }

    internal static GameAsm::GetEffectSprite GetEffectSpriteToGame(GetEffectSprite action)
    {
        return (target, idx) =>
            action(target != null ? new SimObject<BaseSimObject>(target) : null!, idx);
    }

    internal static GetEffectSprite GetEffectSpriteToLib(GameAsm::GetEffectSprite action)
    {
        return (target, idx) =>
            action(target.Raw, idx);
    }

    internal static GameAsm::GetEffectSpriteUI GetEffectSpriteUIToGame(GetEffectSpriteUI action)
    {
        return (effect, idx) =>
            action(effect, idx);
    }

    internal static GetEffectSpriteUI GetEffectSpriteUIToLib(GameAsm::GetEffectSpriteUI action)
    {
        return (effect, idx) =>
            action(effect, idx);
    }

    internal static GameAsm::GetEffectSpritePosition GetEffectSpritePositionToGame(GetEffectSpritePosition action)
    {
        return (target, idx) =>
            action(target != null ? new SimObject<BaseSimObject>(target) : null!, idx);
    }

    internal static GetEffectSpritePosition GetEffectSpritePositionToLib(GameAsm::GetEffectSpritePosition action)
    {
        return (target, idx) =>
            action(target.Raw, idx);
    }

    internal static GameAsm::GetEffectSpritePositionUI GetEffectSpritePositionUIToGame(GetEffectSpritePositionUI action)
    {
        return (effect, idx) =>
            action(effect, idx);
    }

    internal static GetEffectSpritePositionUI GetEffectSpritePositionUIToLib(GameAsm::GetEffectSpritePositionUI action)
    {
        return (effect, idx) =>
            action(effect, idx);
    }

    internal static GameAsm::GetEffectSpriteRotationZ GetEffectSpriteRotationZToGame(GetEffectSpriteRotationZ action)
    {
        return (target, idx) =>
            action(target != null ? new SimObject<BaseSimObject>(target) : null!, idx);
    }

    internal static GetEffectSpriteRotationZ GetEffectSpriteRotationZToLib(GameAsm::GetEffectSpriteRotationZ action)
    {
        return (target, idx) =>
            action(target.Raw, idx);
    }

    // ReSharper disable InconsistentNaming
    internal static GameAsm::GetEffectSpriteRotationZUI GetEffectSpriteRotationZUIToGame(
        GetEffectSpriteRotationZUI action)
    {
        return (effect, idx) =>
            action(effect, idx);
    }

    internal static GetEffectSpriteRotationZUI GetEffectSpriteRotationZUIToLib(
        GameAsm::GetEffectSpriteRotationZUI action)
    {
        return (effect, idx) =>
            action(effect, idx);
    }

    internal static GameAsm::RenderEffectCheck RenderEffectCheckToGame(RenderEffectCheck action)
    {
        return asset => action(asset);
    }

    internal static RenderEffectCheck RenderEffectCheckToLib(GameAsm::RenderEffectCheck action)
    {
        return asset => action(asset);
    }
}