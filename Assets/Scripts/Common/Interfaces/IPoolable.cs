using Core;

namespace Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает абстракцию класса,
    /// для которого может быть создан пул объектов
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Свойство, используемое для инициализации и внешнего доступа
        /// к полю, содержащему ссылку на экземпляр пула объектов
        /// </summary>
        PoolOfObjects PoolInstance { get; set; }
        /// <summary>
        /// Метод используется для получения экземпляра класса из пула объектов
        /// </summary>
        /// <returns>Экземпляр объекта пула</returns>
        IPoolable GetPoolInstance();
        /// <summary>
        /// Метод используется для возврата объекта пула
        /// назад в пул объектов
        /// </summary>
        void ReturnToPool();
    }
}