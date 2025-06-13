using SimpleUi;

namespace Project.Ui.Empty
{
    public class EmptyWindow : WindowBase
    {
        public override string Name => "Empty";

        protected override void AddControllers()
        {
            AddController<EmptyController>();
        }
    }
}