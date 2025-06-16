using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Units
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class RayComponent : IComponent
    {}
}