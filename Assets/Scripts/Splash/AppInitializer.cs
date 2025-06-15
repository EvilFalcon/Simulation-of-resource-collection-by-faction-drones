using Game.Services.SceneLoading;
using Zenject;

namespace Splash
{
    public class AppInitializer : IInitializable
    {
        private readonly ISceneLoader _sceneLoader;

        public AppInitializer(
            ISceneLoader sceneLoader
        )
        {
            _sceneLoader = sceneLoader;
        }

        #region IInitializable Members

        public void Initialize()
        {
            LoadGame();
        }

        #endregion

        private void LoadGame()
        {
            _sceneLoader.LoadGameScene();
        }
    }
}