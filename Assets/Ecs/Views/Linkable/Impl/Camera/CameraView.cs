using Game.Models.Camera;
using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Core.Utils;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Ecs.Views.Linkable.Impl.Camera
{
    public class CameraView : ObjectView
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private CinemachineBrain _brain;

        private IPlayerCameraHolder _cameraHolder;

        [Inject]
        private void Construct(IPlayerCameraHolder playerCameraHolder)
        {
            _cameraHolder = playerCameraHolder;
        }

        protected override void Subscribe(IEntity entity, IUnsubscribeEvent unsubscribe)
        {
            _cameraHolder.SetCamera(_camera);
            _cameraHolder.SetBrain(_brain);

            base.Subscribe(entity, unsubscribe);
        }

        protected override void OnRotation(GameEntity entity, Quaternion value)
        {
            base.OnRotation(entity, value);

            entity.ReplaceLookDirection(transform.forward);
        }
    }
}