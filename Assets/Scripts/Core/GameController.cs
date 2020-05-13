using UnityEngine;
using Common.Interfaces;
using Common.Enums;
using Common.Events;
using Player;
using Weapon;
using Monster;

namespace Core
{
    /// <summary>
    /// Класс, используемый для запуска инициализации объектов при запуске игры
    /// </summary>
    public class GameController : MonoBehaviour, IValidation, IClearable
    {
        /// <summary>
        /// Свойство, отражающее факт запуска игры
        /// </summary>
        public static bool GameIsStarted { get; private set; }

        /// <summary>
        /// Ссылка на объект-инициализатор игрока
        /// </summary>
        [SerializeField]
        private PlayerInitializer playerInitializer = null;

        /// <summary>
        /// Ссылка на объект-инициализатор оружия игрока
        /// </summary>
        [SerializeField]
        private WeaponsInitializer mainWeaponsInitializer = null;

        /// <summary>
        /// Ссылка на объект-инициализатор монстров
        /// </summary>
        [SerializeField]
        private MonstersInitializer monstersInitializer = null;

        /// <summary>
        /// Ссылка на главное меню игры
        /// </summary>
        [SerializeField]
        private Canvas mainMenu = null;

        void Start()
        {
            Validation();
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (!playerInitializer)
                Debug.LogError("Instance \"PlayerInitializer\" not set in " + this);
            if (!mainWeaponsInitializer)
                Debug.LogError("Instance \"MainWeaponsInitializer\" not set in " + this);
            if (!monstersInitializer)
                Debug.LogError("Instance \"MonstersInitializer\" not set in " + this);
            if (!mainMenu)
                Debug.LogError("Instance \"Canvas\" of main menu not set in " + this);
        }

        /// <summary>
        /// Метод, используемый для запуска игры
        /// </summary>
        public void StartGame()
        {
            if (GameIsStarted)
            {
                Debug.LogWarning("Game is already started!");
                return;
            }

            //Отключение меню и изменение свойства статуса игры
            mainMenu.gameObject.SetActive(false);
            GameIsStarted = true;

            //Инициализация объектов
            playerInitializer.Initialize();
            mainWeaponsInitializer.Initialize();
            monstersInitializer.Initialize();
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public void Reboot()
        {
            mainMenu.gameObject.SetActive(true);
            GameIsStarted = false;
        }
    }
}