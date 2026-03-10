using UnityEngine;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate for getting the position of a sprite effect in UI space.
/// </summary>
//TODO: Abstract AvatarEffect
public delegate Vector3 GetEffectSpritePositionUI(GameAsm::AvatarEffect effect, int idx);