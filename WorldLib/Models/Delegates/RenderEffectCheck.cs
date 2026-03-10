namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate that determines whether something should be visually rendered for a given actor.
/// </summary>
public delegate bool RenderEffectCheck(GameAsm::ActorAsset asset);