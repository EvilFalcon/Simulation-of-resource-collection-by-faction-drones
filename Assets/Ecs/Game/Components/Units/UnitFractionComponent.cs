using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Components.Units
{
    [Game]
    public class UnitFractionComponent : IComponent
    {
        public EFractionType FractionType;
        public Vector3 HomePosition;
        public EUnitState State;
        public float Timer;
    }

    public enum EUnitState {
        Searching,
        MovingToResource,
        Collecting,
        ReturningToBase,
        Unloading
    }
}