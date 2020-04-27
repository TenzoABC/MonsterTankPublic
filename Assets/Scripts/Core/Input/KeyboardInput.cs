using System.Collections.Generic;
using UnityEngine;
using Common.Enums;

namespace Core.Input
{
    /// <summary>
    /// Класс, реализующий абстракцию ввода
    /// пользовательских команд через клавиатуру
    /// </summary>
    public class KeyboardInput : AbstractInput
    {
        /// <summary>
        /// Словарь, содержащий команды в виде пар "событие игрока"-"массив клавишь", привязанных к событию
        /// </summary>
        private Dictionary<PlayerEvents, KeyCode[]> keysCommands = new Dictionary<PlayerEvents, KeyCode[]>();

        /// <summary>
        /// Инициализация словаря команд
        /// </summary>
        private void Start()
        {
            activeCommands = new List<PlayerEvents>();

            keysCommands.Add(PlayerEvents.MoveForward, new[] { KeyCode.UpArrow });
            keysCommands.Add(PlayerEvents.MoveBackward, new[] { KeyCode.DownArrow });
            keysCommands.Add(PlayerEvents.TurnRight, new[] { KeyCode.RightArrow });
            keysCommands.Add(PlayerEvents.TurnLeft, new[] { KeyCode.LeftArrow });

            keysCommands.Add(PlayerEvents.NextWeapon, new[] { KeyCode.W });
            keysCommands.Add(PlayerEvents.PrevWeapon, new[] { KeyCode.Q });
            keysCommands.Add(PlayerEvents.Shot, new[] { KeyCode.X });
        }

        /// <summary>
        /// Метод, для получения списка введенных пользователем команд
        /// </summary>
        /// <returns>Список команд игрока</returns>
        public override List<PlayerEvents> GetCommands()
        {
            activeCommands.Clear();

            foreach (var strWithCommand in keysCommands)
            {
                for (int i = 0; i < strWithCommand.Value.Length; i++)
                {
                    if (UnityEngine.Input.GetKey(strWithCommand.Value[i]))
                    {
                        activeCommands.Add(strWithCommand.Key);
                        break;
                    }
                }
            }
            return activeCommands;
        }
    }
}