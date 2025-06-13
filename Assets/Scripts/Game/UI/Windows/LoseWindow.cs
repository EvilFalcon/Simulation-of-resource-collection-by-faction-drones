using Game.UI.Input;
using Game.UI.LoseGame;
using Game.UI.Score;
using SimpleUi;

namespace Game.UI.Windows
{
    public class LoseWindow  : WindowBase
    {
        public override string Name => nameof(WindowBase);
        
        protected override void AddControllers()
        {
            AddController<LoseGameController>();
            AddController<InputController>();
            AddController<ScoreController>();
        }
    }
}