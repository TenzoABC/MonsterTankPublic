using UnityEngine;
using Core;
using Common.Interfaces;

namespace Weapon
{
    /// <summary>
    /// Абстрактный класс, описывающий абстракцию боевого снаряда игрока
    /// </summary>
    public abstract class AbstractBullets : MonoBehaviour, IPoolable
    {
        /// <summary>
        /// Скорость перемещения снаряда при его перемещении
        /// </summary>
        [SerializeField]
        protected float speedMove = 1;
        /// <summary>
        /// Количество урона, наносимого снарядом
        /// </summary>
        [SerializeField]
        protected float damage = 1;
        /// <summary>
        /// Свойство, используемое для получения из других классов
        /// количества урона, наносимого снарядом
        /// </summary>
        public float Damage => damage;

        /// <summary>
        /// Поле, содержащее ссылку на объект пула объектов
        /// </summary>
        protected PoolOfObjects poolInstance = null;
        /// <summary>
        /// Свойство, используемое для инициализации и внешнего доступа
        /// к полю, содержащему ссылку на экземпляр пула объектов
        /// </summary>
        public PoolOfObjects PoolInstance
        {
            get { return poolInstance; }
            set
            {
                if (!poolInstance)
                    poolInstance = value;
            }
        }

        /// <summary>
        /// Метод используется для получения экземпляра снаряда из пула объектов
        /// </summary>
        /// <returns>Экземпляр объекта пула</returns>
        public IPoolable GetPoolInstance()
        {
            if (!poolInstance)
                poolInstance = PoolOfObjects.GetPool(this);

            return poolInstance.GetObject<AbstractBullets>();
        }

        /// <summary>
        /// Метод используется для возврата объекта пула
        /// назад в пул объектов, либо удаление этого объекта,
        /// если он не принадлежит пулу
        /// </summary>
        public void ReturnToPool()
        {
            if (poolInstance)
                poolInstance.AddObject(this);
            else
                Destroy(gameObject);
        }
        
        /// <summary>
        /// Метод используется для перемещения снаряда
        /// </summary>
        public abstract void MoveBullet();
    }
}