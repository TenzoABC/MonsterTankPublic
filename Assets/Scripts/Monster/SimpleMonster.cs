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
        /// Метод, используемый для перемещения монстра
        /// </summary>
        /// <param name="pointOfMove">Точка, к которой необходимо переместить монстра</param>
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
        /// <param name="otherCollider">Коллайдер, содержащий информацию
        /// об объекте, с которым произошло столкновение</param>
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Bullet"))
            {
                var bullet = otherCollider.GetComponent<AbstractBullets>();
                if (bullet)
                    GetDamage(bullet);
            }
            if (otherCollider.CompareTag("Player"))
                isIdle = true;
        }

        /// <summary>
        /// Метод, используемый для обработки урона, полученного от столкновения со снарядом
        /// </summary>
        /// <param name="bullet">Объект снаряда, с которым
        /// произощло столкновение монстра</param>
        protected override void GetDamage(AbstractBullets bullet)
        {
            health = CalculatingHealth(health, defence, bullet.Damage);

            if (health <= 0)
            {
                ReturnToPool();
                EventController<MonsterEvents>.GameEvents[MonsterEvents.Died].Invoke();
            }
        }

        /// <summary>
        /// Метод, используемый перевода монстра из состояния
        /// ожидания в состояние перемещения до игрока
        /// </summary>
        /// <param name="otherCollider">Коллайдер, столкновение с которым закончилось</param>
        private void OnTriggerExit(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
                isIdle = false;
        }

        /// <summary>
        /// Метод, используемый для расчета количества очков здоровья монстра
        /// после попадения в монстра снаряда
        /// </summary>
        /// <param name="health">Количество здоровья, имеющееся у монстра до получения урона</param>
        /// <param name="defence">Текущая защита монстра</param>
        /// <param name="damage">Количество урона, полученного монстром</param>
        /// <returns>Количество здоровья, оставшееся после получения урона</returns>
        protected override float CalculatingHealth(float health, float defence, float damage)
        {
            return health - (damage * (1 - defence));
        }
    }
}