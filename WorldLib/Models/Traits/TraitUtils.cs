extern alias GameAsm;
using System;
using WorldLib.Models.Actors;

namespace WorldLib.Models.Traits;

internal static class TraitUtils
{
    internal static ITraitsOwner<TThis, TAbstracts> TraitOwnerToPublic<TThis, TAbstracts>(
        GameAsm::ITraitsOwner<TAbstracts> raw)
        where TAbstracts : GameAsm::BaseTrait<TAbstracts>
        where TThis : Trait<TThis, TAbstracts>
    {
        return raw switch
        {
            GameAsm::Actor c => (ITraitsOwner<TThis, TAbstracts>)new Actor(c),
            //GameAsm::Building b => (ITraitsOwner<TAbstracts>)new Building(b),
            _ => throw new NotSupportedException($"No wrapper for {raw.GetType().Name}")
        };
    }

    internal static GameAsm::BaseTraitLibrary<TAbstracts> TraitToLibrary<TAbstracts>()
        where TAbstracts : GameAsm::BaseTrait<TAbstracts>
    {
        if (typeof(TAbstracts) == typeof(GameAsm::ActorTrait))
            return (GameAsm::BaseTraitLibrary<TAbstracts>)(object)GameAsm::AssetManager.traits;
        throw new NotSupportedException($"No wrapper for {typeof(TAbstracts).Name}");
    }

    internal static Trait<TThis, TAbstracts> TraitToPublic<TThis, TAbstracts>(GameAsm::BaseTrait<TAbstracts> raw)
        where TAbstracts : GameAsm::BaseTrait<TAbstracts>
        where TThis : Trait<TThis, TAbstracts>
    {
        return raw switch
        {
            GameAsm::ActorTrait t => (Trait<TThis, TAbstracts>)(object)new ActorTrait(t),
            _ => throw new NotSupportedException($"No wrapper for {raw.GetType().Name}")
        };
    }
}