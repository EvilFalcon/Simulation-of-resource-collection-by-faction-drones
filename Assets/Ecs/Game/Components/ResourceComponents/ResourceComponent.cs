using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.ResourceComponents
{
    [Game]
    public class ResourceComponent : IComponent
    {
        public int Amount;
        public EGameResourceType ResourceType;
    }
}