using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.InputService;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Input
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1000, nameof(EFeatures.Common))]
    public class PointerMoveSystem : ForEachCommandUpdateSystem<PointerMoveCommand>
    {
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IInputService _inputService;
    
        //TODO: move to scriptable object
        private const float MOVEMENT_THRESHOLD = 0.1f;
    
        public PointerMoveSystem(
            ICommandBuffer commandBuffer,
            IGameGroupUtils gameGroupUtils,
            IInputService inputService
        ) : base(commandBuffer)
        {
            _gameGroupUtils = gameGroupUtils;
            _inputService = inputService;
        }
    
        protected override void Execute(ref PointerMoveCommand command)
        {
           // using var _ = _gameGroupUtils.GetMovableFigures(out var movableFigures, e => e.IsFigureTouched);
    
            var touchPosition = _inputService.GetTouchPosition(ref command);
    
            // foreach (var movableFigure in movableFigures)
            // {
            //     ProcessMovableFigure(movableFigure, touchPosition);
            // }
        }
    }
}