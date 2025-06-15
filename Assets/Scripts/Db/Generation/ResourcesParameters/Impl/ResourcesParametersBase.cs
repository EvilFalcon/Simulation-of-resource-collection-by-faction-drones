using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Generation.ResourcesParameters.Impl
{
    [CreateAssetMenu(menuName = "Settings/ResourcesParameters", fileName = "ResourcesParameters")]
    public class ResourcesParametersBase : SerializedScriptableObject, IResourcesParameters
    {
        [SerializeField] private List<ResourceConfig> _resources = new List<ResourceConfig>();

        #region IResourcesParameters Members

        [field: SerializeField]
        public int TotalResourcesToSpawn { get; private set; } = 10;

        [field: SerializeField]
        public int MaxAttemptsPerResource { get; private set; } = 10;

        [field: SerializeField]
        public float MinDistanceBetween { get; private set; } = 2f;

        public IReadOnlyList<ResourceConfig> Resources => _resources;

        #endregion
    }
}