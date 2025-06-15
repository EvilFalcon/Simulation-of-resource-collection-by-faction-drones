using System.Collections.Generic;
using Db.GameObjectsBase.Impl;
using Ecs.Views.Linkable.Views.ResourcesView;

namespace Db.GameObjectsBase
{
    public interface IResourcePrefabsCollection
    {
        IReadOnlyDictionary<EGameResourceType, IReadOnlyList<ResourceView>> Prefabs { get; }
    }
}