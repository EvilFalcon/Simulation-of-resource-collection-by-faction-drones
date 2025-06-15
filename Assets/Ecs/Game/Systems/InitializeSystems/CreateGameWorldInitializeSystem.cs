using Core;
using Db.Generation.ResourcesParameters;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Services.Spawners;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.InitializeSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 20, nameof(EFeatures.Initialization))]
    public class CreateGameWorldInitializeSystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGameSceneProvider _gameSceneProvider;
        private readonly IResourcesParameters _resourcesParameters;
        private readonly IResourcesSpawner _resourcesSpawner;

        public CreateGameWorldInitializeSystem(GameContext gameContext,
            IResourcesParameters resourcesParameters,
            IResourcesSpawner resourcesSpawner,
            IGameSceneProvider gameSceneProvider)
        {
            _gameContext = gameContext;
            _resourcesParameters = resourcesParameters;
            _resourcesSpawner = resourcesSpawner;
            _gameSceneProvider = gameSceneProvider;
        }

        public void Initialize()
        {
            InitializeMap();

            CreateMapResources();
        }

        private void CreateMapResources()
        {
            for (var i = 0; i < _resourcesParameters.TotalResourcesToSpawn; i++)
            {
                _resourcesSpawner.Create();
            }
        }

        private void InitializeMap()
        {
            var gameMap = _gameContext.CreateGameMap();
            gameMap.AddTerrain(_gameSceneProvider.Terrain);

            var terrainSize = _gameSceneProvider.Terrain.terrainData.size;
            var terrainPosition = _gameSceneProvider.Terrain.transform.position;
            var minBounds = terrainPosition;
            var maxBounds = terrainPosition + terrainSize;
            gameMap.ReplaceTerrainBounds(maxBounds, minBounds);
            gameMap.ReplaceTerrainData(_gameSceneProvider.Terrain.terrainData);
        }
    }
}