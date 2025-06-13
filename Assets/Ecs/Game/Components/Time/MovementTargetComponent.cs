using System.Numerics;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Time
{
    [Game]
    public class MovementTargetComponent : IComponent
    {
        public Vector3 Position;
    }
}