namespace Game.Services.SceneLoading
{
    public interface ISceneLoader
    {
        void LoadGameScene();
        void ReloadGame();
        float GetProgress();
    }
}