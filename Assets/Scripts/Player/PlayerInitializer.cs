using UnityEngine;
using Common;
using Common.Interfaces;

namespace Player
{
    /// <summary>
    /// Класс, используемый для стартового создания объекта игрока
    /// </summary>
    public class PlayerInitializer : MonoBehaviour, IValidation
    {
        /// <summary>
        /// Ссылка на объект префаба игрока
        /// </summary>
        [SerializeField]
        private Transform playerPrefab = null;
        /// <summary>
        /// Вектор позиции, в которой нужно создать игрока
        /// </summary>
        [SerializeField]
        private Vector3 playerPosition = Vector3.zero;
        /// <summary>
        /// Вектор поворота, с которым нужно создать игрока
        /// </summary>
        [SerializeField]
        private Vector3 playerRotation = Vector3.zero;

        private void Start()
        {
            Validation();
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (!playerPrefab)
                Debug.LogError("Instance \"Transform of player prefab\" not set in " + this);
        }

        /// <summary>
        /// Метод, используемый для создания объекта игрока на
        /// сцене в момент начала игры
        /// </summary>
        public void Initialize()
        {
            Transform playerInstance = Instantiate(playerPrefab, playerPosition, Quaternion.Euler(playerRotation));
            GameData.PlayerTransformPtr = playerInstance;
        }
    }
}