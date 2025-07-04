using Game.UI.DebugView;
using Game.UI.Input;
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
        [SerializeField] private ResourcesView _resourceView;
        [SerializeField] private DebugView _debugView;

        public override void InstallBindings()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(_canvas);
            var canvasTransform = canvasView.transform;
            
            Container.BindUiView<InputController, InputView>(_inputView, canvasTransform);
            Container.BindUiView<ResourcesController, ResourcesView>(_resourceView, canvasTransform);
            Container.BindUiView<DebugController, DebugView>(_debugView, canvasTransform);
        }
    }
}