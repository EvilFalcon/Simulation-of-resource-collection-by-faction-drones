using Game.Services.GameStateService;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using UniRx;

namespace Game.UI.LoseGame
{
    public class LoseGameController : UiController<LoseGameView>, IUiInitializable
    {
        private readonly IGameStateService _gameStateService;

        public LoseGameController(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public void Initialize()
        {
            View.RestartButton.OnClickAsObservable().Subscribe(_ => OnRestartClick()).AddTo(View);
        }

        private void OnRestartClick() =>
            _gameStateService.RestartGame();
    }
}