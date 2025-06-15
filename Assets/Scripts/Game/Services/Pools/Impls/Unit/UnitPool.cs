using System;
using System.Collections.Generic;
using Db.GameObjectsBase;
using Db.GameObjectsBase.Impl;
using Ecs.Managers;
using Ecs.Views.Linkable.Views.Units;
using UnityEngine.Pool;
using Zenject;
using UniRx;
using Object = UnityEngine.Object;

namespace Game.Services.Pools.Impls.Unit
{
    public class UnitPool : IUnitPool
    {
        private readonly IInstantiator _container;
        private readonly Dictionary<EFractionType, ObjectPool<UnitView>> _pools;
        private readonly IReadOnlyDictionary<EFractionType, UnitView> _prefabMap;
        private readonly ReactiveCommand<Uid> _viewOfEntityDestroyedCommand = new();

        public UnitPool(
            IInstantiator container,
            IUnitPrefabsCollection unitPrefabsCollection)
        {
            _container = container;
            _prefabMap = unitPrefabsCollection.Prefabs;
            _pools = new Dictionary<EFractionType, ObjectPool<UnitView>>();

            InitializePools();
        }

        #region IUnitPool Members

        public IObservable<Uid> OnViewOfEntityDestroyed => _viewOfEntityDestroyedCommand;

        public UnitView Get(EFractionType type)
        {
            if (!_pools.TryGetValue(type, out var pool))
                throw new KeyNotFoundException($"Pool for {type} not found");

            return pool.Get();
        }

        public void Release(EFractionType type, UnitView view)
        {
            if (!_pools.TryGetValue(type, out var pool))
                throw new KeyNotFoundException($"Pool for {type} not found");

            pool.Release(view);
        }

        #endregion

        private void InitializePools()
        {
            foreach (var kvp in _prefabMap)
            {
                var fractionType = kvp.Key;
                var prefab = kvp.Value;

                _pools[fractionType] = new ObjectPool<UnitView>(
                    createFunc: () => CreateInstance(prefab),
                    actionOnGet: view => view.gameObject.SetActive(true),
                    actionOnRelease: view => view.gameObject.SetActive(false),
                    actionOnDestroy: view => Object.Destroy(view.gameObject),
                    collectionCheck: false,
                    defaultCapacity: 10
                );
            }
        }

        private UnitView CreateInstance(UnitView prefab)
        {
            return _container.InstantiatePrefab(prefab).GetComponent<UnitView>();
        }
    }

    public interface IUnitPool
    {
        IObservable<Uid> OnViewOfEntityDestroyed { get; }
        UnitView Get(EFractionType type);
        void Release(EFractionType type, UnitView view);
    }
}