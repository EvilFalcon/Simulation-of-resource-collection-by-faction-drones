using Ecs.Game.Extensions;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.InitializeSystems
{
    
    [Install(ExecutionType.Game, ExecutionPriority.High, 10, nameof(EFeatures.Initialization))]
    public class CreateGameWorldInitializeSystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;

        public CreateGameWorldInitializeSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Initialize()
        {
            var gameMap = _gameContext.CreateGameMap();
        }
    }
}