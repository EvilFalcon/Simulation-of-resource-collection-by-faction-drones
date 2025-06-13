using Ecs.Utils;
using Game.Services.SceneLoading;
using Game.UI.Windows;
using SimpleUi.Signals;
using Zenject;

namespace Game.Services.GameStateService.Impl
{
    public class GameStateService : IGameStateService
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GameContext _game;
        private readonly SignalBus _signalBus;

        public GameStateService(
            ISceneLoader sceneLoader,
            GameContext game,
            SignalBus signalBus
        )
        {
            _sceneLoader = sceneLoader;
            _game = game;
            _signalBus = signalBus;
        }

        public void StartGame()
        {
            _game.ReplaceGameState(EGameState.Game);
            _signalBus.OpenWindow<GameplayWindow>();
        }

        public void LoseGame()
        {
            _game.ReplaceGameState(EGameState.Lose);
            _signalBus.OpenWindow<LoseWindow>();
        }

        public void RestartGame()
        {
            _sceneLoader.ReloadGame();
        }
    }
}