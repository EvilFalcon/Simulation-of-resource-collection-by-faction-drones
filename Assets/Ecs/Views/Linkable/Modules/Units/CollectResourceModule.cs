using Ecs.Game.Components.Units;
using Ecs.Utils;
using Ecs.Views.Linkable.Views;
using Ecs.Views.Linkable.Views.ResourcesView;
using JCMG.EntitasRedux.Core.Utils;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Ecs.Views.Linkable.Modules.Units
{
    public class CollectResourceModule : AObjectViewModule
    {
        [SerializeField] private Collider _collectTrigger;
        private GameEntity _entity;

        protected override void Subscribe(GameEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            _entity = entity;
            _collectTrigger.OnTriggerEnterAsObservable().Subscribe(OnCollect).AddTo(unsubscribe);
        }

        private void OnCollect(Collider other)
        {
            if (other.TryGetComponent(out ResourceView resourceView) == false)
                return;

            if (resourceView.transform.GetHashCode() != _entity.TargetResourceId.Value)
                return;
            
            _entity.ReplaceUnitFraction(
                _entity.UnitFraction.FractionType,
                _entity.UnitFraction.HomePosition,
                EUnitState.Collecting,
                0f
                );
        }
    }
}