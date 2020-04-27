using System;
using System.Globalization;
using UnityEngine.Events;

namespace Common.Events
{
    /// <summary>
    /// Класс хранит массив событий заданного типа
    /// </summary>
    /// <typeparam name="T">Тип перечисления для событий</typeparam>
    public class GameEvents<T> where T : Enum, IConvertible
    {
        private UnityAction[] actions;

        /// <summary>
        /// Конструктор для создания массива событий
        /// </summary>
        public GameEvents()
        {
            int countOfEvents = Enum.GetNames(typeof(T)).Length;
            actions = new UnityAction[countOfEvents];
        }

        /// <summary>
        /// Индексатор для получения ссылки на событие по индексу события,
        /// или добавления события в массив
        /// </summary>
        /// <param name="id">Индекс в массиве событий указанного типа</param>
        /// <returns>Ссылка на событие по индексу</returns>
        public UnityAction this[T id]
        {
            get { return actions[id.ToInt32(NumberFormatInfo.CurrentInfo)]; }

            set { actions[id.ToInt32(NumberFormatInfo.CurrentInfo)] = value; }
        }
    }
}