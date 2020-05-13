using UnityEngine;
using Common.Interfaces;

namespace Weapon
{
    /// <summary>
    /// Класс, используемый для описания абстракции оружия игрока
    /// </summary>
    public abstract class AbstractWeapon : MonoBehaviour, IValidation
    {
        /// <summary>
        /// Поле, отражающее временную задержку между 
        /// выстрелами данного экземпляра класса
        /// </summary>
        [SerializeField]
        protected float timeoutBetweenShots = 0.08f;

        /// <summary>
        /// Текущая временная задержка между выстрелами
        /// </summary>
        protected float timeToNextShot = 0;

        /// <summary>
        /// Ссылка на префаб снаряда, создаваемого при выстреле оружия
        /// </summary>
        [SerializeField]
        protected AbstractBullets bullet = null;

        /// <summary>
        /// Точка, в которой происходит создание объекта снаряда при выстреле
        /// </summary>
        [SerializeField]
        protected Transform bulletSpawnPoint = null;

        /// <summary>
        /// Абстрактный метод валидации экземпляра класса
        /// </summary>
        public abstract void Validation();

        /// <summary>
        /// Абстрактный метод обработки задержки между выстрелами оружия
        /// </summary>
        protected abstract void CheckTimeoutOfShots();

        /// <summary>
        /// Абстрактный метод инициализации выстрела оружия
        /// </summary>
        /// <returns>Ссылка на объект снаряда</returns>
        public abstract AbstractBullets Shot();

        /// <summary>
        /// Абстрактный метод определения возможности выстрела
        /// </summary>
        /// <returns>Логическая переменная, означающая возможность выстрела оружия</returns>
        public abstract bool CheckIfCanShot();
    }
}