using Game.UI.DebugView;
using Game.UI.Score;
using SimpleUi;

namespace Game.UI.Windows
{
    public class GameplayWindow : WindowBase
    {
        public override string Name => nameof(GameplayWindow);

        protected override void AddControllers()
        {
            AddController<ResourcesController>();
            AddController<DebugController>();
        }
    }
}