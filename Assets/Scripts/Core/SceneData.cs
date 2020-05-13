using UnityEngine;
using Common;

namespace Core
{
    /// <summary>
    /// Класс, содержащий общую информацию о сцене
    /// </summary>
    public class SceneData : MonoBehaviour
    {
        /// <summary>
        /// Поле, отражающее верхнюю границу боевого поля сцены
        /// </summary>
        [SerializeField]
        private float topBorder = 0;

        /// <summary>
        /// Свойство, возвращающее координату верхней границы
        /// боевого поля сцены
        /// </summary>
        public float TopBorder { get { return topBorder; } }

        /// <summary>
        /// Поле, отражающее нижнюю границу боевого поля сцены
        /// </summary>
        [SerializeField]
        private float downBorder = 0;

        /// <summary>
        /// Свойство, возвращающее координату нижней границы
        /// боевого поля сцены
        /// </summary>
        public float DownBorder { get { return downBorder; } }

        /// <summary>
        /// Поле, отражающее правую границу боевого поля сцены
        /// </summary>
        [SerializeField]
        private float rightBorder = 0;

        /// <summary>
        /// Свойство, возвращающее координату правой границы
        /// боевого поля сцены
        /// </summary>
        public float RightBorder { get { return rightBorder; } }

        /// <summary>
        /// Поле, отражающее левую границу боевого поля сцены
        /// </summary>
        [SerializeField]
        private float leftBorder = 0;

        /// <summary>
        /// Свойство, возвращающее координату левой границы
        /// боевого поля сцены
        /// </summary>
        public float LeftBorder { get { return leftBorder; } }

        /// <summary>
        /// Метод, передающий указатель на объект класса
        /// </summary>
        private void OnEnable()
        {
            GameData.SceneDataPtr = this;
        }
    }
}