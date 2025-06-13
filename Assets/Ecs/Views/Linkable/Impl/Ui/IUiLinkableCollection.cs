using JCMG.EntitasRedux;
using SimpleUi.Interfaces;

namespace Ecs.Views.Linkable.Impl.Ui
{
    public interface IUiLinkableCollection<TEntity, TView> : IUiCollectionBase<TView>
        where TEntity : IEntity
        where TView : IObjectLinkableUi<TEntity>
    {
        void Resize(int size);
        void UnlinkAll();
    }
}