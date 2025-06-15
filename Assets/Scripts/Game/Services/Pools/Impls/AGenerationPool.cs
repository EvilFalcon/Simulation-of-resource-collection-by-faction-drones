using System;
using System.Collections.Generic;
using Ecs.Managers;
using Ecs.Views.Linkable.Views;
using Game.Utils;
using PdUtils.RandomProvider;
using UniRx;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Services.Pools.Impls
{
    public abstract class AGenerationPool<TType, TObject> : IGenerationPool<TType, TObject>, IClearablePool
        where TType : unmanaged
        where TObject : PoolObjectView
    {
        private readonly IInstantiator _container;
        private readonly IRandomProvider _randomProvider;

        private readonly ReactiveCommand<Uid> _viewOfEntityDestroyedCommand = new();

        private Dictionary<TType, RandomObjectPool<TObject>> _pools;

        protected AGenerationPool(
            IInstantiator container,
            IRandomProvider randomProvider
        )
        {
            _container = container;
            _randomProvider = randomProvider;
        }

        protected abstract IReadOnlyDictionary<TType, IReadOnlyList<TObject>> PoolObjectVariants { get; }
        protected virtual int MaxPoolSize => 10;

        public void CreateSubPools()
        {
            _pools = new Dictionary<TType, RandomObjectPool<TObject>>();

            foreach (var objectVariantPair in PoolObjectVariants)
            {
                var pool = new RandomObjectPool<TObject>(
                    prefab => _container.InstantiatePrefab(prefab).GetComponent<TObject>(),
                    view =>
                    {
                        view.gameObject.SetActive(true);
                        view.transform.SetFarAwayPosition();
                    },
                    view =>
                    {
                        view.gameObject.SetActive(false);
                    },
                    view =>
                    {
                        if (view.EntityId != null)
                            _viewOfEntityDestroyedCommand.Execute(view.EntityId.Value);
                        Object.Destroy(view.gameObject);
                    }, false, 3, MaxPoolSize, objectVariantPair.Value, _randomProvider);

                _pools[objectVariantPair.Key] = pool;
            }
        }

        public void Clear()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }

            _pools.Clear();
        }
        
        public IObservable<Uid> OnViewOfEntityDestroyed => _viewOfEntityDestroyedCommand;

        public (TObject objectView, int randomIndex) SpawnObject(TType type)
        {
            return _pools[type].Get();
        }

        public void DespawnObject(TType type, TObject objectView, int randomIndex)
        {
            var pool = _pools[type];
            pool.Release(objectView, randomIndex);
        }
    }
}