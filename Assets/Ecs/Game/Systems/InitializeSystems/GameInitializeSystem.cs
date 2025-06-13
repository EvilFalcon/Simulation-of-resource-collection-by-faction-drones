using Ecs.Utils;
using Game.Services.GameStateService;
using Game.UI.Windows;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Signals;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
	[Install(ExecutionType.Game, ExecutionPriority.High, 10, nameof(EFeatures.Initialization))]
	public class GameInitializeSystem : IInitializeSystem
	{
		private readonly GameContext _game;
		private readonly SignalBus _signalBus;
		private readonly ICommandBuffer _commandBuffer;
		private readonly IGameStateService _gameStateService;

		protected GameInitializeSystem(
			GameContext game,
			SignalBus signalBus,
			ICommandBuffer commandBuffer,
			IGameStateService gameStateService
		)
		{
			_game = game;

			_signalBus = signalBus;
			_commandBuffer = commandBuffer;
			_gameStateService = gameStateService;
		}

		public void Initialize()
		{
			_gameStateService.StartGame();
			_commandBuffer.SignalStart();
			_signalBus.OpenWindow<GameplayWindow>();
		}
	}
}