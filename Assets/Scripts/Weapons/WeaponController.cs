﻿using System.Collections.Generic;
using UnityEngine;
using Common.Interfaces;
using Common.Enums;
using Common.Events;

namespace Weapon
{
    /// <summary>
    /// Класс используется для управления оружием
    /// </summary>
    public class WeaponController : MonoBehaviour, IValidation, IClearable
    {
        /// <summary>
        /// Ссылка на объект, являющийся родителем для создаваемого
        /// оружия и в позиции которого необходимо создавать оружие
        /// </summary>
        [SerializeField]
        private Transform weaponRoot = null;

        /// <summary>
        /// Свойство, используемое для организации доступа к родительскому
        /// объекту оружия из других классов
        /// </summary>
        public Transform WeaponRoot => weaponRoot;

        /// <summary>
        /// Идентификатор текущего оружия
        /// </summary>
        private int idCurrentWeapon = 0;

        /// <summary>
        /// Обобщенная коллекция, используемая для хранения всех созданных экземпляров оружия
        /// </summary>
        public List<AbstractWeapon> ArrayWeapons { get; private set; } = new List<AbstractWeapon>();

        /// <summary>
        /// Задержка между сменой оружия
        /// </summary>
        [SerializeField]
        private float timeoutBetweenChange = 0.3f;

        /// <summary>
        /// Текущее время задержки между сменой оружия
        /// </summary>
        private float timeToNextChange = 0;

        /// <summary>
        /// Запуск валидации полей класса и подписка на события
        /// </summary>
        void Start()
        {
            Validation();

            EventController<PlayerEvents>.GameEvents[PlayerEvents.NextWeapon] += NextWeapon;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.PrevWeapon] += PrevWeapon;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Shot] += Shot;

            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (!weaponRoot)
                Debug.LogError("Instance \"Point position of weapon\" not set in " + this);
        }
        private void Update()
        {
            CheckTimeoutOfChangeWeapon();
        }

        /// <summary>
        /// Метод обработки задержки между сменами оружия
        /// </summary>
        private void CheckTimeoutOfChangeWeapon()
        {
            if (timeToNextChange > 0)
                timeToNextChange -= Time.deltaTime;
        }

        /// <summary>
        /// Метод, используемый для добавления нового оружия в коллекцию оружия
        /// </summary>
        /// <param name="weapon">Экземпляр оружия</param>
        public void AddWeapon(AbstractWeapon weapon)
        {
            ArrayWeapons.Add(weapon);
        }

        /// <summary>
        /// Метод смены оружия на следующее в коллекции оружия
        /// </summary>
        private void NextWeapon()
        {
            if (timeToNextChange <= 0)
            {
                ArrayWeapons[idCurrentWeapon].gameObject.SetActive(false);
                idCurrentWeapon++;
                CheckBordersWeaponsArray();
                ArrayWeapons[idCurrentWeapon].gameObject.SetActive(true);
                timeToNextChange = timeoutBetweenChange;
            }
        }

        /// <summary>
        /// Метод смены оружия на предыдущее в коллекции оружия
        /// </summary>
        private void PrevWeapon()
        {
            if (timeToNextChange <= 0)
            {
                ArrayWeapons[idCurrentWeapon].gameObject.SetActive(false);
                idCurrentWeapon--;
                CheckBordersWeaponsArray();
                ArrayWeapons[idCurrentWeapon].gameObject.SetActive(true);
                timeToNextChange = timeoutBetweenChange;
            }
        }

        /// <summary>
        /// Метод, используемый при смене оружия для
        /// проверки выхода за границы коллекции хранимого оружия
        /// </summary>
        private void CheckBordersWeaponsArray()
        {
            if (idCurrentWeapon >= ArrayWeapons.Count)
                idCurrentWeapon = 0;
            else if (idCurrentWeapon < 0)
                idCurrentWeapon = ArrayWeapons.Count - 1;
        }

        /// <summary>
        /// Метод, используемый для запуска выстрела для активного оружия
        /// </summary>
        private void Shot()
        {
            ArrayWeapons[idCurrentWeapon].Shot();
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public void Reboot()
        {
            EventController<PlayerEvents>.GameEvents[PlayerEvents.NextWeapon] -= NextWeapon;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.PrevWeapon] -= PrevWeapon;
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Shot] -= Shot;
        }
    }
}