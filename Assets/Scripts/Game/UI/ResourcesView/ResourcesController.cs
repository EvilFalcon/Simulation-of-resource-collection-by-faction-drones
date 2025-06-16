using System;
using Core;
using Ecs.Game.Components.Fraction;
using Ecs.Utils;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using UniRx;

namespace Game.UI.Score
{
    public class ResourcesController : UiController<ResourcesView>, IUiInitializable, IDisposable
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly IGameSceneProvider _gameSceneProvider;
        private readonly ILinkedEntityRepository _repository;

        public ResourcesController(IGameSceneProvider gameSceneProvider, ILinkedEntityRepository repository)
        {
            _gameSceneProvider = gameSceneProvider;
            _repository = repository;
        }

        #region IDisposable Members

        public void Dispose()
        {
            _disposable.Dispose();
        }

        #endregion

        #region IUiInitializable Members

        public void Initialize()
        {
            var playerFraction = _repository.Get(_gameSceneProvider.PlayerFractionBase.transform.GetHashCode());
            playerFraction.SubscribeFractionResources(OnPlayerFractionResourcesChanged).AddTo(_disposable);
            var computer = _repository.Get(_gameSceneProvider.ComputerFractionBase.transform.GetHashCode());
            computer.SubscribeFractionResources(OnComputerFractionResourcesChanged).AddTo(_disposable);
        }

        #endregion

        private void OnComputerFractionResourcesChanged(GameEntity entity, FractionResources value)
        {
            View.UpdateComputerMithrilResource(value.Mithril);
            View.UpdateComputerCrystalResource(value.Crystal);
        }

        private void OnPlayerFractionResourcesChanged(GameEntity entity, FractionResources value)
        {
            View.UpdatePlayerMithrilResource(value.Mithril);
            View.UpdatePlayerCrystalResource(value.Crystal);
        }
    }
}