extern alias GameAsm;
using WorldLib.Models.Actions;

namespace WorldLib.Models.Generic;

internal static class DelegateAdapter
{
    internal static GameAsm::WorldAction WorldActionToGame(WorldAction action)
    {
        return (target, tile) =>
        {
            var wrapped = target != null ? new SimObject(target) : null;
            return wrapped != null
                   && action(wrapped, tile);
        };
    }

    internal static WorldAction WorldActionToLib(GameAsm::WorldAction action)
    {
        return (target, tile) => action(target?.Base, tile);
    }

    internal static GameAsm::GetHitAction HitActionToGame(GetHitAction action)
    {
        return (target, attacker, tile) =>
        {
            var wrappedTarget = target != null ? new SimObject(target) : null;
            var wrappedAttacker = attacker != null ? new SimObject(attacker) : null;
            return
                wrappedAttacker != null
                && wrappedTarget != null
                && action(wrappedTarget, wrappedAttacker, tile);
        };
    }

    internal static GetHitAction HitActionToLib(GameAsm::GetHitAction action)
    {
        return (target, attacker, tile) => action(target?.Base, attacker?.Base, tile);
    }
}