using Ecs.Views.Linkable.Impl;
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
            Subscribe(entity,objectView,unsubscribe);
        }

        public abstract void Activate();
        public abstract void Deactivate();
        protected abstract void Subscribe(GameEntity entity, ObjectView objectView, IUnsubscribeEvent unsubscribe);
    }
}