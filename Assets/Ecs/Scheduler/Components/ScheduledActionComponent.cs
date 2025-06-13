using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Components
{
    [Scheduler]
    public class ScheduledActionComponent : IComponent
    {
        public System.Action Value;
    }
}