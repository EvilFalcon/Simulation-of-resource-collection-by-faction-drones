using Ecs.Installers.Game.Feature;
using Ecs.Installers.Game.Feature.Impls;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Installers.Game
{
	public class GameEcsInstaller : AEcsInstaller
	{
		protected override void InstallSystems(Contexts contexts, bool isDebug)
		{
			Container.BindInterfacesTo<CommandBuffer>().AsSingle();
			
			BindContext<ActionContext>();
			BindContext<GameContext>();
			BindContext<InputContext>();
			BindContext<SchedulerContext>();
			BindContext<SignalContext>();

			GameEcsSystems.Install(Container, isDebug);

			// Event systems
			Container.BindInterfacesTo<ActionEventSystems>().AsSingle();
		
			Container.BindInterfacesTo<GameEventSystems>().AsSingle();
			Container.BindInterfacesTo<InputEventSystems>().AsSingle();
		
			Container.BindInterfacesTo<SchedulerEventSystems>().AsSingle();
			Container.BindInterfacesTo<SignalEventSystems>().AsSingle();
			
			BindFeature<GameFeature, IGameFeature>();
		}
	}
}