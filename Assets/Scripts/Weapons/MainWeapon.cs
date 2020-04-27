using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// Класс, реализующий абстракцию конкретного оружия.
    /// </summary>
    public class MainWeapon : AbstractWeapon
    {
        private void Start()
        {
            Validation();
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public override void Validation()
        {
            if (!bullet)
                Debug.LogError("Instance \"AbstractBullets\" not set in " + this);
            if (!bulletSpawnPoint)
                Debug.LogError("Instance \"Transform\" for bullet spawn point not set in " + this);
        }

        private void Update()
        {
            CheckTimeoutOfShots();
        }

        /// <summary>
        /// Метод, используемый для обработки задержки между выстрелами
        /// </summary>
        protected override void CheckTimeoutOfShots()
        {
            if (timeToNextShot > 0)
                timeToNextShot -= Time.deltaTime;
        }

        /// <summary>
        /// Переопределенный метод, используемый для инициализации выстрела оружия
        /// </summary>
        /// <returns>Экземпляр снаряда, создаваемого при выстреле</returns>
        public override AbstractBullets Shot()
        {
            timeToNextShot = timeoutBetweenShots;
            AbstractBullets bulletInstance = (AbstractBullets)bullet.GetPoolInstance();

            bulletInstance.transform.position = bulletSpawnPoint.position;
            bulletInstance.transform.rotation = bulletSpawnPoint.rotation;

            return bulletInstance;
        }

        /// <summary>
        /// Метод, используемый для проверки возможности выстрела оружия
        /// </summary>
        /// <returns>Переменная логического типа, обозначающая возможность выстрела оружия</returns>
        public override bool CheckIfCanShot()
        {
            if (timeToNextShot <= 0)
                return true;
            else
                return false;
        }
    }
}