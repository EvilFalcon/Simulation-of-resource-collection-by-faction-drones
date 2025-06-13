using Ecs.Core.Interfaces;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Core
{
	[Install(ExecutionType.Game, ExecutionPriority.Urgent, -10000, nameof(EFeatures.Common))]
	public class UnityTimeProvider : ITimeProvider, IUpdateSystem, IFixedSystem
	{
		public float Time { get; private set; }
		public float DeltaTime { get; private set; }
		public float FixedDeltaTime { get; private set; }
		public float TimeScale => UnityEngine.Time.timeScale;

		public void Update()
		{
			Time = UnityEngine.Time.time;
			DeltaTime = UnityEngine.Time.deltaTime;
		}

		public void Fixed()
		{
			FixedDeltaTime = UnityEngine.Time.fixedDeltaTime;
		}
	}
}