using Ecs.Views.Linkable;
using UnityEngine;
using Zenject;

namespace Game.Services.Pools.Impls
{
    public class PrefabMemoryPool : MemoryPool<IObjectLinkable>, IPrefabMemoryPool
    {
        private static readonly Vector3 DefaultPosition = new Vector3(0, -5000, 0);

        protected Transform OriginalParent;

        public string Name { get; }

        public PrefabMemoryPool(string name)
        {
            Name = name;
            
            var container = new GameObject("Pool: " + name);
            OriginalParent = container.transform;
        }

        protected override void OnCreated(IObjectLinkable item)
        {
            var transform = item.Transform;
            transform.SetPositionAndRotation(DefaultPosition, Quaternion.identity);
            transform.SetParent(OriginalParent);
        }

        protected override void OnDestroyed(IObjectLinkable item)
        {
            Object.Destroy(item.Transform.gameObject);
        }

        protected override void OnSpawned(IObjectLinkable item)
        {
//            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(IObjectLinkable item)
        {
            var transform = item.Transform;
            transform.SetPositionAndRotation(DefaultPosition, Quaternion.identity);

#if UNITY_EDITOR
            var parent = transform.transform.parent;
            if (OriginalParent == null && parent == null)
                return;

            if (OriginalParent == null && parent != null
                || parent.GetInstanceID() != OriginalParent.GetInstanceID())
            {
                transform.transform.SetParent(OriginalParent, false);
            }
#endif
        }
    }
}