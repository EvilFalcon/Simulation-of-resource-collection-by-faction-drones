using JCMG.EntitasRedux;
using SimpleUi.Interfaces;

namespace Ecs.Views.Linkable.Views.Ui
{
    public interface IUiLinkableListCollection<TEntity, TView> : IUiLinkableCollection<TEntity, TView>,
        IUiListCollection<TView>
        where TEntity : IEntity
        where TView : IObjectLinkableUi<TEntity>
    {}
}