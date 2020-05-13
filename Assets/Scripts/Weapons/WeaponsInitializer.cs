using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;
using Common.Interfaces;
using Common.Enums;
using Common.Events;

namespace Weapon
{
    /// <summary>
    /// Класс, используемый для создания и инициализации оружия
    /// в момент запуска игры
    /// </summary>
    public class WeaponsInitializer : MonoBehaviour, IValidation, IClearable
    {
        /// <summary>
        /// Коллекция, хранящая префабы всех экземпляров оружия
        /// создаваемого при запуске игры
        /// </summary>
        [SerializeField]
        private List<AbstractWeapon> weaponPrefabs;

        /// <summary>
        /// Ссылка на объект контроллера оружия
        /// </summary>
        private WeaponController mainWeaponController = null;

        /// <summary>
        /// Подписка на событие перезагрузки игры
        /// </summary>
        private void Start()
        {
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Инициализация объекта класса
        /// </summary>
        public void Initialize()
        {
            mainWeaponController = GameData.PlayerTransformPtr.GetComponent<WeaponController>();
            Validation();
            CreateWeapons();
            ActivateWeapon();
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (!mainWeaponController)
                Debug.LogError("Instance \"MainWeaponController\" not found in player object. " + this);
        }

        /// <summary>
        /// Метод, используемый для создания оружия
        /// </summary>
        private void CreateWeapons()
        {
            foreach (var prefabWeapon in weaponPrefabs)
            {
                var weapon = Instantiate(prefabWeapon, mainWeaponController.WeaponRoot);
                mainWeaponController.AddWeapon(weapon);
                weapon.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Активация первого оружия, представленного в коллекции
        /// созданного оружия
        /// </summary>
        private void ActivateWeapon()
        {
            var activeWeapon = mainWeaponController.ArrayWeapons.FirstOrDefault();
            if (activeWeapon)
                activeWeapon.gameObject.SetActive(true);
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public void Reboot()
        {
            mainWeaponController = null;
        }
    }
}