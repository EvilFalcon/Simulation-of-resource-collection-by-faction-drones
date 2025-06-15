using System;
using Db.GameObjectsBase.Impl;
using Ecs.Views.Linkable.Views.ResourcesView;

namespace Game.Services.Pools
{
    public interface IResourcesPool
    {
        IObservable<UniRx.Unit> OnViewReleased { get; }
        void Despawn(EGameResourceType resourceType, GameEntity entity, int randomIndex);
        (ResourceView obstacle, int randomIndex) Spawn(EGameResourceType resourceType);
    }
}