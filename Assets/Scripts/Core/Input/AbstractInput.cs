﻿using System.Collections.Generic;
using UnityEngine;
using Common.Enums;

namespace Core.Input
{
    /// <summary>
    /// Абстрактный класс, описывающий абстракцию
    /// ввода в приложение команд (событий) игрока
    /// </summary>
    public abstract class AbstractInput : MonoBehaviour
    {
        /// <summary>
        /// Список команд, полученных от игрока
        /// </summary>
        protected List<PlayerEvents> activeCommands;

        /// <summary>
        /// Абстрактный метод для получения списка введенных игроком команд
        /// </summary>
        /// <returns>Список команд игрока</returns>
        public abstract List<PlayerEvents> GetCommands();
    }
}