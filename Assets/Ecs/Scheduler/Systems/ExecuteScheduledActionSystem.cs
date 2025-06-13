using Ecs.Core.Interfaces;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine.Pool;

namespace Ecs.Scheduler.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1970, nameof(EFeatures.Scheduler))]
    public class ExecuteScheduledActionSystem : IUpdateSystem
    {
        private readonly IGroup<SchedulerEntity> _actionGroup;
        private readonly ITimeProvider _timeProvider;
        private readonly GameContext _game;

        public ExecuteScheduledActionSystem(
            SchedulerContext scheduler,
            ITimeProvider timeProvider,
            GameContext game
        )
        {
            _timeProvider = timeProvider;
            _game = game;
            _actionGroup = scheduler.GetGroup(SchedulerMatcher
                .AllOf(SchedulerMatcher.ScheduledAction, SchedulerMatcher.Accumulator)
                .AnyOf(SchedulerMatcher.IntervalSec, SchedulerMatcher.TimerSec)
                .NoneOf(SchedulerMatcher.Paused, SchedulerMatcher.Destroyed));
        }

        public void Update()
        {
            using var _ = ListPool<SchedulerEntity>.Get(out var buffer);
            _actionGroup.GetEntities(buffer);
            foreach (var action in buffer)
            {
                var timeScale = 1f;
                if (action.HasTimeScaleTarget)
                {
                    var target = _game.GetEntityWithUid(action.TimeScaleTarget.Value);
                    if (target.HasLocalTimeScale)
                        timeScale = target.LocalTimeScale.Value;
                }
                action.ReplaceAccumulator(action.Accumulator.Value + _timeProvider.DeltaTime * timeScale);
            }
        }
    }
}