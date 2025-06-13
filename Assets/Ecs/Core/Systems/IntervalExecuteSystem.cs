using Ecs.Core.Interfaces;
using JCMG.EntitasRedux;

namespace Ecs.Core.Systems
{
	public abstract class IntervalExecuteSystem : IUpdateSystem
	{
		private readonly ITimeProvider _timeProvider;

		private float _accumulator;

		protected IntervalExecuteSystem(ITimeProvider timeProvider, float interval)
		{
			_timeProvider = timeProvider;
			Interval = interval;
		}

		protected float Interval { get; }


		public void Update()
		{
			_accumulator += _timeProvider.DeltaTime;

			while (_accumulator >= Interval)
			{
				_accumulator -= Interval;
				UpdateInterval();
			}
		}

		protected abstract void UpdateInterval();
	}
}