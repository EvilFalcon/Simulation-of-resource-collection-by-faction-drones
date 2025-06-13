using System.Collections.Generic;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1984, nameof(EFeatures.Scheduler))]
    public class IntervalSystem : ReactiveSystem<SchedulerEntity>
    {
        public IntervalSystem(SchedulerContext scheduler) : base(scheduler)
        {
        }

        protected override ICollector<SchedulerEntity> GetTrigger(IContext<SchedulerEntity> context) =>
            context.CreateCollector(SchedulerMatcher.IntervalAccumulator);

        protected override bool Filter(SchedulerEntity entity) =>
            entity.HasIntervalAccumulator
            && entity.HasIntervalSec
            && !entity.IsPaused
            && !entity.IsDestroyed;

        protected override void Execute(IEnumerable<SchedulerEntity> entities)
        {
            foreach (var action in entities)
                if (action.IntervalAccumulator.Value >= action.IntervalSec.Value)
                {
                    action.IntervalAccumulator.Value -= action.IntervalSec.Value;
                    action.ScheduledAction.Value.Invoke();
                }
        }
    }
}