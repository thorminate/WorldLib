using WorldLib.Models.Objects;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate for getting the z rotation of a sprite effect.
/// </summary>
public delegate float GetEffectSpriteRotationZ(SimObject<GameAsm::BaseSimObject> obj, int idx);