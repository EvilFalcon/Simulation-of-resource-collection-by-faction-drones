using JCMG.EntitasRedux;
using SimpleUi.Interfaces;

namespace Ecs.Views.Linkable.Impl.Ui
{
    public interface IUiLinkableStaticCollection<TEntity, TView> : IUiLinkableCollection<TEntity, TView>,
        IUiStaticCollection<TView>
        where TEntity : IEntity
        where TView : IObjectLinkableUi<TEntity>
    {
    }
}