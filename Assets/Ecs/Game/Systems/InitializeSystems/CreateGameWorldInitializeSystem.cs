using Core;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.InitializeSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 20, nameof(EFeatures.Initialization))]
    public class CreateGameWorldInitializeSystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IGameSceneProvider _gameSceneProvider;

        public CreateGameWorldInitializeSystem(GameContext gameContext,
            ICommandBuffer commandBuffer,
            IGameSceneProvider gameSceneProvider)
        {
            _gameContext = gameContext;
            _commandBuffer = commandBuffer;
            _gameSceneProvider = gameSceneProvider;
        }

        public void Initialize()
        {
            var gameMap = _gameContext.CreateGameMap();
            gameMap.AddTerrain(_gameSceneProvider.Terrain);

            var terrainSize = _gameSceneProvider.Terrain.terrainData.size;
            var terrainPosition = _gameSceneProvider.Terrain.transform.position;
            var minBounds = terrainPosition;
            var maxBounds = terrainPosition + terrainSize;
            gameMap.ReplaceTerrainBounds(minBounds, maxBounds);
            gameMap.ReplaceTerrainData(_gameSceneProvider.Terrain.terrainData);
            _commandBuffer.GenerateRandomResource();
        }
    }
}