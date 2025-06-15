using Db.GameObjectsBase.Impl;
using UnityEngine;

namespace Db.Generation.ResourcesParameters.Impl
{
    public struct ResourceConfig
    {
        [Range(0.1f, 10f)] public float SpawnWeight;
        public EGameResourceType ResourceType;
        public int Amount;
    }
}