using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Components
{
    [Game]
    [Event(EventTarget.Self)]
    public class LocalTimeScaleComponent : IComponent
    {
        public float Value;
    }
}