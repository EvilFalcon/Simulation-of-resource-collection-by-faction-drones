using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Components
{
    [Scheduler]
    public class TimeScaleTargetComponent : IComponent
    {
        public Uid Value;
    }
}