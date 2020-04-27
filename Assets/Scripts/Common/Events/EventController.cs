using System;

namespace Common.Events
{
    /// <summary>
    /// Статический класс для доступа к событиям различного типа
    /// </summary>
    /// <typeparam name="T">Тип перечисления для событий</typeparam>
    public static class EventController<T> where T : Enum, IConvertible
    {
        /// <summary>
        /// Свойство для доступа к событию через индексатор класса
        /// </summary>
        public static GameEvents<T> GameEvents { get; private set; }

        /// <summary>
        /// Статический конструктор для создания ссылки на класс событий
        /// </summary>
        static EventController()
        {
            GameEvents = new GameEvents<T>();
        }
    }
}