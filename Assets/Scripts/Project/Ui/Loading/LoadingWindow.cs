using Project.Ui.Loading.Controllers;
using SimpleUi;

namespace Project.Ui.Loading
{
	public class LoadingWindow : WindowBase
	{
		public override string Name => "Loading";

		protected override void AddControllers()
		{
			AddController<LoadingController>();
		}
	}
}