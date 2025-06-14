using System.Collections.Generic;
using Db.GameObjectsBase.Impl;
using UnityEngine;

namespace Db.GameObjectsBase
{
    public interface IUnitBase
    {
        IReadOnlyDictionary<EFractionType, GameObject> Prefabs { get; }
    }
}