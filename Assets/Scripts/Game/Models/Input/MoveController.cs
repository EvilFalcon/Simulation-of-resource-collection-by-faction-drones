using UnityEngine;

namespace Game.Models.Input
{
    public class MoveController : IMoveController
    {
        #region IMoveController Members

        public Vector2 Velocity { get; }

        public void OnJumpComplete()
        {
            
        }

        #endregion
    }
}