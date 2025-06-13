using System.Collections.Generic;
using Db.GameObjectsBase.Impl;
using UnityEngine;

namespace Db.GameObjectsBase
{
    public interface IUnitBase
    {
        IReadOnlyDictionary<EUnitType, GameObject> Prefabs { get; }
    }
}