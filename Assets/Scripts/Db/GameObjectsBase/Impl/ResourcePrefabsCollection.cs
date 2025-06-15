using System.Collections.Generic;
using Ecs.Views.Linkable.Views.ResourcesView;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameObjectsBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/ResourceBase", fileName = "ResourcePrefabsCollection")]
    public class ResourcePrefabsCollection : SerializedScriptableObject, IResourcePrefabsCollection
    {
        [OdinSerialize]
        [HideInInspector]
        private Dictionary<EGameResourceType, IReadOnlyList<ResourceView>> _prefabs;

        [OdinSerialize]
        private Dictionary<EGameResourceType, List<ResourceView>> _resourcePrefabsCollection;

        #region IResourcePrefabsCollection Members

        public IReadOnlyDictionary<EGameResourceType, IReadOnlyList<ResourceView>> Prefabs => _prefabs;

        #endregion

#if UNITY_EDITOR
        private void OnValidate()
        {
            _prefabs = new Dictionary<EGameResourceType, IReadOnlyList<ResourceView>>();

            foreach (var pair in _resourcePrefabsCollection)
            {
                _prefabs[pair.Key] = pair.Value;
            }
        }
#endif
    }
}