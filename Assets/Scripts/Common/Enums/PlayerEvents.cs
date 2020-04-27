
namespace Common.Enums
{
    /// <summary>
    /// Перечисление всех событий для игрока
    /// </summary>
    public enum PlayerEvents
    {
        /// <summary>
        /// Двигаться вперед
        /// </summary>
        MoveForward,

        /// <summary>
        /// Двигаться назад
        /// </summary>
        MoveBackward,

        /// <summary>
        /// Поворачивать направо
        /// </summary>
        TurnRight,

        /// <summary>
        /// Поворачивать налево
        /// </summary>
        TurnLeft,

        /// <summary>
        /// Сменить оружие на следующее
        /// </summary>
        NextWeapon,

        /// <summary>
        /// Сменить оружие на предыдущее
        /// </summary>
        PrevWeapon,

        /// <summary>
        /// Выстрелить
        /// </summary>
        Shot,

        /// <summary>
        /// Смерть игрока
        /// </summary>
        Lose
    }
}