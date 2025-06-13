using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Components
{
    [Scheduler]
    public class AccumulatorComponent : IComponent
    {
        public float Value;
    }
}