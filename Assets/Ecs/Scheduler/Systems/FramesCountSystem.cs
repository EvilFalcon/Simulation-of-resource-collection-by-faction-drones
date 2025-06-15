using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine.Pool;

namespace Ecs.Scheduler.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 1980, nameof(EFeatures.Scheduler))]
    public class FramesCountSystem : IUpdateSystem
    {
        private readonly IGroup<SchedulerEntity> _actionGroup;

        public FramesCountSystem(SchedulerContext scheduler)
        {
            _actionGroup = scheduler.GetGroup(SchedulerMatcher.AllOf(SchedulerMatcher.ScheduledAction, SchedulerMatcher.Frames)
                .NoneOf(SchedulerMatcher.Paused, SchedulerMatcher.Destroyed));
        }
        
        public void Update()
        {
            using var _ = ListPool<SchedulerEntity>.Get(out var buffer);
 
            _actionGroup.GetEntities(buffer);
 
            foreach (var action in buffer)
            {
                var currentFrames = action.Frames.Value;
 
                action.ReplaceFrames(currentFrames + 1);   
            }
        }
    }
}