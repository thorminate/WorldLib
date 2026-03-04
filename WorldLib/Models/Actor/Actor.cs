extern alias GameAsm;
using WorldLib.Models.Generic;

namespace WorldLib.Models.Actor;

public class Actor : SimObject
{
    internal Actor(GameAsm::Actor actor) : base(actor)
    {
    }
}