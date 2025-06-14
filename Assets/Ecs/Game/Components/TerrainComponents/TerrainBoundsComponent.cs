using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.TerrainComponents
{
    [Game]
    public class TerrainBoundsComponent : IComponent
    {
        public Vector3 Min;
        public Vector3 Max;
    }
}