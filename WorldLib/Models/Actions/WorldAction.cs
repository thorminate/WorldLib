using WorldLib.Models.Generic;

namespace WorldLib.Models.Actions;

extern alias GameAsm;

public delegate bool WorldAction(SimObject target, GameAsm::WorldTile tile);