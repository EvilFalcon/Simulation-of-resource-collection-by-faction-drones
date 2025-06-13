using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
	[Game]
	[Event(EventTarget.Self)]
	[Event(EventTarget.Self, EventType.Removed)]
	public class CountComponent : IComponent
	{
		public int Value;
	}
}