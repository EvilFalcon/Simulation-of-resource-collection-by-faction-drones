using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Db.Generation.ResourcesParameters;
using Db.Generation.ResourcesParameters.Impl;
using Ecs.Core.Interfaces;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Ecs.Utils.Repositories;
using Game.Services.Pools;
using Game.Utils;
using PdUtils.RandomProvider;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Services.Spawners.Impl
{
    public class ResourcesSpawner : IResourcesSpawner, ITickedSpawner, IInitializable, IDisposable
    {
        private readonly IActiveResourcesRepository _activeResourcesRepository;
        private readonly GameContext _gameContext;
        private readonly IGameSceneProvider _gameSceneProvider;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IRandomProvider _randomProvider;
        private readonly IResourcesParameters _resourcesParameters;
        private readonly IResourcesPool _resourcesPool;

        private readonly ITimeProvider _timeProvider;
        private IDisposable _disposable;

        private List<SpawnerProcess> _processes = new List<SpawnerProcess>();
        private List<SpawnerProcess> _disposableProcesses = new List<SpawnerProcess>();

        public ResourcesSpawner(
            ITimeProvider timeProvider,
            IResourcesPool resourcesPool,
            IResourcesParameters resourcesParameters,
            IGameSceneProvider gameSceneProvider,
            IRandomProvider randomProvider,
            IActiveResourcesRepository activeResourcesRepository,
            ILinkedEntityRepository linkedEntityRepository,
            GameContext gameContext
            )
        {
            _timeProvider = timeProvider;
            _resourcesPool = resourcesPool;
            _resourcesParameters = resourcesParameters;
            _gameSceneProvider = gameSceneProvider;
            _randomProvider = randomProvider;
            _activeResourcesRepository = activeResourcesRepository;
            _linkedEntityRepository = linkedEntityRepository;
            _gameContext = gameContext;
        }

        #region IDisposable Members

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        #endregion

        #region IInitializable Members

        public void Initialize()
        {
            _disposable = _resourcesPool.OnViewReleased.Subscribe(_ => OnCreate());
        }

        #endregion

        #region IResourcesSpawner Members

        public void Create()
        {
            var terrainEntityTerrainData = _gameContext.TerrainEntity.TerrainData;
            var terrainData = terrainEntityTerrainData.Value;
            var terrainSize = terrainData.size;
            var terrainPos = _gameSceneProvider.Terrain.transform.position;

            var resource = GetRandomResource();
            
            if (resource == null) 
                return;

            for (var attempt = 0; attempt < _resourcesParameters.MaxAttemptsPerResource; attempt++)
            {
                var spawnPosition = new Vector3(
                    terrainPos.x + _randomProvider.Range(0, terrainSize.x),
                    0,
                    terrainPos.z + _randomProvider.Range(0, terrainSize.z)
                );

                spawnPosition.y = _gameContext.TerrainEntity.Terrain.Value.SampleHeight(spawnPosition);

                if (!IsValidSpawnPosition(spawnPosition))
                    continue;
               
                var (resourceView, randomIndex) = _resourcesPool.Spawn(resource.Value.ResourceType);
                var resourceEntity = _gameContext.CreateResource();
                resourceEntity.AddResource(randomIndex);
                resourceEntity.AddResourceData(resource.Value.Amount, resource.Value.ResourceType);
                resourceEntity.AddPosition(spawnPosition);
                resourceView.Link(resourceEntity);
                
                _activeResourcesRepository.Add(resourceEntity);
                _linkedEntityRepository.Add(resourceEntity.Link.View.Transform.GetHashCode(), resourceEntity);
                break;
            }
        }

        #endregion

        #region ITickedSpawner Members

        public void OnTick()
        {
            if (_processes.Count == 0)
                return;
            
            foreach (var process in _processes)
            {
                process.Timer -= _timeProvider.DeltaTime;

                if (process.Timer >= 0)
                    continue;

                process.Action?.Invoke();
                _disposableProcesses.Add(process);
            }

            foreach (var dispolceDrocess in _disposableProcesses)
            {
                _processes.Remove(dispolceDrocess);
            }
        }

        #endregion

        private ResourceConfig? GetRandomResource()
        {
            if (_resourcesParameters.Resources.Count == 0) return null;

            var totalWeight = _resourcesParameters.Resources.Sum(res => res.SpawnWeight);

            var randomValue = _randomProvider.Range(0, totalWeight);
            float currentWeight = 0;

            foreach (var res in _resourcesParameters.Resources)
            {
                currentWeight += res.SpawnWeight;
                if (randomValue <= currentWeight)
                    return res;
            }

            return _resourcesParameters.Resources[0];
        }

        private bool IsValidSpawnPosition(Vector3 position)
        {
            return !Physics.CheckSphere(position, _resourcesParameters.MinDistanceBetween, LayerMasks.Resource);
        }

        private void OnCreate()
        {
            _processes.Add(new SpawnerProcess
            {
                Timer = 1,
                Action = () => Create()
            });
        }

        #region Nested type: SpawnerProcess

        private class SpawnerProcess
        {
            public Action Action;
            public float Timer;
        }

        #endregion
    }
}