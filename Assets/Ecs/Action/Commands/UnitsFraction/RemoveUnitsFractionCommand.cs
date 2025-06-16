using Db.GameObjectsBase.Impl;
using Ecs.Commands.Generator;

namespace Ecs.Action.Commands.UnitsFraction
{
    [Command]
    public struct RemoveUnitsFractionCommand
    {
        public EFractionType FractionType;
    }
}