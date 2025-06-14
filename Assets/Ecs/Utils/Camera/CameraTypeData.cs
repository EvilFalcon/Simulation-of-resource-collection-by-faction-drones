using System;
using Unity.Cinemachine;

namespace Ecs.Utils.Camera
{
    [Serializable]
    public struct CameraTypeData
    {
        public EVirtualCameraType VirtualCameraType;
        public CinemachineVirtualCamera VirtualCamera;
    }
}