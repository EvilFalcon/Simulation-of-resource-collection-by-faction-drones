using Ecs.Game.Components.Units;
using Ecs.Utils;
using Ecs.Views.Linkable.Views;
using Ecs.Views.Linkable.Views.Units;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.Utils;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Modules.Fraction
{
    public class FractionUnloadingModule : AObjectViewModule
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] Collider _unloadingResourcesTrigger;
        private ICommandBuffer _commandBuffer;
        private GameEntity _entity;
        private ILinkedEntityRepository _linckedEntityRepository;

        [Inject]
        private void Construct(ILinkedEntityRepository linkedEntityRepository, ICommandBuffer commandBuffer)
        {
            _linckedEntityRepository = linkedEntityRepository;
            _commandBuffer = commandBuffer;
        }

        protected override void Subscribe(GameEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            _unloadingResourcesTrigger.OnTriggerEnterAsObservable().Subscribe(OnUnliadingResources).AddTo(unsubscribe);

            _entity = entity;
        }

        private void OnUnliadingResources(Collider other)
        {
            if (other.TryGetComponent(out UnitView unitFraction) == false)
                return;

            if (!_linckedEntityRepository.TryGet(unitFraction.transform.GetHashCode(), out var unit))
                return;

            if (!unit.HasUnitFraction)
                return;

            if (unit.UnitFraction.FractionType != _entity.FractionType.Value)
                return;

            if (unit.UnitFraction.State != EUnitState.ReturningToBase)
                return;

            _commandBuffer.CreditFactionResources(_entity, unit);
            _particleSystem.Play();
            unit.ReplaceUnitFraction(
                unit.UnitFraction.FractionType,
                unit.UnitFraction.HomePosition,
                EUnitState.Searching,
                0f
            );
        }
    }
}