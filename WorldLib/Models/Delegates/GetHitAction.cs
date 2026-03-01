using WorldLib.Models.Generic;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

//TODO: Abstract WorldTile
public delegate bool GetHitAction(SimObject pSelf, SimObject pAttackedBy, GameAsm::WorldTile pTile);