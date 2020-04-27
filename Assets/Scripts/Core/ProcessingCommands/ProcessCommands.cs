using System.Collections.Generic;
using Common.Enums;
using Common.Events;

namespace Core.Processing
{
    /// <summary>
    /// Класс, реализующий обработку команд, введенных игроком
    /// </summary>
    public static class ProcessCommands
    {
        /// <summary>
        /// Метод получает в качестве параметра коллекцию команд и
        /// сигнализирует системе о возникновении событий ввода команд
        /// </summary>
        /// <param name="commands">Список команд игрока</param>
        public static void ProcessCommandsOfManagement(List<PlayerEvents> commands)
        {
            foreach (var command in commands)
            {
                EventController<PlayerEvents>.GameEvents[command]?.Invoke();
            }
        }
    }
}