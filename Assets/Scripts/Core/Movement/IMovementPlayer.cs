
namespace Core.Movement
{
    /// <summary>
    /// Интерфейс для описания абстракции перемещения игрока
    /// </summary>
    public interface IMovementPlayer
    {
        /// <summary>
        /// Метод, используемый для реализации движения вперед
        /// </summary>
        void MoveForward();

        /// <summary>
        /// Метод, используемый для реализации движения назад
        /// </summary>
        void MoveBackward();

        /// <summary>
        /// Метод, используемый для реализации поворота влево
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Метод, используемый для реализации поворота вправо
        /// </summary>
        void TurnRight();
    }
}