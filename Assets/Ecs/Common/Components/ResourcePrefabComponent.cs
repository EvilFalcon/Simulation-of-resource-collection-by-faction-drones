using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
    [Game]
    public class ResourcePrefabComponent : IComponent
    {
        public EGameResourceType Value;
    }
}