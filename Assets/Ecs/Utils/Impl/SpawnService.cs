using System;
using Db.GameObjectsBase;
using Ecs.Views.Linkable;
using Game.Services.Pools;
using UnityEngine;
using Zenject;

namespace Ecs.Utils.Impl
{
    public class SpawnService : ISpawnService<GameEntity, IObjectLinkable>
    {
        private readonly DiContainer _container;
        private readonly IPrefabPoolService _prefabPoolService;
        private readonly IPrefabsBase _unitBase;

        public SpawnService(
            DiContainer container,
            IPrefabsBase unitBase,
            IPrefabPoolService prefabPoolService
        )
        {
            _container = container;
            _unitBase = unitBase;
            _prefabPoolService = prefabPoolService;
        }

        #region ISpawnService<GameEntity,IObjectLinkable> Members

        public IObjectLinkable Spawn(GameEntity entity)
        {
            var position = entity.HasPosition ? entity.Position.Value : Vector3.zero;

            if (!entity.HasCorePrefab)
                throw new Exception($"[SpawnService] Can't instantiate entity with uid: " + entity);

            var prefabName = entity.CorePrefab.Value.ToString();
            return _prefabPoolService.Spawn(prefabName, position, Quaternion.identity, out var linkable)
                ? linkable
                : InstantiateLinkableView(position, _unitBase.Get(prefabName));
        }

        #endregion

        private IObjectLinkable InstantiateLinkableView(Vector3 position, GameObject prefab)
        {
            var gameObject = _container.InstantiatePrefab(prefab, position, Quaternion.identity, null);
            return gameObject.GetComponent<IObjectLinkable>();
        }
    }
}