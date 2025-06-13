using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Components
{
    [Scheduler]
    public class IntervalAccumulatorComponent : IComponent
    {
        public float Value;
    }
}