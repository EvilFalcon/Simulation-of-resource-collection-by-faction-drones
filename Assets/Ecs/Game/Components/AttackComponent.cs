using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
	[Game]
	[Event(EventTarget.Self)]
	public class AttackComponent : IComponent
	{
		public Uid Uid;
	}
}