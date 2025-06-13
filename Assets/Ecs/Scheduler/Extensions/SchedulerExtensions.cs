using Ecs.Managers;
using UnityEngine.Pool;

namespace Ecs.Scheduler.Extensions
{
	public static class SchedulerExtensions
	{
		public static SchedulerEntity CreateTimerAction(
			this SchedulerContext context,
			System.Action action,
			float timerSec,
			Uid? uid = null,
			Uid? timeScaleTarget = null
		)
		{
			var entity = context.CreateEntity();
			entity.AddUid(uid ?? UidGenerator.Next());
			entity.AddAccumulator(0);
			entity.AddScheduledAction(action);
			entity.AddTimerSec(timerSec);
			entity.AddElapsedSec(timerSec);

			if (timeScaleTarget.HasValue)
				entity.AddTimeScaleTarget(timeScaleTarget.Value);

			return entity;
		}
		
		public static SchedulerEntity CreateSkipFramesAction(
			this SchedulerContext context,
			System.Action action,
			int frames,
			Uid? uid = null
		)
		{
			var entity = context.CreateEntity();
			entity.AddUid(uid ?? UidGenerator.Next());
			entity.AddFramesToElapse(frames);
			entity.AddScheduledAction(action);
			entity.AddFrames(-1); //because we add one frame in this frame
      
			return entity;
		}

		public static SchedulerEntity CreateIntervalAction(
			this SchedulerContext context,
			System.Action action,
			float intervalSec,
			Uid? uid = null
		)
		{
			var entity = context.CreateEntity();
			entity.AddUid(uid ?? UidGenerator.Next());
			entity.AddIntervalAccumulator(0);
			entity.AddScheduledAction(action);
			entity.AddIntervalSec(intervalSec);
			return entity;
		}

		public static SchedulerEntity CreateIntervalActionWithTimer(
			this SchedulerContext context,
			System.Action action,
			float intervalSec,
			float timerSec,
			Uid? uid = null
		)
		{
			var entity = CreateIntervalAction(context, action, intervalSec, uid);
			entity.AddAccumulator(0);
			entity.AddTimerSec(timerSec);
			return entity;
		}

		public static SchedulerEntity CreateIntervalActionWithElapsed(
			this SchedulerContext context,
			System.Action action,
			float intervalSec,
			float elapsedSec,
			Uid? uid = null
		)
		{
			var entity = CreateIntervalAction(context, action, intervalSec, uid);
			entity.AddElapsedSec(elapsedSec);
			return entity;
		}

		public static SchedulerEntity GetEntityByName(this SchedulerContext context, string name)
		{
			using var _ = ListPool<SchedulerEntity>.Get(out var buffer);
			var group = context.GetGroup(SchedulerMatcher.AllOf(SchedulerMatcher.Name)
				.NoneOf(SchedulerMatcher.Destroyed));
			group.GetEntities(buffer);
			SchedulerEntity res = null;
			foreach (var schedulerEntity in buffer)
				if (schedulerEntity.Name.Value.Equals(name))
				{
					res = schedulerEntity;
					break;
				}
			return res;
		}
	}
}