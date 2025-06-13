using System;
using PdUtils.SceneLoadingProcessor.Impls;
using Project.Ui.Loading;
using SimpleUi.Signals;
using UniRx;
using Zenject;

namespace Game.Services.SceneLoading.Processors
{
	public class OpenLoadingWindowProcess : Process
	{
		private readonly SignalBus _signalBus;

		public OpenLoadingWindowProcess(SignalBus signalBus)
		{
			_signalBus = signalBus;
		}

		public override void Do(Action complete)
		{
			_signalBus.OpenWindow<LoadingWindow>(EWindowLayer.Project);
			Observable.NextFrame().Subscribe(_ => complete());
		}
	}
}