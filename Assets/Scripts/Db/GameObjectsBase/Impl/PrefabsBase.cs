using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utils.Drawers.Key;

namespace Db.GameObjectsBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/PrefabsBase", fileName = "PrefabsBase")]
    public class PrefabsBase : SerializedScriptableObject, IPrefabsBase
    {
        [KeyValue(nameof(EObjectType.ToString))] [OdinSerialize]
        private Dictionary<EObjectType, Prefab> prefabs;

        public GameObject Get(string prefabName)
        {
            if (prefabs.TryGetValue((EObjectType)Enum.Parse(typeof(EObjectType), prefabName), out var prefab))
                return prefab.gameObject;
            
            throw new Exception($"[PrefabsBase] Can't find prefab with name: {name}");
        }

        [Serializable]
        public class Prefab
        {
            public GameObject gameObject;
        }
    }
}