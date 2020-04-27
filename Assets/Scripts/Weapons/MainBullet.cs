using UnityEngine;

namespace Weapon
{
    /// <summary>
    /// Класс, реализующий абстракцию конкретного снаряда
    /// </summary>
    public class MainBullet : AbstractBullets
    {
        /// <summary>
        /// Переопределенный метод для обработки перемещения снаряда
        /// </summary>
        public override void MoveBullet()
        {
            transform.Translate(new Vector3(0, 0, speedMove * Time.deltaTime));
        }

        /// <summary>
        /// Метод, используемый для обработки столкновения снаряда с
        /// монстром или с границей игрового поля
        /// </summary>
        /// <param name="otherCollider">Коллайдер, содержащий информацию
        /// об объекте, с которым произошло столкновение</param>
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.CompareTag("BorderOfZone") == true ||
                otherCollider.CompareTag("Monster") == true)
            {
                ReturnToPool();
            }
        }
    }
}