using UnityEngine;
using WorldLib.Models.Objects;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate for getting an effect sprite.
/// </summary>
public delegate Sprite GetEffectSprite(SimObject<GameAsm::BaseSimObject> obj, int idx);