using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.GlobalComponents
{
    [Game]
    [Event(EventTarget.Self)]
    public class LocalPositionComponent : IComponent
    {
        public Vector3 Value;
    }
}