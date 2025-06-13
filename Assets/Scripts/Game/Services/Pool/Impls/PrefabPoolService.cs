using System;
using System.Collections.Generic;
using Ecs.Managers;
using Ecs.Views.Linkable;
using Ecs.Views.Linkable.Impl;
using Game.Utils;
using PdUtils.RandomProvider;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Services.Pool.Impls
{
    public class PrefabPoolService : IPrefabPoolService
    {
        private readonly Dictionary<string, IPrefabMemoryPool> _prefabPools = new();

        public PrefabPoolService(List<IPrefabMemoryPool> prefabPools)
        {
            foreach (var pool in prefabPools)
            {
                _prefabPools.Add(pool.Name, pool);
            }
        }

        public bool Spawn(string prefab, Vector3 position, Quaternion rotation, out IObjectLinkable linkable)
        {
            linkable = null;
            if (!_prefabPools.TryGetValue(prefab, out var pool))
                return false;

            linkable = pool.Spawn();
            linkable.Transform.SetPositionAndRotation(position, rotation);
            return true;
        }

        private void CreatePool(string prefab)
        {
            _prefabPools.Add(prefab, new PrefabMemoryPool(prefab));
        }
    }

    public abstract class AGenerationPool<TType, TObject> : IGenerationPool<TType, TObject>, IClearablePool
        where TType : unmanaged
        where TObject : PoolObjectView
    {
        private readonly IInstantiator _container;
        private readonly IRandomProvider _randomProvider;

        private Dictionary<TType, RandomObjectPool<TObject>> _pools;

        private readonly ReactiveCommand<Uid> _viewOfEntityDestroyedCommand = new();
        public IObservable<Uid> OnViewOfEntityDestroyed => _viewOfEntityDestroyedCommand;

        protected abstract IReadOnlyDictionary<TType, IReadOnlyList<TObject>> PoolObjectVariants { get; }
        protected virtual int MaxPoolSize => 10;

        protected AGenerationPool(
            IInstantiator container,
            IRandomProvider randomProvider
        )
        {
            _container = container;
            _randomProvider = randomProvider;
        }

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
                    view => { view.gameObject.SetActive(false); },
                    view =>
                    {
                        if (view.EntityId != null)
                            _viewOfEntityDestroyedCommand.Execute(view.EntityId.Value);
                        Object.Destroy(view.gameObject);
                    }, false, 3, MaxPoolSize, objectVariantPair.Value, _randomProvider);

                _pools[objectVariantPair.Key] = pool;
            }
        }

        public (TObject objectView, int randomIndex) SpawnObject(TType type)
        {
            return _pools[type].Get();
        }

        public void DespawnObject(TType type, TObject objectView, int randomIndex)
        {
            var pool = _pools[type];
            pool.Release(objectView, randomIndex);
        }

        public void Clear()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }

            _pools.Clear();
        }
    }

    public interface IGenerationPool<in TType, TObject> : IViewPool<TObject>
    {
        (TObject objectView, int randomIndex) SpawnObject(TType type);
        void DespawnObject(TType type, TObject objectView, int randomIndex);
    }

    public interface IClearablePool
    {
        void CreateSubPools();
        public void Clear();
    }

    public interface IViewPool<TObject>
    {
        public IObservable<Uid> OnViewOfEntityDestroyed { get; }
    }
}