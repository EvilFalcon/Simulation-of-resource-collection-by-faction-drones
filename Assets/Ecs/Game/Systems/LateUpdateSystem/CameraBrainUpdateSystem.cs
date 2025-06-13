using Ecs.Core.Interfaces;
using Ecs.Utils;
using Game.Models.Camera;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Camera
{
	[Install(ExecutionType.Game, ExecutionPriority.Urgent, 0, nameof(EFeatures.Camera))]
	public class CameraBrainUpdateSystem : ILateSystem
	{
		private readonly IPlayerCameraHolder _playerCameraHolder;

		public CameraBrainUpdateSystem(IPlayerCameraHolder playerCameraHolder)
		{
			_playerCameraHolder = playerCameraHolder;
		}

		public void Late()
		{
			_playerCameraHolder.ManualUpdate();
		}
	}
}