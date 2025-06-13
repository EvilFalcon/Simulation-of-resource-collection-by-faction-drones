using Ecs.Action.Commands.Input;
using Game.Models.Camera;
using PdUtils.Interfaces;
using UnityEngine;

namespace Game.Services.InputService.Impl
{
    public class InputService : IInputService, IUiInitializable
    {
        private readonly IPlayerCameraHolder _cameraHolder;
        private Camera _camera;

        public InputService(IPlayerCameraHolder cameraHolder)
        {
            _cameraHolder = cameraHolder;
        }
        
        public void Initialize()
        {
            _camera = _cameraHolder.GetCamera();
        }
        
        public Ray GetRayOfTouch(ref PointerDownCommand command)
        {
            var touchPosition = command.Position;
            
            return _camera.ScreenPointToRay(touchPosition);
        }

        public Vector3 GetTouchPosition(ref PointerMoveCommand command)
        {
            var touchPosition = command.Position;
            touchPosition.z = -_camera.transform.position.z - 1;
            
            return _camera.ScreenToWorldPoint(touchPosition);
        }
    }
}