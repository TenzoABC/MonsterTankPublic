using UnityEngine;
using Common;
using Common.Interfaces;
using Common.Events;
using Common.Enums;

namespace Core.Movement
{
    /// <summary>
    /// Класс, используемый для реализации движения игрока по сцене
    /// </summary>
    public class MovementHandler : MonoBehaviour, IMovementPlayer, IClearable
    {
        /// <summary>
        /// Скорость движения игрока
        /// </summary>
        [SerializeField]
        private float speedMove = 0.5f;
        /// <summary>
        /// Скорость поворота игрока
        /// </summary>
        [SerializeField]
        private float speedTurn = 2;

        private void Start()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Метод, реализующий подписку на события движения
        /// игрока и на событие смерти игрока
        /// </summary>
        private void SubscribeEvents()
        {
            EventController<PlayerEvents>.GameEvents[PlayerEvents.MoveForward] += MoveForward;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.MoveBackward] += MoveBackward;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.TurnRight] += TurnRight;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.TurnLeft] += TurnLeft;

            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Метод, используемый для реализации движения вперед
        /// </summary>
        public void MoveForward()
        {
            transform.Translate(new Vector3(0, 0, speedMove * Time.deltaTime));
            ClampInScene();
        }

        /// <summary>
        /// Метод, используемый для реализации движения назад
        /// </summary>
        public void MoveBackward()
        {
            transform.Translate(new Vector3(0, 0, -speedMove * Time.deltaTime));
            ClampInScene();
        }

        /// <summary>
        /// Метод, используемый для реализации поворота влево
        /// </summary>
        public void TurnLeft()
        {
            transform.Rotate(Vector3.up, -speedTurn * Time.deltaTime);
        }

        /// <summary>
        /// Метод, используемый для реализации поворота вправо
        /// </summary>
        public void TurnRight()
        {
            transform.Rotate(Vector3.up, speedTurn * Time.deltaTime);
        }

        /// <summary>
        /// Метод, осуществляющий проверку игрока при движении
        /// на выход за границы игрового поля
        /// </summary>
        private void ClampInScene()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, GameData.SceneDataPtr.LeftBorder, GameData.SceneDataPtr.RightBorder),
                transform.position.y,
                Mathf.Clamp(transform.position.z, GameData.SceneDataPtr.DownBorder, GameData.SceneDataPtr.TopBorder)
                );
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public void Reboot()
        {
            EventController<PlayerEvents>.GameEvents[PlayerEvents.MoveForward] -= MoveForward;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.MoveBackward] -= MoveBackward;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.TurnRight] -= TurnRight;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.TurnLeft] -= TurnLeft;
        }
    }
}