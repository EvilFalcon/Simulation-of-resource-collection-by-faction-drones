using Db;
using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
	[Game]
	public class CorePrefabComponent : IComponent
	{
		public EObjectType Value;
	}
}