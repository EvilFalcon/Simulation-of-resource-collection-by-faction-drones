using Db.GameObjectsBase.Impl;
using Ecs.Commands.Generator;

namespace Ecs.Action.Commands.Fraction
{
    [Command]
    public struct CreditFactionResourcesCommand
    {
        public GameEntity Fraction;
        public EGameResourceType ResourceType;
        public int Amaunt;
    }
}