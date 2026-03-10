using WorldLib.Models.Objects;

namespace WorldLib.Models.Delegates;

extern alias GameAsm;

/// <summary>
///     Represents an action that occurs in the world somewhere, like an actor dying.
/// </summary>
//TODO: Abstract WorldTile
public delegate bool WorldAction(SimObject<GameAsm::BaseSimObject> target, GameAsm::WorldTile tile);