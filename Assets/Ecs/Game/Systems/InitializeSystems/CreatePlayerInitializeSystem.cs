using Ecs.Game.Extensions;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.InitializeSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 20, nameof(EFeatures.Initialization))]
    public class CreatePlayerInitializeSystem : IInitializeSystem
    {
        private readonly GameContext _game;

        public CreatePlayerInitializeSystem(
            GameContext game
        )
        {
            _game = game;
        }

        public void Initialize()
        {
            _game.CreatePlayer();

            _game.CreateCamera();
            _game.CreateVirtualCamera();
        }
    }
}