using UnityEngine;
using Weapon;
using Core;
using Common.Interfaces;

namespace Monster
{
    /// <summary>
    /// Абстрактный класс, описывающий абстракцию игрового монстра
    /// </summary>
    public abstract class AbstractMonster : MonoBehaviour, IPoolable
    {
        /// <summary>
        /// Скорость перемещения монстра при его инициализации
        /// </summary>
        [SerializeField]
        protected float speedMoveOfInstace = 1;

        /// <summary>
        /// Количество здоровья монстра при его инициализации
        /// </summary>
        [SerializeField]
        protected float healthOfInstace = 1;

        /// <summary>
        /// Защита монстра при его инициализации
        /// </summary>
        [SerializeField, Range(0, 1)]
        protected float defenceOfInstace = 0;

        /// <summary>
        /// Урон монстра при его инициализации
        /// </summary>
        [SerializeField]
        protected float damageOfInstace = 1;

        /// <summary>
        /// Текущая скорость монстра
        /// </summary>
        protected float speedMove = 0;

        /// <summary>
        /// Текущие очки здоровья монстра
        /// </summary>
        protected float health = 0;

        /// <summary>
        /// Текущая защита монстра
        /// </summary>
        protected float defence = 0;

        /// <summary>
        /// Текущие очки урона монстра
        /// </summary>
        protected float damage = 0;

        /// <summary>
        /// Свойство для получения из кругих классов
        /// текущих очков урона монстра 
        /// </summary>
        public float Damage => damage;

        /// <summary>
        /// Поле, содержащее ссылку на объект пула объектов
        /// </summary>
        protected PoolOfObjects poolInstance = null;

        /// <summary>
        /// Свойство, используемое для инициализации и доступа
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
        /// Метод используется для получения экземпляра монстра из пула объектов
        /// </summary>
        /// <returns>Экземпляр объекта из пула</returns>
        public IPoolable GetPoolInstance()
        {
            if (!poolInstance)
                poolInstance = PoolOfObjects.GetPool(this);

            return poolInstance.GetObject<AbstractMonster>();
        }

        /// <summary>
        /// Метод используется для возврата объекта
        /// назад в пул объектов, либо удаление этого объекта,
        /// если он не принадлежит пулу
        /// </summary>
        public void ReturnToPool()
        {
            if (PoolInstance)
                PoolInstance.AddObject(this);
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// Метод используется для перемещения монстра к точке на сцене
        /// </summary>
        /// <param name="pointOfMove">Вектор позиции, к которой необходимо переместиться</param>
        public abstract void MoveToPoint(Vector3 pointOfMove);

        /// <summary>
        /// Метод используется для получения урона монстром
        /// </summary>
        /// <param name="bullet">Ссылка на экземпляр снаряда</param>
        protected abstract void GetDamage(AbstractBullets bullet);

        /// <summary>
        /// Метод используется для перевода монстра в режим боя
        /// </summary>
        protected abstract void Fight();

        /// <summary>
        /// Метод вычисляет количество здоровья монстра, оставшееся после нанесения ему урона
        /// </summary>
        /// <param name="damage">Количество урона, нанесенного монстру</param>
        /// <returns>Количество очков здоровья, оставшееся у 
        /// монстра после получения урона</returns>
        protected abstract float CalculatingHealth(float damage);
    }
}