using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
	[Game]
	[Action]
	[Input]
	[Signal]
	[Scheduler]
	[Event(EventTarget.Self)]
	public class DestroyedComponent : IComponent
	{
	}
}