using Zenject;

namespace Ecs.Views.Linkable.Impl
{
    public class TerrainView : ObjectView
    {
        private GameContext _gameContext;

        [Inject]
        private void Construct(GameContext gameContext)
        {
            _gameContext = gameContext;
            var entity = _gameContext.CreateEntity();
            entity.AddLink(this);
        }
    }
}