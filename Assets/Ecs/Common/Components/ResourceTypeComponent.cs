using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
	[Game]
	[Event(EventTarget.Self)]
	public class ResourceTypeComponent : IComponent
	{
		public EGameResourceType Value;

		public override string ToString() => "ResourceType: " + Value;
	}
}