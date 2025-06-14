using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.TerrainComponents
{
    [Game]
    [Unique]
    public class TerrainComponent : IComponent
    {
        public Terrain Value;
    }
}