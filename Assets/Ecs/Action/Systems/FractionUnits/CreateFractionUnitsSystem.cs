using Ecs.Action.Commands.UnitsFraction;
using Ecs.Game.Components.Units;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Services.Pools.Impls.Unit;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.FractionUnits
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 200, nameof(EFeatures.Common))]
    public class CreateFractionUnitsSystem : ForEachCommandUpdateSystem<CreateUnitsFractionCommand>
    {
        private readonly GameContext _gameContext;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IUnitPool _unitPool;

        public CreateFractionUnitsSystem(
            ICommandBuffer commandBuffer,
            IUnitPool unitPool,
            ILinkedEntityRepository linkedEntityRepository,
            GameContext gameContext) : base(commandBuffer)
        {
            _unitPool = unitPool;
            _linkedEntityRepository = linkedEntityRepository;
            _gameContext = gameContext;
        }

        protected override void Execute(ref CreateUnitsFractionCommand command)
        {
            var unitEntity = _gameContext.CreateUnit(command.FractionType);
            var unitView = _unitPool.Get(command.FractionType);
            unitEntity.AddUnitFraction(
                command.FractionType,
                command.FractionBasePosition,
                EUnitState.Searching,
                0f);
            
            unitView.Link(unitEntity);
            unitEntity.ReplacePosition(command.UnitSpawnPosition);
            _linkedEntityRepository.Add(unitView.transform.GetHashCode(), unitEntity);
        }
    }
}