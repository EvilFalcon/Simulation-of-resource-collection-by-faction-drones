using Db.GameObjectsBase.Impl;
using Ecs.Action.Commands.UnitsFraction;
using Ecs.Game.Components.Units;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Ecs.Utils.Repositories;
using Ecs.Views.Linkable.Views.Units;
using Game.Services.Pools.Impls.Unit;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.FractionUnits
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 200, nameof(EFeatures.Common))]
    public class RemoveFractionUnitsSystem : ForEachCommandUpdateSystem<RemoveUnitsFractionCommand>
    {
        private readonly GameContext _gameContext;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IActiveResourcesRepository _activeResourcesRepository;
        private readonly IUnitPool _unitPool;

        public RemoveFractionUnitsSystem(
            ICommandBuffer commandBuffer,
            IUnitPool unitPool,
            ILinkedEntityRepository linkedEntityRepository,
            IGameGroupUtils gameGroupUtils,
            IActiveResourcesRepository activeResourcesRepository,
            GameContext gameContext) : base(commandBuffer)
        {
            _unitPool = unitPool;
            _linkedEntityRepository = linkedEntityRepository;
            _gameGroupUtils = gameGroupUtils;
            _activeResourcesRepository = activeResourcesRepository;
            _gameContext = gameContext;
        }

        protected override void Execute(ref RemoveUnitsFractionCommand command)
        {
            var fractionType = command.FractionType;
            Remove(fractionType);
        }

        private void Remove(EFractionType fractionType)
        {
            using var _ = _gameGroupUtils.GetUnits(out var entities,
                entity => !entity.IsDestroyed && entity.UnitFraction.FractionType == fractionType);

            foreach (var entity in entities)
            {
                if (entity.HasTargetResourceId)
                {
                    if (_linkedEntityRepository.TryGet(entity.TargetResourceId.Value, out var resourcesEntity) == false)
                        continue;

                    _activeResourcesRepository.Add(resourcesEntity);
                }

                if (entity.HasResourceData)
                    entity.RemoveResourceData();

                entity.RemoveUnitFraction();
                entity.RemoveTargetResourceId();
                entity.NavMeshAgent.Value.ResetPath();
                entity.IsRay = false;

                var unitView = (UnitView)entity.Link.View;
                
                unitView.Unlink();
                entity.IsDestroyed = true;
                entity.RemoveAllComponents();
                entity.Destroy();
                _unitPool.Release(fractionType, unitView);
                return;
            }
        }
    }
}