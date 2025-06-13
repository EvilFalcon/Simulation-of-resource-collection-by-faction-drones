using Game.UI.Input;
using Game.UI.LoseGame;
using Game.UI.Score;
using SimpleUi;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameUiPrefabInstaller), fileName = nameof(GameUiPrefabInstaller))]
    public class GameUiPrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private InputView _inputView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private LoseGameView _loseGameView;
        
        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(_canvas);
            var canvasTransform = canvasView.transform;
            
            Container.BindUiView<InputController, InputView>(_inputView, canvasTransform);
            Container.BindUiView<ScoreController, ScoreView>(_scoreView, canvasTransform);
            Container.BindUiView<LoseGameController, LoseGameView>(_loseGameView, canvasTransform);
        }
    }
}