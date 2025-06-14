using Ecs.Utils.Camera;
using Unity.Cinemachine;

namespace Game.Models.Camera
{
    public interface ICameraHolder
    {
        UnityEngine.Camera GetCamera();
        CinemachineVirtualCamera GetVirtualCamera(EVirtualCameraType type);
    }
}