using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    public class ResourcePrefabComponent : IComponent
    {
        public EGameResourceType Value;
    }
}