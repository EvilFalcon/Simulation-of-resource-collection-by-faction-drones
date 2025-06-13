using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using JCMG.EntitasRedux.Core.View;
using SimpleUi.Interfaces;

namespace Ecs.Views.Linkable
{
    public interface IObjectLinkableUi<in TEntity> : IUiView, IObjectLinkable, ILinkable
        where TEntity : IEntity
    {
        void Subscribe(TEntity entity, IUnsubscribeEvent unsubscribe);
        void Unsubscribe();
        void Reset();
    }
}