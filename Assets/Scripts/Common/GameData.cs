using UnityEngine;
using Common.Events;
using Common.Enums;
using Core;

namespace Common
{
    /// <summary>
    /// Статический класс для доступа к общим игровым данным 
    /// </summary>
    public static class GameData
    {
        /// <summary>
        /// Статический конструктор для создания подписки
        /// на событиие смерти игрока
        /// </summary>
        static GameData()
        {
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Поле, являющееся указателем на компонент Transform игрока
        /// </summary>
        private static Transform playerTransformPtr = null;

        /// <summary>
        /// Свойство для инициализации и доступа
        /// к компоненту Transform игрока
        /// </summary>
        public static Transform PlayerTransformPtr
        {
            get { return playerTransformPtr; }
            set
            {
                if (!playerTransformPtr)
                    playerTransformPtr = value;
            }
        }

        /// <summary>
        /// Поле, являющееся указателем на объект,
        /// содержащий информацию о сцене
        /// </summary>
        private static SceneData sceneDataPtr = null;

        /// <summary>
        /// Свойство для инициализации и доступа к объекту,
        /// содержащему информацию о сцене
        /// </summary>
        public static SceneData SceneDataPtr
        {
            get { return sceneDataPtr; }
            set
            {
                if (!sceneDataPtr)
                    sceneDataPtr = value;
            }
        }

        /// <summary>
        /// Метод для перезагрузки класса
        /// </summary>
        private static void Reboot()
        {
            playerTransformPtr = null;
        }
    }
}