using Core;
using Db.Generation.ResourcesParameters;
using Ecs.Action.Commands.Generation;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Services.Pools.Impls.Resouces;
using Game.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.ResourcesGenerationSystem
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 100, nameof(EFeatures.Generation))]
    public class AdvancedResourceSpawnerSystem : ForEachCommandUpdateSystem<GenerateRandomResourceCommand>
    {
        private readonly IResourcesPool _resourcesPool;
        private readonly IResourcesParameters _resourcesParameters;
        private readonly IGameSceneProvider _gameSceneProvider;
        private readonly GameContext _gameContext;

        public AdvancedResourceSpawnerSystem(
            ICommandBuffer commandBuffer,
            IResourcesPool resourcesPool,
            IResourcesParameters resourcesParameters,
            IGameSceneProvider gameSceneProvider,
            GameContext gameContext)
            : base(commandBuffer)
        {
            _resourcesPool = resourcesPool;
            _resourcesParameters = resourcesParameters;
            _gameSceneProvider = gameSceneProvider;
            _gameContext = gameContext;
        }

        protected override void Execute(ref GenerateRandomResourceCommand command)
        {
            SpawnResources();
        }

        private void SpawnResources()
        {
            var terrainEntityTerrainData = _gameContext.TerrainEntity.TerrainData;
            var terrainData = terrainEntityTerrainData.Value;
            var terrainSize = terrainData.size;
            var terrainPos = _gameSceneProvider.Terrain.transform.position;

            for (int i = 0; i < _resourcesParameters.TotalResourcesToSpawn; i++)
            {
                var resource = GetRandomResource();

                if (resource == null) continue;

                for (var attempt = 0; attempt < _resourcesParameters.MaxAttemptsPerResource; attempt++)
                {
                    var spawnPosition = new Vector3(
                        terrainPos.x + Random.Range(0, terrainSize.x),
                        0,
                        terrainPos.z + Random.Range(0, terrainSize.z)
                    );

                    // Получаем высоту в точке
                    spawnPosition.y = _gameContext.TerrainEntity.Terrain.Value.SampleHeight(spawnPosition);

                    if (!IsValidSpawnPosition(spawnPosition))
                        continue;
                    // Спавним ресурс

                    var (resourceView, randomIndex) = _resourcesPool.Spawn(resource.Value.ResourceType);
                    var resourceEntity = _gameContext.CreateResource();
                    resourceEntity.AddResourceType(resource.Value.ResourceType);
                    resourceEntity.AddPosition(spawnPosition);
                    resourceView.Link(resourceEntity);
                    break;
                }
            }
        }

        private bool IsValidSpawnPosition(Vector3 position)
        {
            // Проверка коллизий
            return !Physics.CheckSphere(position, _resourcesParameters.MinDistanceBetween, LayerMasks.Resource);
        }

        private ResourceConfig? GetRandomResource()
        {
            if (_resourcesParameters.Resources.Count == 0) return null;

            float totalWeight = 0;
            foreach (var res in _resourcesParameters.Resources)
                totalWeight += res.SpawnWeight;

            var randomValue = Random.Range(0, totalWeight);
            float currentWeight = 0;

            foreach (var res in _resourcesParameters.Resources)
            {
                currentWeight += res.SpawnWeight;
                if (randomValue <= currentWeight)
                    return res;
            }

            return _resourcesParameters.Resources[0];
        }
    }
}