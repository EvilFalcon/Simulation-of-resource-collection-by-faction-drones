using Ecs.Commands.Generator;
using UnityEngine;

namespace Ecs.Action.Commands.Input
{
    [Command]
    public struct PointerDownCommand
    {
        public Vector3 Position;
    }
}