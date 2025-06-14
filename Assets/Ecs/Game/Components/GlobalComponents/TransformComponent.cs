using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.GlobalComponents
{
    [Game]
    public class TransformComponent : IComponent
    {
        public Transform Value;
    }
}