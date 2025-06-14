using System.Numerics;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    public class MovementTargetComponent : IComponent
    {
        public Vector3 Position;
    }
}