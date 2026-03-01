using UnityEngine;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

//TODO: Abstract AvatarEffect
public delegate Vector3 GetEffectSpritePositionUI(GameAsm::AvatarEffect effect, int idx);