using Project.Ui.Empty;
using Project.Ui.Loading.Controllers;
using Project.Ui.Loading.Views;
using SimpleUi;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
	[CreateAssetMenu(menuName = "Installers/ProjectUiPrefabsInstaller", fileName = "ProjectUiPrefabsInstaller")]
	public class ProjectUiPrefabsInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private Canvas canvas;

		public EmptyView emptyView;
		public LoadingView loadingView;

		public override void InstallBindings()
		{
			var canvasView = Container.InstantiatePrefabForComponent<Canvas>(canvas);
			var canvasTransform = canvasView.transform;

			Container.BindUiView<EmptyController, EmptyView>(emptyView, canvasTransform);
			Container.BindUiView<LoadingController, LoadingView>(loadingView, canvasTransform);
		}
	}
}