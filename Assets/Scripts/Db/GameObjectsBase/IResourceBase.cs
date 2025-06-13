using System.Collections.Generic;
using UnityEngine;

namespace Db.GameObjectsBase.Impl
{
    public interface IResourceBase
    {
        IReadOnlyDictionary<EGameResourceType, GameObject> Prefabs { get; }
    }
}