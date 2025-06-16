using System.Collections.Generic;
using Db.GameObjectsBase.Impl;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.GameSceneSettings.Impl
{
    [CreateAssetMenu(menuName = "Settings/FractionParameters", fileName = "FractionParameters")]
    public class FractionParameters : SerializedScriptableObject ,IFractionParameters
    {
        [OdinSerialize] private Dictionary<EFractionType, Vector3> _unitSpawnPositions;
        [OdinSerialize] private Dictionary<EFractionType, Vector3> _unitFractionBasePosition;

        #region IFractionParameters Members

        public IReadOnlyDictionary<EFractionType, Vector3> UnitSpawnPositions => _unitSpawnPositions;
        public IReadOnlyDictionary<EFractionType, Vector3> UnitFractionBasePosition => _unitFractionBasePosition;
        [field: SerializeField] public int UnitsCount { get; private set; }

        #endregion
    }
}  