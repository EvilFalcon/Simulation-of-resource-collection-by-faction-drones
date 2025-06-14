using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    public class UnitPrefabComponent : IComponent
    {
        public EFractionType Value;
    }
}