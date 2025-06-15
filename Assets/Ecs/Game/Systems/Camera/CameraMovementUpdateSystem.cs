using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Utils.Camera;
using Game.Models.Camera;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Camera
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 800, nameof(EFeatures.Camera))]
    public class CameraMovementUpdateSystem : ILateSystem
    {
        private readonly ICameraHolder _cameraHolder;
        private readonly GameContext _gameContext;
        private float panSpeed = 10f;
        private float edgeThreshold = 10f; // Расстояние от края экрана, при котором начинается движение
        private Vector3 mapMinBounds; // Минимальные границы карты (X, Y)
        private Vector3 mapMaxBounds; // Максимальные границы карты (X, Y)

        public CameraMovementUpdateSystem(ICameraHolder cameraHolder, GameContext gameContext)
        {
            _cameraHolder = cameraHolder;
            _gameContext = gameContext;
        }

        public void Late()
        {
            Vector3 moveDirection = Vector3.zero;
            Vector2 mousePosition = UnityEngine.Input.mousePosition;
            var camera = _cameraHolder.GetVirtualCamera(EVirtualCameraType.Gameplay);
            var cameraPosition = camera.transform.position;

            // Проверяем, находится ли курсор у края экрана
            if (mousePosition.x <= edgeThreshold)
                moveDirection.x = -1f; // Движение влево
            else if (mousePosition.x >= Screen.width - edgeThreshold)
                moveDirection.x = 1f; // Движение вправо

            if (mousePosition.y <= edgeThreshold)
                moveDirection.z = -1f; // Движение вниз
            else if (mousePosition.y >= Screen.height - edgeThreshold)
                moveDirection.z = 1f; // Движение вверх

            // Нормализуем, если движение по диагонали
            if (moveDirection.magnitude > 1f)
                moveDirection.Normalize();

            // Вычисляем новую позицию камеры
            Vector3 newPosition = cameraPosition + moveDirection * panSpeed * Time.deltaTime;
            var mapBounds = _gameContext.TerrainEntity.TerrainBounds;
            // Ограничиваем движение границами карты
            newPosition.x = Mathf.Clamp(newPosition.x, mapBounds.Min.x + 46.5f, mapBounds.Max.x - 46.5f);
            newPosition.z = Mathf.Clamp(newPosition.z, mapBounds.Min.z + 26, mapBounds.Max.z - 26);

            // Применяем новую позицию
            camera.transform.position = newPosition;
        }
    }
}