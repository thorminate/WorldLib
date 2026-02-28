using WorldLib.Models.Generic;

namespace WorldLib.Models.Actions;

extern alias GameAsm;

//TODO: Abstract WorldTile
public delegate bool GetHitAction(SimObject pSelf, SimObject pAttackedBy, GameAsm::WorldTile pTile);