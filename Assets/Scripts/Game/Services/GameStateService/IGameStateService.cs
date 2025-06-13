namespace Game.Services.GameStateService
{
    public interface IGameStateService
    {
        void LoseGame();
        void RestartGame();
        void StartGame();
    }
}