using Game.Services.SceneLoading.Game.SceneLoading;
using Game.Services.SceneLoading.Processors;
using PdUtils.SceneLoadingProcessor.Impls;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Services.SceneLoading.Impls
{
    public class SceneLoader : ISceneLoader
    {
        private readonly SignalBus _signalBus;

        private LoadingProcessor _processor;

        public SceneLoader(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void LoadGameScene()
        {
            _processor = new LoadingProcessor();
            _processor.AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(SceneNames.GAME, LoadSceneMode.Additive))
                .AddProcess(new SetActiveSceneProcess(SceneNames.GAME))
                .AddProcess(new UnloadProcess(SceneNames.INIT))
                .AddProcess(new RunContextProcess("GameContext"))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public void ReloadGame()
        {
            _processor = new LoadingProcessor();
            _processor.AddProcess(new OpenLoadingWindowProcess(_signalBus))
                .AddProcess(new LoadingProcess(SceneNames.GAME, LoadSceneMode.Single))
                .AddProcess(new SetActiveSceneProcess(SceneNames.GAME))
                .AddProcess(new RunContextProcess("GameContext"))
                .AddProcess(new WaitUpdateProcess(4))
                .AddProcess(new ProjectWindowBack(_signalBus))
                .DoProcess();
        }

        public float GetProgress() => _processor?.Progress ?? 0f;
    }
}