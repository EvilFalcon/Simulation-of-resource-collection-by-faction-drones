using Ecs.Core.Interfaces;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine.Pool;

namespace Ecs.Scheduler.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1970, nameof(EFeatures.Scheduler))]
    public class IntervalAccumulatorSystem : IUpdateSystem
    {
        private readonly IGroup<SchedulerEntity> _actionGroup;
        private readonly ITimeProvider _timeProvider;

        public IntervalAccumulatorSystem(SchedulerContext scheduler, ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _actionGroup = scheduler.GetGroup(SchedulerMatcher.AllOf(SchedulerMatcher.ScheduledAction,
                    SchedulerMatcher.IntervalAccumulator)
                .AnyOf(SchedulerMatcher.IntervalSec, SchedulerMatcher.TimerSec)
                .NoneOf(SchedulerMatcher.Paused, SchedulerMatcher.Destroyed));
        }

        public void Update()
        {
            using var _ = ListPool<SchedulerEntity>.Get(out var buffer);
            _actionGroup.GetEntities(buffer);
           
            foreach (var action in buffer)
            {
                action.ReplaceIntervalAccumulator(action.IntervalAccumulator.Value + _timeProvider.DeltaTime);   
            }
        }
    }
}