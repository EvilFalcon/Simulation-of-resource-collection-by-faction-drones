using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.ResourceComponents
{
	[Game]
	public class ResourceDataComponent : IComponent
	{
		public int Amount;
		public EGameResourceType ResourceType;
		public override string ToString() => "ResourceType: " + ResourceType;
	}
}