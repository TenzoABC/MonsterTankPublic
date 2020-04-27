using UnityEngine;
using Common.Interfaces;
using Core.Input;
using Core.Processing;

namespace Core
{
    /// <summary>
    /// Класс, используемый для управления получением и обработкой пользовательских команд
    /// </summary>
    public class TankManagement : MonoBehaviour, IValidation
    {
        /// <summary>
        /// Ссылка на объект получения команд пользовательского ввода 
        /// </summary>
        [SerializeField]
        private AbstractInput processInput = null;

        private void Start()
        {
            Validation();
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (!processInput)
                Debug.LogError("Instance \"AbstractInput\" not set in " + this);
        }

        private void Update()
        {
            if (GameController.GameIsStarted)
                MovementControl();
        }

        /// <summary>
        /// Получение и обработка пользовательского ввода
        /// </summary>
        private void MovementControl()
        {
            ProcessCommands.ProcessCommandsOfManagement(processInput.GetCommands());
        }
    }
}