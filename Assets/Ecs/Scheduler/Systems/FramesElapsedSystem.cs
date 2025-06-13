using System.Collections.Generic;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Scheduler.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1990, nameof(EFeatures.Scheduler))]
    public class FramesElapsedSystem : ReactiveSystem<SchedulerEntity>
    {
        public FramesElapsedSystem(SchedulerContext scheduler) : base(scheduler)
        {
        }
 
        protected override ICollector<SchedulerEntity> GetTrigger(IContext<SchedulerEntity> context) =>
            context.CreateCollector(SchedulerMatcher.Frames);
 
        protected override bool Filter(SchedulerEntity entity) =>
            entity.HasFramesToElapse
            && entity.HasFrames
            && !entity.IsPaused
            && !entity.IsDestroyed;
 
        protected override void Execute(IEnumerable<SchedulerEntity> entities)
        {
            foreach (var action in entities)
            {
                if (action.Frames.Value >= action.FramesToElapse.Value)
                {
                    action.ScheduledAction.Value.Invoke();
                    action.IsDestroyed = true;
                }
            }
        }
    }
}