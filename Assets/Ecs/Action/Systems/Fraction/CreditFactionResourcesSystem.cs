using Db.GameObjectsBase.Impl;
using Ecs.Action.Commands.Fraction;
using Ecs.Game.Components.Fraction;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Action.Systems.Fraction
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 200, nameof(EFeatures.Common))]
    public class CreditFactionResourcesSystem : ForEachCommandUpdateSystem<CreditFactionResourcesCommand>
    {
        public CreditFactionResourcesSystem(ICommandBuffer commandBuffer) : base(commandBuffer)
        {
        }

        protected override void Execute(ref CreditFactionResourcesCommand command)
        {
            if (command.Unit.HasResourceData == false)
                return;
            
            if (command.FractionBase.HasFractionResources == false)
                return;
            
            var fractionResources = command.FractionBase.FractionResources.Value;

            fractionResources = CountingResource(command, fractionResources);

            command.FractionBase.ReplaceFractionResources(fractionResources);
            command.Unit.RemoveResourceData();
        }

        private static FractionResources CountingResource(CreditFactionResourcesCommand command, FractionResources fractionResources)
        {
            switch (command.Unit.ResourceData.ResourceType)
            {
                case EGameResourceType.Mithril:
                    fractionResources.Mithril += command.Unit.ResourceData.Amount;
                    break;
                case EGameResourceType.Сrystal:
                    fractionResources.Crystal += command.Unit.ResourceData.Amount;
                    break;
            }

            return fractionResources;
        }
    }
}