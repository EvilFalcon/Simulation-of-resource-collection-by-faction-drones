using Db.GameObjectsBase.Impl;
using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands.UnitsFraction
{
    [Command]
    public struct CreateUnitsFractionCommand
    {
        public EFractionType FractionType;
        public Vector3 FractionBasePosition;
        public Vector3 UnitSpawnPosition;
    }
}