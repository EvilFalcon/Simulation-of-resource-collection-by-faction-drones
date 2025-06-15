using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Ecs.Views.Linkable.Views.Units
{
    public class UnitView : PoolObjectView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            var self = (GameEntity)entity;
            
            self.ReplaceNavMeshAgent(_navMeshAgent);
            base.Subscribe(entity, unsubscribe);
        }
    }
}