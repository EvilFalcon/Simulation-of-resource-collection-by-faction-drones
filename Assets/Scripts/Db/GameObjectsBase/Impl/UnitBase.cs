using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameObjectsBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/UnitBase", fileName = "UnitPrefabsCollection")]
    public class UnitBase : SerializedScriptableObject, IUnitBase
    {
        [OdinSerialize] private Dictionary<EFractionType, GameObject> _prefabs;

        public IReadOnlyDictionary<EFractionType, GameObject> Prefabs => _prefabs;
    }
}