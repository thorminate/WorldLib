using UnityEngine;
using WorldLib.Models.Objects;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate for getting the position of a sprite effect.
/// </summary>
public delegate Vector3 GetEffectSpritePosition(SimObject<GameAsm::BaseSimObject> obj, int idx);