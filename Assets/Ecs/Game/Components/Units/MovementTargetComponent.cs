using System.Numerics;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Units
{
    [Game]
    public class MovementTargetComponent : IComponent
    {
        public Vector3 Position;
    }
}