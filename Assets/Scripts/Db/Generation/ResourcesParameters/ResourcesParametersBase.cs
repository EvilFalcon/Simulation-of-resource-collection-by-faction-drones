using System.Collections.Generic;
using Db.GameObjectsBase.Impl;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.Generation.ResourcesParameters
{
    [CreateAssetMenu(menuName = "Settings/ResourcesParameters", fileName = "ResourcesParameters")]
    public class ResourcesParametersBase : SerializedScriptableObject, IResourcesParameters
    {
        [SerializeField] private List<ResourceConfig> _resources = new List<ResourceConfig>();

        [field: SerializeField]
        public int TotalResourcesToSpawn { get; private set; } = 10;
        
        [field: SerializeField]
        public int MaxAttemptsPerResource { get; private set; } = 10;
        
        [field: SerializeField]
        public float MinDistanceBetween { get; private set; } = 2f;
        
        public IReadOnlyList<ResourceConfig> Resources => _resources;
    }

    public struct ResourceConfig
    {
        [Range(0.1f, 10f)] public float SpawnWeight;
        public EGameResourceType ResourceType;
        public int Amount;
    }

    public interface IResourcesParameters
    {
        IReadOnlyList<ResourceConfig> Resources { get; }
        int TotalResourcesToSpawn { get; }
        int MaxAttemptsPerResource { get; }
        float MinDistanceBetween { get; }
    }
}