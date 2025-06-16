using Ecs.Game.Components.Units;
using Ecs.Utils;
using Ecs.Utils.Repositories;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.UpdateSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 100, nameof(EFeatures.Common))]
    public class UnitSearchUpdateSystem : IUpdateSystem
    {
        private readonly IActiveResourcesRepository _activeResourcesRepository;
        private readonly GameContext _gameContext;

        public UnitSearchUpdateSystem(IActiveResourcesRepository activeResourcesRepository, GameContext gameContext)
        {
            _activeResourcesRepository = activeResourcesRepository;
            _gameContext = gameContext;
        }

        #region IUpdateSystem Members

        public void Update()
        {
            if (_gameContext.GameState.Value != EGameState.Game)
                return;

            var searchingDrones = _gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.UnitFraction,
                GameMatcher.NavMeshAgent
            ));

            foreach (var unit in searchingDrones.GetEntities())
            {
                if (unit.UnitFraction.State != EUnitState.Searching)
                    continue;

                var nearestResource = _activeResourcesRepository.GetNearestEntity(unit.NavMeshAgent.Value.transform.position);

                if (nearestResource == null)
                    continue;

                unit.ReplaceTargetResourceId(nearestResource.Link.View.Transform.GetHashCode());
                unit.ReplaceUnitFraction(
                    unit.UnitFraction.FractionType,
                    unit.UnitFraction.HomePosition,
                    EUnitState.MovingToResource,
                    0f
                );

                unit.NavMeshAgent.Value.SetDestination(nearestResource.Position.Value);
            }
        }

        #endregion
    }
}