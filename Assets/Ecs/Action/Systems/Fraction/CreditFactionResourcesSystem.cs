using Db.GameObjectsBase.Impl;
using Ecs.Action.Commands.Fraction;
using Ecs.Game.Components.Fraction;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Fraction
{
    public class CreditFactionResourcesSystem : ForEachCommandUpdateSystem<CreditFactionResourcesCommand>
    {
        public CreditFactionResourcesSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
        }

        protected override void Execute(ref CreditFactionResourcesCommand command)
        {
            var fractionResources = command.Fraction.FractionResources.Value;

            fractionResources = CountingResource(command, fractionResources);

            command.Fraction.ReplaceFractionResources(fractionResources);
        }

        private static FractionResources CountingResource(CreditFactionResourcesCommand command, FractionResources fractionResources)
        {
            switch (command.ResourceType)
            {
                case EGameResourceType.Mithril:
                    fractionResources.Mithril += command.Amaunt;
                    break;
                case EGameResourceType.Сrystal:
                    fractionResources.Crystal += command.Amaunt;
                    break;
            }

            return fractionResources;
        }
    }
}