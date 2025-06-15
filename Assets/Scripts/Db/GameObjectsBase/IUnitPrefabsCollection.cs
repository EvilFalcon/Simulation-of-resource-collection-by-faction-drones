using System.Collections.Generic;
using Db.GameObjectsBase.Impl;
using Ecs.Views.Linkable.Views.Units;
using UnityEngine;

namespace Db.GameObjectsBase
{
    public interface IUnitPrefabsCollection
    {
        IReadOnlyDictionary<EFractionType, UnitView> Prefabs { get; }
    }
}