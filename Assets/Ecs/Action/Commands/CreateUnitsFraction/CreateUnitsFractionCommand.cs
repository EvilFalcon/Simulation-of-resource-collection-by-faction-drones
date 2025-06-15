using Db.GameObjectsBase.Impl;
using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands.CreateUnitsFraction
{
    [Command]
    public struct CreateUnitsFractionCommand
    {
        public int UnitsCount;
        public EFractionType FractionType;
        public Vector3 FractionBasePosition;
    }
}