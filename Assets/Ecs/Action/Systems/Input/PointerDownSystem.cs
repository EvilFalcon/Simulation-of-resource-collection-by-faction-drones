using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Game.Services.InputService;
using Game.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Input
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1000, nameof(EFeatures.Common))]
    public class PointerDownSystem : ForEachCommandUpdateSystem<PointerDownCommand>
    {
        private const int RayCastLength = 100;
        
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IInputService _inputService;
        private readonly GameContext _game;
        
        public PointerDownSystem(
            ICommandBuffer commandBuffer,
            ILinkedEntityRepository linkedEntityRepository,
            IInputService inputService,
            GameContext game
        ) : base(commandBuffer)
        {
            _linkedEntityRepository = linkedEntityRepository;
            _inputService = inputService;
            _game = game;
        }
        
        protected override void Execute(ref PointerDownCommand command)
        {
            if (_game.GameState.Value != EGameState.Game)
                return;
            
            var ray = _inputService.GetRayOfTouch(ref command);
            
            if (!Physics.Raycast(ray, out var hit, RayCastLength, LayerMasks.Unit))
                return;

            var id = hit.transform.GetHashCode();
        }
    }
}