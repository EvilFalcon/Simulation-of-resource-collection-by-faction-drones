using Ecs.Managers;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;

namespace Ecs.Views.Linkable.Views
{
    public class PoolObjectView : ObjectView
    {
        protected override bool IsDestroyOnUnlink => false;
        public Uid? EntityId { get; private set; }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            base.Subscribe(entity, unsubscribe);

            EntityId = ((GameEntity)entity).Uid.Value;
        }
    }
}