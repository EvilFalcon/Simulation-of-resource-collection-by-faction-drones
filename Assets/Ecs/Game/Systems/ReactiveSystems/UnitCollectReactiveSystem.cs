using System.Collections.Generic;
using Ecs.Game.Components.Units;
using Ecs.Utils;
using Game.Services.Pools;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Systems.ReactiveSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1100, nameof(EFeatures.Common))]
    public class UnitCollectReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IResourcesPool _resourcesPool;

        public UnitCollectReactiveSystem(
            GameContext context,
            ILinkedEntityRepository linkedEntityRepository,
            IResourcesPool resourcesPool
        ) : base(context)
        {
            _linkedEntityRepository = linkedEntityRepository;
            _resourcesPool = resourcesPool;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.UnitFraction.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.HasUnitFraction &&
                   entity.UnitFraction.State == EUnitState.Collecting;
        }

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var unit in entities)
            {
                var timer = unit.UnitFraction.Timer + Time.deltaTime;
                unit.ReplaceUnitFraction(
                    unit.UnitFraction.FractionType,
                    unit.UnitFraction.HomePosition,
                    unit.UnitFraction.State,
                    timer
                );

                if (!(timer >= 2f))
                {
                    unit.NavMeshAgent.Value.ResetPath();
                    continue;
                }

                var resource = _linkedEntityRepository.Get(unit.TargetResourceId.Value);
                unit.ReplaceResourceData(resource.ResourceData.Amount, resource.ResourceData.ResourceType);
                _resourcesPool.Despawn(resource.ResourceData.ResourceType, resource, resource.Resource.PoolIndex);

                unit.ReplaceUnitFraction(
                    unit.UnitFraction.FractionType,
                    unit.UnitFraction.HomePosition,
                    EUnitState.ReturningToBase,
                    0f
                );
                unit.NavMeshAgent.Value.SetDestination(unit.UnitFraction.HomePosition);
            }
        }
    }
}