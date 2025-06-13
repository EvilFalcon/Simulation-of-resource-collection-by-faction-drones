using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Components
{
    [Scheduler]
    public class NameComponent : IComponent
    {
        public string Value;
    }
}