using UniRx;
using Zenject;

namespace Splash
{
	public class InitInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			MainThreadDispatcher.Initialize();

			Container.BindInterfacesAndSelfTo<AppInitializer>().AsSingle().NonLazy();
			
		}
	}
}