using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameObjectsBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/ResourceBase", fileName = "ResourcePrefabsCollection")]
    public class ResourceBase : SerializedScriptableObject, IResourceBase
    {
        [OdinSerialize] private Dictionary<EGameResourceType, GameObject> _prefabs;

        public IReadOnlyDictionary<EGameResourceType, GameObject> Prefabs => _prefabs;
    }
}