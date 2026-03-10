using WorldLib.Models.Objects;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents a delegate that is run upon a sim object getting hit.
/// </summary>
//TODO: Abstract WorldTile
public delegate bool GetHitAction(SimObject<GameAsm::BaseSimObject> pSelf,
    SimObject<GameAsm::BaseSimObject> pAttackedBy, GameAsm::WorldTile pTile);