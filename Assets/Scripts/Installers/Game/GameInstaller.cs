using Ecs.Utils.Groups.Impl;
using Ecs.Utils.Impl;
using Game.Models.Camera.Impl;
using Game.Models.Input;
using Game.Services.GameStateService.Impl;
using Game.Services.InputService.Impl;
using Game.Services.Pools.Impls;
using Game.Services.Pools.Impls.Resouces;
using Game.UI.Windows;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameInitializer _gameInitializer;
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
            Container.BindInterfacesAndSelfTo<ResourcesPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInitializer>().FromInstance(_gameInitializer).AsSingle();
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