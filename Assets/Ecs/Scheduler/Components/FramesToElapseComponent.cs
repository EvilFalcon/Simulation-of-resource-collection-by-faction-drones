using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Components
{
    [Scheduler]
    public class FramesToElapseComponent : IComponent
    {
        public int Value;
    }
}