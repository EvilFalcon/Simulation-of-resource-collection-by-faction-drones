using Ecs.Utils.Groups.Impl;
using Ecs.Utils.Impl;
using Game.Models.Camera.Impl;
using Game.Models.Input;
using Game.Services.GameStateService.Impl;
using Game.Services.InputService.Impl;
using Game.Services.Pool.Impls;
using Game.UI.Windows;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind();
            BindWindows();
            BindManagers();
            BindServices();
        }

        private void Bind()
        {
            Container.Bind<LateFixedManager>().AsSingle();
            Container.Bind<LateFixedUpdate>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerCameraHolder>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<GameGroupUtils>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();
        }

        private void BindWindows()
        {
            Container.DeclareSignal<GameplayWindow>();
            Container.BindInterfacesAndSelfTo<GameplayWindow>().AsSingle();
            Container.DeclareSignal<LoseWindow>();
            Container.BindInterfacesAndSelfTo<LoseWindow>().AsSingle();
        }

        private void BindManagers()
        {
            Container.BindInterfacesTo<MoveController>().AsSingle();
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<LinkedEntityRepository>().AsSingle();
            Container.BindInterfacesTo<PrefabPoolService>().AsSingle();
        }
    }
}