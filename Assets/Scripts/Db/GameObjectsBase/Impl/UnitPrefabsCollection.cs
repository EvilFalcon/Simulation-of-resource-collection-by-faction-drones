using System.Collections.Generic;
using Ecs.Views.Linkable.Views.Units;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameObjectsBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/UnitBase", fileName = "UnitPrefabsCollection")]
    public class UnitPrefabsCollection : SerializedScriptableObject, IUnitPrefabsCollection
    {
        [OdinSerialize] private Dictionary<EFractionType, UnitView> _prefabs;

        #region IUnitPrefabsCollection Members

        public IReadOnlyDictionary<EFractionType, UnitView> Prefabs => _prefabs;

        #endregion
    }
}