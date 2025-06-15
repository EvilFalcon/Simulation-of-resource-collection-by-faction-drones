using Ecs.Views.Linkable.Views;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using UnityEngine;

namespace Ecs.Utils
{
    public abstract class AObjectViewModule : MonoBehaviour
    {
        public void Subscribe(IEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe)
        {
            var self = (GameEntity)entity;
            Subscribe(self, objectView, unsubscribe);
        }

        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }

        protected abstract void Subscribe(GameEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe);
    }
}