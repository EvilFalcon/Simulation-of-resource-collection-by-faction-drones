using System.Collections.Generic;
using Ecs.Signal.Commands;
using Ecs.Utils;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;

namespace Ecs.Signal.Systems
{
	[Install(ExecutionType.Game, ExecutionPriority.Low, 2000, nameof(EFeatures.Initialization))]
	public class SignalStartSystem : ForEachCommandUpdateSystem<SignalStartCommand>
	{
		private readonly List<IUiInitializable> _uiInitializables;

		public SignalStartSystem(
			ICommandBuffer commandBuffer,
			List<IUiInitializable> uiInitializables
		) : base(commandBuffer)
		{
			_uiInitializables = uiInitializables;
		}


		protected override void Execute(ref SignalStartCommand command)
		{
			foreach (var uiInitializable in _uiInitializables)
			{
				uiInitializable.Initialize();
			}
		}
	}
}