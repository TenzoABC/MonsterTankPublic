using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;
using Common.Interfaces;
using Common.Enums;
using Common.Events;
using Core;

namespace Monster
{
    /// <summary>
    /// Класс используется для управления перемещением монстров
    /// и хранением уникальных монстров в массиве
    /// </summary>
    public class MonstersController : MonoBehaviour, IClearable
    {
        /// <summary>
        /// Коллекция, содержащая экземпляры всех созданных монстров
        /// </summary>
        private List<AbstractMonster> monstersInstances = new List<AbstractMonster>();

        private void Start()
        {
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Вызов метода перемещения монстра в каждом кадре по Unity-событию
        /// </summary>
        private void Update()
        {
            if (GameController.GameIsStarted)
                MoveMonsters();
        }

        /// <summary>
        /// Метод, используемый для возврата количества живых монстров
        /// </summary>
        /// <returns>Количество активных и включенных монстров</returns>
        public int CheckAliveMonsters()
        {
            var countAliveMonsters = monstersInstances.Count(monster => monster.isActiveAndEnabled);
            return countAliveMonsters;
        }

        /// <summary>
        /// Метод, используемый для добавления нового
        /// монстра в коллекцию монстров
        /// </summary>
        /// <param name="monsterInstance">Экземпляр монстра</param>
        public void AddNewMonster(AbstractMonster monsterInstance)
        {
            if (!monstersInstances.Contains(monsterInstance))
                monstersInstances.Add(monsterInstance);
        }

        /// <summary>
        /// Метод для перемещения всех живых монстров к игроку
        /// </summary>
        private void MoveMonsters()
        {
            if (monstersInstances.Count <= 0)
                return;

            Vector3 playerPosition = GameData.PlayerTransformPtr.position;
            foreach (var monster in monstersInstances)
            {
                if (monster.isActiveAndEnabled)
                    monster.MoveToPoint(playerPosition);
            }
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public void Reboot()
        {
            foreach (var monster in monstersInstances)
                monster.ReturnToPool();

            monstersInstances.Clear();
        }
    }
}