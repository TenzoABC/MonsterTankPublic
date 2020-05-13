using UnityEngine;
using Weapon;
using Common.Events;
using Common.Enums;

namespace Monster
{
    /// <summary>
    /// Класс, представляющий конкретную реализацию абстракции монстра
    /// </summary>
    public class SimpleMonster : AbstractMonster
    {
        /// <summary>
        /// Поле, используемое для блокировки перемещения
        /// монстра и перевода его в состояние ожидания
        /// </summary>
        private bool isIdle = false;

        /// <summary>
        /// Инициализация объекта при включении
        /// </summary>
        private void OnEnable()
        {
            isIdle = false;

            speedMove = speedMoveOfInstace;
            health = healthOfInstace;
            defence = defenceOfInstace;
            damage = damageOfInstace;
        }

        /// <summary>
        /// Метод используется для перемещения монстра к точке на сцене
        /// </summary>
        /// <param name="pointOfMove">Вектор позиции, к которой необходимо переместиться</param>
        public override void MoveToPoint(Vector3 pointOfMove)
        {
            if (!isIdle)
            {
                transform.LookAt(pointOfMove);
                transform.Translate(new Vector3(0, 0, speedMove * Time.deltaTime));
            }
        }

        /// <summary>
        /// Метод, используемый для обработки столкновения
        /// монстра со снарядом или игроком
        /// </summary>
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Bullet"))
            {
                var bullet = otherCollider.GetComponent<AbstractBullets>();
                if (bullet)
                    GetDamage(bullet);
            }
            if (otherCollider.CompareTag("Player"))
                Fight();
        }

        /// <summary>
        /// Метод используется для получения урона монстром
        /// </summary>
        /// <param name="bullet">Ссылка на экземпляр снаряда</param>
        protected override void GetDamage(AbstractBullets bullet)
        {
            health = CalculatingHealth(bullet.Damage);

            if (health <= 0)
            {
                ReturnToPool();
                EventController<MonsterEvents>.GameEvents[MonsterEvents.Died].Invoke();
            }
        }

        /// <summary>
        /// Метод используется для перевода монстра в режим боя
        /// </summary>
        protected override void Fight()
        {
            isIdle = true;
        }

        /// <summary>
        /// Метод, используемый перевода монстра из состояния
        /// ожидания в состояние перемещения до игрока
        /// </summary>
        private void OnTriggerExit(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
                isIdle = false;
        }

        /// <summary>
        /// Метод вычисляет количество здоровья монстра, оставшееся после нанесения ему урона
        /// </summary>
        /// <param name="damage">Количество урона, нанесенного монстру</param>
        /// <returns>Количество очков здоровья, оставшееся у 
        /// монстра после получения урона</returns>
        protected override float CalculatingHealth(float damage)
        {
            float multiplierOfDamage = 1 - defence;
            return (health - (damage * multiplierOfDamage));
        }
    }
}