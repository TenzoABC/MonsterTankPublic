using UnityEngine;
using Common;
using Common.Interfaces;
using Common.Events;
using Common.Enums;

namespace Core
{
    /// <summary>
    /// Класс, используемый для перемещения камеры игрока
    /// </summary>
    public class CameraController : MonoBehaviour, IValidation, IClearable
    {
        /// <summary>
        /// Ссылка на компонент Transform камеры
        /// </summary>
        [SerializeField]
        private Transform cameraInstance = null;

        /// <summary>
        /// Расстояние, на которое должна быть удалена
        /// камера от объекта игрока (танка)
        /// </summary>
        [SerializeField]
        private Vector3 differenceOfPositions = new Vector3(0, -30, 30);

        /// <summary>
        /// Стартовая позиция камеры, в которой должна
        /// находиться камера в периоды неактивности
        /// </summary>
        private Vector3 startPositionOfCamera = Vector3.zero;

        private void Start()
        {
            Validation();
            startPositionOfCamera = cameraInstance.position;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (!cameraInstance)
                Debug.LogError("Instance \"Camera\" not set in " + this);
            Camera camera = cameraInstance.GetComponent<Camera>();
            if (!camera)
                Debug.LogError("Current object on \"cameraInstance\" haven't component \"Camera\". " + this);
        }

        private void LateUpdate()
        {
            if (GameController.GameIsStarted)
                Movement();
        }

        /// <summary>
        /// Перемещение камеры к объекту игрока
        /// </summary>
        private void Movement()
        {
            cameraInstance.position = GameData.PlayerTransformPtr.position - differenceOfPositions;
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public void Reboot()
        {
            cameraInstance.position = startPositionOfCamera;
        }
    }
}