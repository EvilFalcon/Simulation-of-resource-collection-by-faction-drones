using Core;
using Ecs.Views.Linkable.Views.Fractions;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class GameInitializer : MonoBehaviour, IInitializable
    {
        [SerializeField] private FractionBase _computerFractionBase;
        private IGameSceneProvider _gameSceneProvider;

        [SerializeField] private FractionBase _playerFractionBase;
        [SerializeField] private Terrain _terrain;

        [Inject]
        private void Construct(IGameSceneProvider gameSceneProvider)
        {
            _gameSceneProvider = gameSceneProvider;
        }

        public void Initialize()
        {
            _gameSceneProvider.Terrain = _terrain;
            _gameSceneProvider.PlayerFractionBase = _playerFractionBase;
            _gameSceneProvider.ComputerFractionBase = _computerFractionBase;
        }
    }
}