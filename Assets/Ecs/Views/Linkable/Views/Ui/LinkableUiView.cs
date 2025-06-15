using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using SimpleUi.Abstracts;
using UnityEngine;

namespace Ecs.Views.Linkable.Views.Ui
{
    public abstract class ObjectLinkableUiView<TEntity> : UiView, IObjectLinkableUi<TEntity>
        where TEntity : class, IEntity
    {
        private readonly UnsubscribeEvent _unsubscribeEvent = new();
        private bool _destroyed;
        private TEntity _entity;

        #region IObjectLinkableUi<TEntity> Members

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

        public virtual void Subscribe(TEntity entity, IUnsubscribeEvent unsubscribe){}

        public void Unsubscribe()
        {
            _unsubscribeEvent.Clear();
            OnUnsubscribe();
        }

        #endregion

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