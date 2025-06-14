using Db.GameObjectsBase.Impl;
using JCMG.EntitasRedux;

namespace Ecs.Common.Components
{
    [Game]
    public class UnitPrefabComponent : IComponent
    {
        public EFractionType Value;
    }
}