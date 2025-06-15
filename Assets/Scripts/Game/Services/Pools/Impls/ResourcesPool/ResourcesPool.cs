using System;
using System.Collections.Generic;
using Db.GameObjectsBase;
using Db.GameObjectsBase.Impl;
using Ecs.Views.Linkable.Views.ResourcesView;
using PdUtils.RandomProvider;
using UniRx;
using Zenject;

namespace Game.Services.Pools.Impls.ResourcesPool
{
    public class ResourcesPool : AGenerationPool<EGameResourceType, ResourceView>, IResourcesPool
    {
        private readonly IResourcePrefabsCollection _resourcePrefabsCollection;

        private readonly ReactiveCommand<UniRx.Unit> _viewReleasedCommand = new();

        public ResourcesPool(
            IInstantiator container,
            IRandomProvider randomProvider,
            IResourcePrefabsCollection resourcePrefabsCollection
        ) : base(container, randomProvider)
        {
            _resourcePrefabsCollection = resourcePrefabsCollection;
            CreateSubPools();
        }

        protected override IReadOnlyDictionary<EGameResourceType, IReadOnlyList<ResourceView>> PoolObjectVariants
            => _resourcePrefabsCollection.Prefabs;

        protected override int MaxPoolSize => 25;

        #region IResourcesPool Members

        public IObservable<UniRx.Unit> OnViewReleased => _viewReleasedCommand;

        public (ResourceView obstacle, int randomIndex) Spawn(EGameResourceType resourceType)
        {
            var (resourceView, randomIndex) = SpawnObject(resourceType);
            return (resourceView, randomIndex);
        }

        public void Despawn(EGameResourceType resourceType, GameEntity entity, int randomIndex)
        {
            var resourceView = entity.Link.View;
            entity.Link.View.Unlink();
            entity.Destroy();
            _viewReleasedCommand.Execute(UniRx.Unit.Default);
            DespawnObject(resourceType, (ResourceView)resourceView, randomIndex);
        }

        #endregion
    }
}