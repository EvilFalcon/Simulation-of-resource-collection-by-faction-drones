using UnityEngine;

namespace Game.Models.Input
{
    public interface IMoveController
    {
        Vector2 Velocity { get; }

        void OnJumpComplete();
    }
}