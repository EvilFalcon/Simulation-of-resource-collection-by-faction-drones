using Db;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
	[Game]
	public class CorePrefabComponent : IComponent
	{
		public EObjectType Value;
	}
}