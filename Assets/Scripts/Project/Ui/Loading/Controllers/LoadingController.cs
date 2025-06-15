using Game.Services.SceneLoading;
using Project.Ui.Loading.Views;
using SimpleUi.Abstracts;
using UnityEngine;
using Zenject;

namespace Project.Ui.Loading.Controllers
{
	public class LoadingController : UiController<LoadingView>, ITickable
	{
		private readonly ISceneLoader _sceneLoader;

		public LoadingController(ISceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
		}

		#region ITickable Members

		public void Tick()
		{
			View.TextLoading.text = Mathf.RoundToInt(_sceneLoader.GetProgress() * 100) + "% loaded";
		}

		#endregion
	}
}