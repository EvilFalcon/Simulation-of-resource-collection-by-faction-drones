using Ecs.Views.Linkable.Impl.Faction;
using UnityEngine;

namespace Core.Impl
{
    public class GameSceneProvider : IGameSceneProvider
    {
        public Terrain Terrain { get; set; }
        public FractionBase PlayerFractionBase { get; set; }
        public FractionBase ComputerFractionBase { get; set; }
    }
}