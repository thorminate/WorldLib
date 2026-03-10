using WorldLib.Models.Assets;
using WorldLib.Models.Objects;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate that is used in conjunction to events relating to augmentations,
///     like when adding a trait to an actor.
/// </summary>
//TODO: Abstract WorldTile
public delegate bool WorldActionTrait(NanoObject<GameAsm::NanoObject> target,
    AugmentationAsset<GameAsm::BaseAugmentationAsset> asset);