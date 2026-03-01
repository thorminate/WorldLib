using UnityEngine;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

//TODO: Abstract AvatarEffect.
public delegate Sprite GetEffectSpriteUI(GameAsm::AvatarEffect effect, int idx);