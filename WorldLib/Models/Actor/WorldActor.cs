extern alias GameAsm;
using WorldLib.Models.Generic;

namespace WorldLib.Models.Actor;

public class WorldActor : SimObject
{
    internal WorldActor(GameAsm::Actor actor) : base(actor)
    {
    }
}