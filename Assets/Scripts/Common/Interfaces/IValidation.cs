﻿namespace Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает абстракцию проверки
    /// корректности инициализации полей в объекте класса
    /// </summary>
    interface IValidation
    {
        /// <summary>
        /// Метод для проверки объекта класса
        /// </summary>
        void Validation();
    }
}