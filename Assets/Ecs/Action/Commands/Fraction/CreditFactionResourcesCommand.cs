using Ecs.Commands.Generator;

namespace Ecs.Action.Commands.Fraction
{
    [Command]
    public struct CreditFactionResourcesCommand
    {
        public GameEntity FractionBase;
        public GameEntity Unit;
    }
}