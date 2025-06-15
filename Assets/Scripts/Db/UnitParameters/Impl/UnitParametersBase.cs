using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.UnitParameters.Impl
{
    [CreateAssetMenu(menuName = "Settings/UnitParameters", fileName = "UnitParameters")]
    public class UnitParametersBase : SerializedScriptableObject, IUnitParameters
    {
        #region IUnitParameters Members

        [BoxGroup("NavMeshAgent Settings")]
        [field: SerializeField]
        public float Speed { get; private set; } = 3.5f;

        [BoxGroup("NavMeshAgent Settings")]
        [field: SerializeField]
        public float AngularSpeed { get; private set; } = 120;

        [BoxGroup("NavMeshAgent Settings")]
        [field: SerializeField]
        public float StoppingDistance { get; private set; } = 1;

        #endregion
    }
}