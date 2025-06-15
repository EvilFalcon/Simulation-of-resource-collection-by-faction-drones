using Project.Ui.Empty;
using SimpleUi.Signals;
using Zenject;

namespace Project
{
	public class ProjectWindowManager : IInitializable
	{
		private readonly SignalBus _signalBus;

		public ProjectWindowManager(SignalBus signalBus)
		{
			_signalBus = signalBus;
		}

		#region IInitializable Members

		public void Initialize()
		{ 
			_signalBus.OpenWindow<EmptyWindow>(EWindowLayer.Project);
		}

		#endregion
	}
}