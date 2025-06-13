using System.Collections.Generic;
using Ecs.Utils.Camera;
using Unity.Cinemachine;

namespace Game.Models.Camera
{
	public interface IPlayerCameraHolder : ICameraHolder
	{
		void Init(IReadOnlyDictionary<EVirtualCameraType, CinemachineVirtualCamera> cameras);
		void SetVirtualCamera(EVirtualCameraType cameraType);
		void SetBrain(CinemachineBrain brain);
		void SetCamera(UnityEngine.Camera camera);
		void ManualUpdate();
	}
}