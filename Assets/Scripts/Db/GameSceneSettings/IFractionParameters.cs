using System.Collections.Generic;
using Db.GameObjectsBase.Impl;
using UnityEngine;

namespace Db.GameSceneSettings
{
    public interface IFractionParameters
    {
        int UnitsCount { get; }
        IReadOnlyDictionary<EFractionType, Vector3> UnitSpawnPositions { get; }
        IReadOnlyDictionary<EFractionType, Vector3> UnitFractionBasePosition { get; }
    }
}