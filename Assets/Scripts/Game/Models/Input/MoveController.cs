using UnityEngine;

namespace Game.Models.Input
{
    public class MoveController : IMoveController
    {
        public Vector2 Velocity { get; }
        public void OnJumpComplete()
        {
            
        }
    }
}