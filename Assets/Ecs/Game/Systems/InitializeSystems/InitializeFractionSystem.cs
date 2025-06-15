using Core;
using Db.GameObjectsBase.Impl;
using Db.GameSceneSettings;
using Ecs.Game.Components.Units;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Services.Pools.Impls.Unit;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;
using JCMG.EntitasRedux.Core.View.Impls;

namespace Ecs.Game.Systems.InitializeSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 30, nameof(EFeatures.Initialization))]
    public class InitializeFractionSystem : IInitializeSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly IFractionParameters _fractionParameters;
        private readonly GameContext _gameContext;
        private readonly IGameSceneProvider _gameSceneProvider;
        private readonly ILinkedEntityRepository _linkedEntityRepository;

        public InitializeFractionSystem(
            IGameSceneProvider gameSceneProvider,
            ICommandBuffer commandBuffer,
            IFractionParameters fractionParameters,
            ILinkedEntityRepository linkedEntityRepository,
            GameContext gameContext)
        {
            _gameSceneProvider = gameSceneProvider;
            _commandBuffer = commandBuffer;
            _fractionParameters = fractionParameters;
            _linkedEntityRepository = linkedEntityRepository;
            _gameContext = gameContext;
        }

        #region IInitializeSystem Members

        public void Initialize()
        {
            InitializeBase(EFractionType.FractionPlayer, _gameSceneProvider.PlayerFractionBase);
            InitializeBase(EFractionType.FractionСomputer, _gameSceneProvider.ComputerFractionBase);
        }

        #endregion

        private void InitializeBase(EFractionType fraction, LinkableView fractionBase)
        {
            var fractionEntity = _gameContext.CreateFraction(fraction);
            fractionBase.Link(fractionEntity);
            fractionEntity.AddPosition(fractionBase.transform.position);
            _commandBuffer.CreateUnitsFraction(_fractionParameters.UnitsCount, fraction, fractionEntity.Position.Value);
            _linkedEntityRepository.Add(fractionBase.transform.GetHashCode(), fractionEntity);
        }
    }
}