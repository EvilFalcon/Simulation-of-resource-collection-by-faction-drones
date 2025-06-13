using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    public class VelocityComponent : IComponent
    {
        public Vector3 Value;
    }
}