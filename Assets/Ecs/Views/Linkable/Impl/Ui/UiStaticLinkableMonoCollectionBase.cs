using System;
using System.Collections;
using System.Collections.Generic;
using JCMG.EntitasRedux;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Ui
{
    public abstract class UiStaticLinkableMonoCollectionBase<TEntity, TView> : MonoBehaviour,
        IUiLinkableStaticCollection<TEntity, TView>
        where TEntity : IEntity
        where TView : MonoBehaviour, IObjectLinkableUi<TEntity>
    {
        [SerializeField] private List<TView> items = new();

        [Inject]
        public void Configure(DiContainer diContainer)
        {
            foreach (var item in items)
            {
                diContainer.Inject(item);
                item.Reset();
            }
        }

        public int Count => items.Count;

        public void Clear()
        {
            while (items.Count > 0) Remove(items[0]);
        }

        public IEnumerator<TView> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Remove(TView view)
        {
            if (!items.Remove(view))
                throw new Exception($"[{GetType().Name}] No item in collection {view}");
            view.Unlink();
            view.Destroy();
        }

        public void RemoveAt(int index)
        {
            Remove(items[index]);
        }

        public TView this[int index] => items[index];

        public void Resize(int size)
        {
        }

        public void UnlinkAll()
        {
            foreach (var item in items) item.Unlink();
        }
    }
}