//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Commands Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Ecs.Commands.Generator;
using Ecs.Signal.Commands;
using Ecs.Action.Commands.Fraction;
using UnityEngine;
using Ecs.Action.Commands.Input;
using Db.GameObjectsBase.Impl;
using Ecs.Action.Commands.UnitsFraction;
using JCMG.EntitasRedux.Commands;


namespace Generated.Commands
{
    public static partial class CommandBufferExtensions
    {

        public static void SignalStart(this ICommandBuffer commandBuffer)
        {
            ref var command = ref commandBuffer.Create<SignalStartCommand>();
        }

        public static void CreditFactionResources(this ICommandBuffer commandBuffer, GameEntity fractionBase, GameEntity unit)
        {
            ref var command = ref commandBuffer.Create<CreditFactionResourcesCommand>();
            command.FractionBase = fractionBase;
            command.Unit = unit;
        }

        public static void PointerDown(this ICommandBuffer commandBuffer, Vector3 position)
        {
            ref var command = ref commandBuffer.Create<PointerDownCommand>();
            command.Position = position;
        }

        public static void PointerMove(this ICommandBuffer commandBuffer, Vector3 position)
        {
            ref var command = ref commandBuffer.Create<PointerMoveCommand>();
            command.Position = position;
        }

        public static void PointerUp(this ICommandBuffer commandBuffer)
        {
            ref var command = ref commandBuffer.Create<PointerUpCommand>();
        }

        public static void CreateUnitsFraction(this ICommandBuffer commandBuffer, EFractionType fractionType, Vector3 fractionBasePosition, Vector3 unitSpawnPosition)
        {
            ref var command = ref commandBuffer.Create<CreateUnitsFractionCommand>();
            command.FractionType = fractionType;
            command.FractionBasePosition = fractionBasePosition;
            command.UnitSpawnPosition = unitSpawnPosition;
        }

        public static void RemoveUnitsFraction(this ICommandBuffer commandBuffer, EFractionType fractionType)
        {
            ref var command = ref commandBuffer.Create<RemoveUnitsFractionCommand>();
            command.FractionType = fractionType;
        }

    }
}
