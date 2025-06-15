using System.Collections.Generic;
using Ecs.Utils.Camera;
using Unity.Cinemachine;

namespace Game.Models.Camera.Impl
{
    public class PlayerCameraHolder : IPlayerCameraHolder
    {
        private const int ACTIVE_PRIORITY = 10;
        private const int INACTIVE_PRIORITY = 0;

        private CinemachineBrain _brain;
        private UnityEngine.Camera _camera;
        private IReadOnlyDictionary<EVirtualCameraType, CinemachineVirtualCamera> _cameras;

        #region IPlayerCameraHolder Members

        public void Init(IReadOnlyDictionary<EVirtualCameraType, CinemachineVirtualCamera> cameras)
        {
            _cameras = cameras;
        }

        public UnityEngine.Camera GetCamera() => _camera;

        public CinemachineVirtualCamera GetVirtualCamera(EVirtualCameraType type)
        {
            return _cameras[type];
        }

        public void SetVirtualCamera(EVirtualCameraType virtualCameraType)
        {
            if (_cameras is null)
                return;

            foreach (var (type, camera) in _cameras)
            {
                var typesMatch = type == virtualCameraType;
                camera.Priority = typesMatch ? ACTIVE_PRIORITY : INACTIVE_PRIORITY;
            }
        }

        public void SetBrain(CinemachineBrain brain)
        {
            _brain = brain;
        }

        public void SetCamera(UnityEngine.Camera camera)
        {
            _camera = camera;
        }

        public void ManualUpdate()
        {
            if (_brain == null)
                return;

            _brain.ManualUpdate();
        }

        #endregion
    }
}