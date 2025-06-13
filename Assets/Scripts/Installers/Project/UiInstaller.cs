using Project.Ui.Empty;
using Project.Ui.Loading;
using Zenject;

namespace Installers.Project
{
	public class UiInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<EmptyWindow>().AsSingle();
			Container.BindInterfacesAndSelfTo<LoadingWindow>().AsSingle();
		}
	}
}