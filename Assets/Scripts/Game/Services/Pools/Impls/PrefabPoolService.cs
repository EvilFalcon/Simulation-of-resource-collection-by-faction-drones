using System.Collections.Generic;
using Ecs.Views.Linkable;
using UnityEngine;

namespace Game.Services.Pools.Impls
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

        #region IPrefabPoolService Members

        public bool Spawn(string prefab, Vector3 position, Quaternion rotation, out IObjectLinkable linkable)
        {
            linkable = null;
            if (!_prefabPools.TryGetValue(prefab, out var pool))
                return false;

            linkable = pool.Spawn();
            linkable.Transform.SetPositionAndRotation(position, rotation);
            return true;
        }

        #endregion
    }
}