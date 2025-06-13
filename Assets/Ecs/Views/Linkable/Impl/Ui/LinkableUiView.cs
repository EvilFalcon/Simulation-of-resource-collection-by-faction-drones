using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl.Ui
{
    public abstract class ObjectLinkableUiView<TEntity> : UiView, IObjectLinkableUi<TEntity>
        where TEntity : class, IEntity
    {
        private TEntity _entity;
        private bool _destroyed;
        
        private readonly UnsubscribeEvent _unsubscribeEvent = new();

        public int Hash => transform.GetHashCode();
        public Transform Transform => transform;

        public void Link(IEntity entity)
        {
            _entity = (TEntity)entity;
            entity.OnBeforeDestroyEntity += OnDestroyEntity;
            Subscribe(_entity, _unsubscribeEvent);
        }

        public void Unlink()
        {
            if (_entity == null)
                return;

            OnDestroyEntity(_entity);
        }

        void IObjectLinkableUi<TEntity>.Reset()
        {
            OnReset();
        }

        protected virtual void OnReset()
        {
            
        }
        
        private void OnDestroyEntity(IEntity entity)
        {
            if (entity.IsEnabled)
            {
                entity.OnBeforeDestroyEntity -= OnDestroyEntity;
                Unsubscribe();
            }

            if (!_destroyed)
                OnClear();
        }

        protected virtual void OnClear()
        {
        }
        
        public virtual void Subscribe(TEntity entity, IUnsubscribeEvent unsubscribe){}

        public void Unsubscribe()
        {
            _unsubscribeEvent.Clear();
            OnUnsubscribe();
        }

        protected virtual void OnUnsubscribe()
        {
        }

        protected override void OnDestroy()
        {
            _destroyed = true;
            
            if (_entity != null)
                OnDestroyEntity(_entity);

            OnDestroyed();
        }

        protected virtual void OnDestroyed()
        {
        }
    }
}