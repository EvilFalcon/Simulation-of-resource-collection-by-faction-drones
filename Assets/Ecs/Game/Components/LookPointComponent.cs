using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components
{
    [Game]
    public class LookPointComponent : IComponent
    {
        public Transform Value;
    }
}