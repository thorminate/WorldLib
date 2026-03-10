using UnityEngine;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate for getting an effect sprite in UI space.
/// </summary>
//TODO: Abstract AvatarEffect.
public delegate Sprite GetEffectSpriteUI(GameAsm::AvatarEffect effect, int idx);