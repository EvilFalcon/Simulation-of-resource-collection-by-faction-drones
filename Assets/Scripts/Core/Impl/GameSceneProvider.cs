using Ecs.Views.Linkable.Views.Fractions;
using UnityEngine;

namespace Core.Impl
{
    public class GameSceneProvider : IGameSceneProvider
    {
        #region IGameSceneProvider Members

        public Terrain Terrain { get; set; }
        public FractionBase PlayerFractionBase { get; set; }
        public FractionBase ComputerFractionBase { get; set; }

        #endregion
    }
}