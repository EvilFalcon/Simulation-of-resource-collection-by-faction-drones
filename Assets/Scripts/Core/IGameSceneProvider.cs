using Ecs.Views.Linkable.Impl.Faction;
using UnityEngine;

namespace Core
{
    public interface IGameSceneProvider
    {
        Terrain Terrain { get; set; }
        FractionBase PlayerFractionBase { get; set; }
        FractionBase ComputerFractionBase { get; set; }
    }
}