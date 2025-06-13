using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    public class TransformComponent : IComponent
    {
        public Transform Value;
    }
}