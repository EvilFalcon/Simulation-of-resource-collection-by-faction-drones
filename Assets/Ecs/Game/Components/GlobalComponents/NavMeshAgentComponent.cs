using JCMG.EntitasRedux;
using UnityEngine.AI;

namespace Ecs.Game.Components.GlobalComponents
{
    [Game]
    public class NavMeshAgentComponent : IComponent
    {
        public NavMeshAgent Value;
    }
}