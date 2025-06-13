using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Time
{
    [Game]
    public class ResourceComponent : IComponent
    {
        public EGameResourceType ResourceType;
        public int Amount;
    }
}