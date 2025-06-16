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

namespace Ecs.Game.Systems.InitializeSystems
{
	[Install(ExecutionType.Game, ExecutionPriority.High, 10, nameof(EFeatures.Initialization))]
	public class GameInitializeSystem : IInitializeSystem
	{
		private readonly ICommandBuffer _commandBuffer;
		private readonly IGameStateService _gameStateService;
		private readonly SignalBus _signalBus;

		protected GameInitializeSystem(
			GameContext game,
			SignalBus signalBus,
			ICommandBuffer commandBuffer,
			IGameStateService gameStateService
		)
		{
			_signalBus = signalBus;
			_commandBuffer = commandBuffer;
			_gameStateService = gameStateService;
		}

		public void Initialize()
		{
			_gameStateService.StartGame();
			_commandBuffer.SignalStart();
		}
	}
}