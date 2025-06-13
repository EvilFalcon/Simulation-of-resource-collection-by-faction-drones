using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
	[Game]
	[Scheduler]
	public class UidComponent : IComponent
	{
		[PrimaryEntityIndex] public Uid Value;

		public override string ToString() => Value.ToString();
	}
	
}