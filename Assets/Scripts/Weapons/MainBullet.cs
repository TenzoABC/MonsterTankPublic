using Common.Enums;
using Common.Events;
using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// Класс, реализующий абстракцию конкретного снаряда
    /// </summary>
    public class MainBullet : AbstractBullets
    {
        private void Start()
        {
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Вызов метода перемещения снаряда в каждом кадре по Unity-событию
        /// </summary>
        private void Update()
        {
            MoveBullet();
        }

        /// <summary>
        /// Переопределенный метод для обработки перемещения снаряда
        /// </summary>
        public override void MoveBullet()
        {
            transform.Translate(new Vector3(0, 0, speedMove * Time.deltaTime));
        }

        /// <summary>
        /// Метод, используемый для обработки столкновения снаряда с
        /// монстром или с границей игрового поля
        /// </summary>
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.CompareTag("BorderOfZone") == true ||
                otherCollider.CompareTag("Monster") == true)
            {
                ReturnToPool();
            }
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public override void Reboot()
        {
            if (isActiveAndEnabled)
            {
                ReturnToPool();
            }
        }
    }
}