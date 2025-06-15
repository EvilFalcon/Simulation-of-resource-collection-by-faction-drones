using Ecs.Game.Extensions;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Systems.InitializeSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 20, nameof(EFeatures.Initialization))]
    public class CreatePlayerInitializeSystem : IInitializeSystem
    {
        private readonly GameContext _game;

        public CreatePlayerInitializeSystem(
            GameContext game
        )
        {
            _game = game;
        }
        
        public void Initialize()
        {
            _game.CreatePlayer();
            InitializeCamera();
        }
        
        private void InitializeCamera()
        {
            _game.CreateCamera();
            var camera = _game.CreateVirtualCamera();
            
            var newCameraRotation = Quaternion.Euler(90, 0, 0);
            camera.ReplaceRotation(newCameraRotation);
            camera.ReplacePosition(new Vector3(22.7f, 22.7f, 12.8f));
        }
    }
}