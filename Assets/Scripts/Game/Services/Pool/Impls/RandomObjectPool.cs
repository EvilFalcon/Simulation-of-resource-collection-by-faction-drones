using System;
using System.Collections.Generic;
using PdUtils.RandomProvider;
using UnityEngine.Pool;

namespace Game.Services.Pool.Impls
{
    public sealed class RandomObjectPool<T> where T : UnityEngine.Object
    {
        private readonly IRandomProvider _randomProvider;
        private readonly ObjectPool<T>[] _pools;

        public RandomObjectPool(
            Func<T, T> createFunc,
            Action<T> actionOnGet,
            Action<T> actionOnRelease,
            Action<T> actionOnDestroy,
            bool collectionCheck,
            int defaultCapacity,
            int maxSize,
            IReadOnlyList<T> collectionOfObjects,
            IRandomProvider randomProvider
        )
        {
            _randomProvider = randomProvider;
            _pools = new ObjectPool<T>[collectionOfObjects.Count];

            for (var i = 0; i < _pools.Length; i++)
            {
                var number = i;
                _pools[i] = new ObjectPool<T>(() =>
                {
                    var prefab = collectionOfObjects[number];
                    return createFunc?.Invoke(prefab);
                }, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize);
            }
        }

        public (T pooledObject, int randomIndex) Get()
        {
            var randomNumber = _randomProvider.Range(0, _pools.Length);

            return (_pools[randomNumber].Get(), randomNumber);
        }

        public void Release(T pooledObject, int randomIndex)
        {
            _pools[randomIndex].Release(pooledObject);
        }

        public void Clear()
        {
            foreach (var pool in _pools)
            {
                pool.Clear();
            }
        }
    }
}