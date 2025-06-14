using System.Collections.Generic;
using Db.GameObjectsBase;
using Db.GameObjectsBase.Impl;
using Ecs.Views.Linkable.Impl.ResourcesView;
using PdUtils.RandomProvider;
using Zenject;

namespace Game.Services.Pools.Impls.Resouces
{
    public class ResourcesPool : AGenerationPool<EGameResourceType, ResourceView>, IResourcesPool
    {
        private readonly IResourcePrefabsCollection _resourcePrefabsCollection;

        protected override IReadOnlyDictionary<EGameResourceType, IReadOnlyList<ResourceView>> PoolObjectVariants
            => _resourcePrefabsCollection.Prefabs;

        protected override int MaxPoolSize => 25;

        public ResourcesPool(
            IInstantiator container,
            IRandomProvider randomProvider,
            IResourcePrefabsCollection resourcePrefabsCollection
        ) : base(container, randomProvider)
        {
            _resourcePrefabsCollection = resourcePrefabsCollection;

            CreateSubPools();
        }

        public (ResourceView obstacle, int randomIndex) Spawn(EGameResourceType resourceType)
        {
            var (resourceView, randomIndex) = SpawnObject(resourceType);
            return (resourceView, randomIndex);
        }

        public void Despawn(EGameResourceType resourceType, ResourceView obstacle, int randomIndex)
        {
            DespawnObject(resourceType, obstacle, randomIndex);
        }
    }

    public interface IResourcesPool
    {
        void Despawn(EGameResourceType resourceType, ResourceView obstacle, int randomIndex);
        (ResourceView obstacle, int randomIndex) Spawn(EGameResourceType resourceType);
    }
}