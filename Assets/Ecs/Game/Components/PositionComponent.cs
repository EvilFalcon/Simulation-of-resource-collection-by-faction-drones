using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    public class PositionComponent : IComponent
    {
        public Vector3 Value;
    }
}