using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Units
{
    [Game]
    public class UnitFractionComponent : IComponent
    {
        public EFractionType Value;
    }
}