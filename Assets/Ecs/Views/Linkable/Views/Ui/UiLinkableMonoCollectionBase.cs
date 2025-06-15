using System;
using System.Collections;
using System.Collections.Generic;
using JCMG.EntitasRedux;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Views.Ui
{
    public abstract class UiLinkableMonoCollectionBase<TEntity, TView> : MonoBehaviour,
        IUiLinkableListCollection<TEntity, TView>
        where TEntity : IEntity
        where TView : MonoBehaviour, IObjectLinkableUi<TEntity>
    {
        private readonly List<TView> _items = new();

        [SerializeField] private Transform _collectionRoot;

        [Inject] private DiContainer _container;
        [SerializeField] private TView _prefab;

        #region IUiLinkableListCollection<TEntity,TView> Members

        public TView Create()
        {
            var view = _container.InstantiatePrefabForComponent<TView>(_prefab, _collectionRoot);
            view.Reset();
            return view;
        }

        public void Resize(int size)
        {
            if (size > _items.Count)
                while (_items.Count < size)
                {
                    var item = Create();
                    _items.Add(item);
                }
            else
                while (_items.Count > size)
                {
                    var index = _items.Count - 1;
                    var item = _items[index];
                    Remove(item);
                }
        }

        public int Count => _items.Count;

        public void UnlinkAll()
        {
            foreach (var item in _items) item.Unlink();
        }

        public void Clear()
        {
            while (_items.Count > 0) Remove(_items[0]);
        }

        public IEnumerator<TView> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Remove(TView view)
        {
            if (!_items.Remove(view))
                throw new Exception($"[{GetType().Name}] No item in collection {view}");
            view.Unlink();
            view.Destroy();
        }

        public void RemoveAt(int index)
        {
            Remove(_items[index]);
        }

        public TView this[int index] => _items[index];

        #endregion
    }
}
