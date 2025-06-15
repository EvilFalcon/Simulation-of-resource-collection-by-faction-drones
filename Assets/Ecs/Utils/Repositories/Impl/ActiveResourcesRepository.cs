using System.Collections.Generic;
using UnityEngine;

namespace Ecs.Utils.Repositories.Impl
{
    public class ActiveResourcesRepository : IActiveResourcesRepository
    {
        private readonly Dictionary<Vector3, GameEntity> _resources = new Dictionary<Vector3, GameEntity>();

        #region IActiveResourcesRepository Members

        public void Add(GameEntity entity)
        {
            var position = entity.Position.Value;

            if (!_resources.ContainsKey(position))
            {
                _resources.Add(position, entity);
            }
        }

        public GameEntity Get(Vector3 position)
        {
            _resources.TryGetValue(position, out var entity);
            return entity;
        }

        public GameEntity GetNearestEntity(Vector3 position)
        {
            if (_resources.Count == 0)
                return null;

            Vector3 nearestKey = default;
            var minDistance = float.MaxValue;

            foreach (var (key, _) in _resources)
            {
                var currentDistance = Vector3.Distance(position, key);

                if (!(currentDistance < minDistance))
                    continue;

                minDistance = currentDistance;
                nearestKey = key;
            }

            var entity = _resources[nearestKey];
            _resources.Remove(nearestKey);
            return entity;
        }

        public void Clear()
        {
            _resources.Clear();
        }

        #endregion
    }
}

