using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Common.Interfaces;
using Common.Events;
using Common.Enums;
using Monster;

namespace Player
{
    /// <summary>
    /// Класс, используемый для обработки получения урона игроком
    /// </summary>
    public class PlayerDamageController : MonoBehaviour, IValidation
    {
        /// <summary>
        /// Ссылка на индикатор здоровья игрока
        /// </summary>
        [SerializeField]
        private Slider healthBar = null;
        /// <summary>
        /// Текущее количество здоровья игрока
        /// </summary>
        [SerializeField]
        private float health = 100;
        /// <summary>
        /// Текущее количество защиты игрока
        /// </summary>
        [SerializeField, Range(0, 1)]
        private float defence = 0;
        /// <summary>
        /// Время, используемое для получения задержки между моментами получения урона игроком
        /// </summary>
        [SerializeField]
        private float timeoutBetweenDamages = 0.3f;
        /// <summary>
        /// Текущее время задержки для получения урона
        /// </summary>
        private float timeToNextDamages = 0;

        /// <summary>
        /// Инициализация объекта класса
        /// </summary>
        private void Start()
        {
            Validation();
            healthBar.maxValue = health;
            healthBar.value = health;
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (!healthBar)
                Debug.LogError("Instance \"Slider\" not set in " + this);
        }

        private void Update()
        {
            CheckTimeoutOfDamage();
        }

        /// <summary>
        /// Метод, используемый для обработки времени задержки между моментами получения урона
        /// </summary>
        private void CheckTimeoutOfDamage()
        {
            if (timeToNextDamages > 0)
                timeToNextDamages -= Time.deltaTime;
        }

        /// <summary>
        /// Метод, срабатывающий по Unity-событию о столкновении игрока с другим объектом.
        /// Метод используется для обработки столкновения игрока с монстром
        /// </summary>
        /// <param name="otherCollider">Коллайдер, содержащий информацию
        /// об объекте, с которым произошло столкновение</param>
        private void OnTriggerStay(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Monster"))
            {
                if (CheckGetDamage())
                {
                    var monster = otherCollider.GetComponent<AbstractMonster>();
                    if (monster)
                        GetDamage(monster);
                }
            }
        }

        /// <summary>
        /// Метод, используемый для определения возможности получения урона игроком.
        /// В случае, если получение урона возможно, то запускает сопрограмму для
        /// ожидания обработки столкновения со всеми монстрами в кадре
        /// </summary>
        /// <returns>Переменная логического типа, отражающая
        /// возможность получения урона игроком</returns>
        private bool CheckGetDamage()
        {
            if (timeToNextDamages <= 0)
            {
                StartCoroutine(WaitAllTriggers());
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Сопрограмма (итератор) для ожидания обработки столкновения со
        /// всеми монстрами, с которыми произошло столкновение в кадре
        /// </summary>
        /// <returns>Выполнение итератора продолжается события LateUpdate и рендеринга сцены</returns>
        private IEnumerator WaitAllTriggers()
        {
            yield return new WaitForEndOfFrame();
            timeToNextDamages = timeoutBetweenDamages;
        }

        /// <summary>
        /// Метод, используемый для обработки получения урона
        /// </summary>
        /// <param name="monster">Экземпляр монстра, который наносит урон</param>
        private void GetDamage(AbstractMonster monster)
        {
            health = CalculatingHealth(health, defence, monster.Damage);

            UpdateHealth();

            if (health <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Died");
                EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose].Invoke();
            }
        }

        /// <summary>
        /// Метод, используемый для расчета количества очков здоровья игрока
        /// после столкновения с монстром
        /// </summary>
        /// <param name="health">Количество здоровья, имеющееся у игрока до получения урона</param>
        /// <param name="defence">Текущая защита игрока</param>
        /// <param name="damage">Количество урона, полученного игроком</param>
        /// <returns>Количество здоровья, оставшееся после получения урона</returns>
        private float CalculatingHealth(float health, float defence, float damage)
        {
            return health - (damage * (1 - defence));
        }

        /// <summary>
        /// Метод, используемый для обновления очков здоровья на индикаторе здоровья
        /// </summary>
        private void UpdateHealth()
        {
            healthBar.value = health;
        }
    }
}