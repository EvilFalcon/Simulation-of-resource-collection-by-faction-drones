using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    public class SpeedComponent : IComponent
    {
        public float Value;
    }
}