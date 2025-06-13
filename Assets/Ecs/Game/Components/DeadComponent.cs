using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
	[Game]
	[Event(EventTarget.Self)]
	[Event(EventTarget.Self, EventType.Removed)]
	public class DeadComponent : IComponent
	{
		public override string ToString() => "IsDead";
	}
}