using WorldLib.Models.Generic;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

//TODO: Abstract WorldTile
public delegate bool WorldAction(SimObject target, GameAsm::WorldTile tile);