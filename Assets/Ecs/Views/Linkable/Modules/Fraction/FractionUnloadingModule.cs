using Ecs.Game.Components.Units;
using Ecs.Utils;
using Ecs.Views.Linkable.Views;
using Ecs.Views.Linkable.Views.Units;
using JCMG.EntitasRedux.Core.Utils;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Modules.Fraction
{
    public class FractionUnloadingModule : AObjectViewModule
    {
        private GameEntity _entity;

        private ILinkedEntityRepository _linckedEntityRepository;

        [SerializeField] Collider _unloadingResourcesTrigger;

        [Inject]
        private void Construct(ILinkedEntityRepository linkedEntityRepository)
        {
            _linckedEntityRepository = linkedEntityRepository;
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

            _linckedEntityRepository.TryGet(unitFraction.transform.GetHashCode(), out var unit);

            if (unit.UnitFraction.FractionType != _entity.UnitFraction.FractionType)
                return;

            unit.RemoveResourceData();

            unit.ReplaceUnitFraction(
                _entity.UnitFraction.FractionType,
                _entity.UnitFraction.HomePosition,
                EUnitState.Searching,
                -1, 
                0f
            );
        }
    }
}