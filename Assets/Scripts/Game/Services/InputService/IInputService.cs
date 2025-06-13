using Ecs.Action.Commands.Input;
using UnityEngine;

namespace Game.Services.InputService
{
    public interface IInputService
    {
        Ray GetRayOfTouch(ref PointerDownCommand command);
        Vector3 GetTouchPosition(ref PointerMoveCommand command);
    }
}