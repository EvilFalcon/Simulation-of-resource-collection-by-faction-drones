using System.Collections.Generic;
using Ecs.Managers;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.SpawnersComponents
{
    [Game]
    public class SpawnHistoryComponent : IComponent
    {
        public int TotalSpawned;
        public List<Uid> Spawned;
    }
}