namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate for getting the z rotation of a sprite effect in UI space.
/// </summary>
//TODO: Abstract AvatarEffect
// ReSharper disable once InconsistentNaming
public delegate float GetEffectSpriteRotationZUI(GameAsm::AvatarEffect effect, int idx);