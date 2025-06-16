using System.Globalization;
using Db.GameObjectsBase.Impl;
using Db.GameSceneSettings;
using Db.UnitParameters;
using Ecs.Utils.Groups;
using Game.Services.Spawners;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using SimpleUi.Abstracts;
using UniRx;
using Zenject;

namespace Game.UI.DebugView
{
    public class DebugController : UiController<DebugView>, IInitializable
    {
        private const string UnitsCountTextFormat = "Number of units: {0}";
        private const string UnitsSpeedTextFormat = "Units speed: {0}";
        private const string NavMeshToggleTextFormat = "NavMeshPathVisualizer: {0}";

        private readonly IResourcesSpawner _resourcesSpawner;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly IFractionParameters _fractionParameters;
        private readonly IUnitParameters _unitParameters;
        private readonly ICommandBuffer _commandBuffer;

        private bool _isNavMeshAgentEnabled;
        private int _unitsCount;

        public DebugController(
            IResourcesSpawner resourcesSpawner,
            IGameGroupUtils gameGroupUtils,
            IFractionParameters fractionParameters,
            IUnitParameters unitParameters,
            ICommandBuffer commandBuffer)
        {
            _resourcesSpawner = resourcesSpawner;
            _gameGroupUtils = gameGroupUtils;
            _fractionParameters = fractionParameters;
            _unitParameters = unitParameters;
            _commandBuffer = commandBuffer;
        }

        public void Initialize()
        {
            _unitsCount = _fractionParameters.UnitsCount;
            InitializeUIElements();
            SetupEventSubscriptions();
        }

        private void InitializeUIElements()
        {
            View.CountUnitsText.text = string.Format(UnitsCountTextFormat, _fractionParameters.UnitsCount);
            View.UnitsCountSlider.value = _fractionParameters.UnitsCount;

            View.UnitsSpeedText.text = string.Format(UnitsSpeedTextFormat, _unitParameters.Speed);
            View.UnitsSpeedSlider.value = _unitParameters.Speed;

            View.SpawnDelay.text = _resourcesSpawner.SpawnDelay.ToString(CultureInfo.InvariantCulture);
            UpdateNavMeshToggleText();
        }

        private void SetupEventSubscriptions()
        {
            View.UnitsCountSlider.OnValueChangedAsObservable()
                .Subscribe(_ => OnUnitsCountChanged())
                .AddTo(View);

            View.UnitsSpeedSlider.OnValueChangedAsObservable()
                .Subscribe(_ => OnUnitsSpeedChanged())
                .AddTo(View);

            View.SpawnDelay.OnValueChangedAsObservable()
                .Subscribe(_ => OnSpawnDelayChanged())
                .AddTo(View);

            View.NavMeshAgentButton.OnClickAsObservable()
                .Subscribe(_ => ToggleNavMeshAgent())
                .AddTo(View);
        }

        private void ToggleNavMeshAgent()
        {
            _isNavMeshAgentEnabled = !_isNavMeshAgentEnabled;
            UpdateNavMeshToggleText();
            UpdateUnitsRayStatus();
        }

        private void UpdateNavMeshToggleText()
        {
            View.NavMeshAgentTogle.text = string.Format(NavMeshToggleTextFormat, _isNavMeshAgentEnabled);
        }

        private void UpdateUnitsRayStatus()
        {
            using var unitsGroup = _gameGroupUtils.GetUnits(out var units);
            
            foreach (var unit in units)
            {
                unit.IsRay = _isNavMeshAgentEnabled;
            }
        }

        private void OnSpawnDelayChanged()
        {
            if (float.TryParse(View.SpawnDelay.text, out var delayValue))
            {
                _resourcesSpawner.SetSpawnDelay(delayValue);
            }
        }

        private void OnUnitsSpeedChanged()
        {
            var newSpeed = View.UnitsSpeedSlider.value;
            UpdateUnitsSpeed(newSpeed);
            UpdateSpeedText(newSpeed);
        }

        private void UpdateUnitsSpeed(float speed)
        {
            using var unitsGroup = _gameGroupUtils.GetUnits(out var units);

            foreach (var unit in units)
            {
                unit.NavMeshAgent.Value.speed = speed;
            }
        }

        private void UpdateSpeedText(float speed)
        {
            View.UnitsSpeedText.text = string.Format(UnitsSpeedTextFormat, speed);
        }

        private void OnUnitsCountChanged()
        {
            if (InFocus == false)
                return;
            
            int targetCount = (int)View.UnitsCountSlider.value;
            int difference = targetCount - _unitsCount;
            
            if (difference > 0)
            {
                AddUnitsToFractions();
                _unitsCount++;
            }
            else
            {
                RemoveAllUnits();
                _unitsCount--;
            }

            UpdateUnitsCountText(targetCount);
        }

        private void AddUnitsToFractions()
        {
            AddUnitsToFraction(EFractionType.FractionPlayer);
            AddUnitsToFraction(EFractionType.FractionСomputer);
        }

        private void AddUnitsToFraction(EFractionType fractionType)
        {
            _commandBuffer.CreateUnitsFraction(
                fractionType,
                _fractionParameters.UnitFractionBasePosition[fractionType],
                _fractionParameters.UnitSpawnPositions[fractionType]);
        }

        private void RemoveAllUnits()
        {
            _commandBuffer.RemoveUnitsFraction(EFractionType.FractionPlayer);
            _commandBuffer.RemoveUnitsFraction(EFractionType.FractionСomputer);
        }

        private void UpdateUnitsCountText(int count)
        {
            View.CountUnitsText.text = string.Format(UnitsCountTextFormat, count);
        }
    }
}