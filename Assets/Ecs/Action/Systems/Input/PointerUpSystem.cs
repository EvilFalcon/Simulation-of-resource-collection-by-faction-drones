using Ecs.Action.Commands.Input;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Input
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1000, nameof(EFeatures.Common))]
    public class PointerUpSystem : ForEachCommandUpdateSystem<PointerUpCommand>
    {
        private readonly IGameGroupUtils _gameGroupUtils;

        public PointerUpSystem(
            ICommandBuffer commandBuffer,
            IGameGroupUtils gameGroupUtils
        ) : base(commandBuffer)
        {
            _gameGroupUtils = gameGroupUtils;
        }

        protected override void Execute(ref PointerUpCommand command)
        {
            // using var _ = _gameGroupUtils.GetMovableFigures(out var movableFigures, e => e.IsFigureTouched);
        }
    }
}