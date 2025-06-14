using System.Collections.Generic;
using Ecs.Utils;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using JCMG.EntitasRedux.Core.View.Impls;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ecs.Views.Linkable.Impl
{
    public class ObjectView : LinkableView, IObjectLinkable
    {
        [SerializeField] private List<AObjectViewModule> modules;

        public List<AObjectViewModule> Modules => modules;
        public Transform Transform => transform;
        protected virtual bool IsDestroyOnUnlink => true;

        [Button]
        public void FillModules()
        {
            modules.Clear();
            modules.AddRange(GetComponents<AObjectViewModule>());
        }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            var self = (GameEntity)entity;
            self.AddLink(this);
            self.AddTransform(transform);

            if (self.HasLocalPosition)
                OnLocalPosition(self, self.LocalPosition.Value);

            if (self.HasPosition)
                OnPosition(self, self.Position.Value);

            if (self.HasRotation)
                OnRotation(self, self.Rotation.Value);

            foreach (var module in Modules)
            {
                if (module == null)
                {
                    var parentObjectName = transform.parent != null ? transform.parent.name : "null";
                    Debug.LogError($"Null module found: {gameObject.name} with parent {parentObjectName}");
                    continue;
                }

                module.Subscribe(entity, this, unsubscribe);
            }

            self.SubscribePosition(OnPosition).AddTo(unsubscribe);
            self.SubscribeLocalPosition(OnLocalPosition).AddTo(unsubscribe);
            self.SubscribeRotation(OnRotation).AddTo(unsubscribe);
            self.SubscribeVelocity(OnVelocity).AddTo(unsubscribe);
        }

        public virtual void Activate()
        {
            foreach (var module in modules)
            {
                if (module == null)
                    continue;
                
                module.Activate();
            }
        }

        public virtual void Deactivate()
        {
            foreach (var module in modules)
            {
                if (module == null)
                    continue;
                
                module.Deactivate();
            }
        }

        protected virtual void OnPosition(GameEntity entity, Vector3 value)
        {
            transform.position = value;
            SetEntityLocalPosition(entity);
        }

        protected void OnLocalPosition(GameEntity entity, Vector3 value)
        {
            transform.localPosition = value;
            SetEntityPosition(entity);
        }

        protected virtual void OnRotation(GameEntity entity, Quaternion value)
        {
            transform.rotation = value;
        }

        protected virtual void OnVelocity(GameEntity entity, Vector3 value)
        {
            transform.position += value;
            SetEntityPosition(entity);
            SetEntityLocalPosition(entity);
        }

        protected void SetEntityPosition(GameEntity entity)
        {
            if (!entity.HasPosition)
                entity.AddPosition(transform.position);
            else
                entity.Position.Value = transform.position;
        }

        protected void SetEntityLocalPosition(GameEntity entity)
        {
            if (!entity.HasLocalPosition)
                entity.AddLocalPosition(transform.localPosition);
            else
                entity.LocalPosition.Value = transform.localPosition;
        }

        protected override void OnClear()
        {
            if (IsDestroyOnUnlink)
            {
                Destroy(gameObject);
            }
        }
    }
}