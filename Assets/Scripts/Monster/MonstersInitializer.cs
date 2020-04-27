using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Interfaces;
using Common.Events;
using Common.Enums;

namespace Monster
{
    /// <summary>
    /// Класс, используемый для создания и инициализации монстров
    /// </summary>
    public class MonstersInitializer : MonoBehaviour, IValidation, IClearable
    {
        /// <summary>
        /// Поле, используемое для блокировки повторного запуска стартового создания монстров
        /// </summary>
        private static bool isInitialized = false;

        /// <summary>
        /// Поле, определяющее максимальное количество монстров на сцене
        /// </summary>
        [SerializeField]
        private int maxCountOfMonsters = 10;
        /// <summary>
        /// Коллекция, хранящая префабы всех монстров, которые могут быть созданы
        /// </summary>
        [SerializeField]
        private List<AbstractMonster> monstersPrefabs = null;
        /// <summary>
        /// Ссылка на объект, представляющий контроллер управления монстрами
        /// </summary>
        [SerializeField]
        private MonstersController monstersController = null;

        void Start()
        {
            Validation();
            EventController<PlayerEvents>.GameEvents[PlayerEvents.Lose] += Reboot;
        }

        /// <summary>
        /// Валидация полей класса
        /// </summary>
        public void Validation()
        {
            if (monstersPrefabs.Count < 1)
                Debug.LogError("Count of List with Monsters Prefabs less then 1 in script " + this);
            if (!monstersController)
                Debug.LogError("Instance \"MonstersController\" not set in " + this);
        }

        /// <summary>
        /// Метод запуска стартовой инициализации монстров
        /// </summary>
        public void Initialize()
        {
            if (isInitialized)
            {
                Debug.LogWarning("Monsters is Initialized!");
                return;
            }

            isInitialized = true;
            for (int i = 0; i < maxCountOfMonsters; i++)
                CreateMonster();

            EventController<MonsterEvents>.GameEvents[MonsterEvents.Died] += CreateMonster;
        }

        /// <summary>
        /// Метод, управляющий запуском метода создания монстра,
        /// если создание нового монстра возможно
        /// </summary>
        private void CreateMonster()
        {
            if (CheckIfCanInstantiate())
                InstantiateMonster();
        }

        /// <summary>
        /// Метод, проверяющий соответствие текущего количества активных монстров
        /// максимальному числу одновременно активных монстров
        /// </summary>
        /// <returns>Переменная, отражающая возможность создания нового монстра на сцене</returns>
        private bool CheckIfCanInstantiate()
        {
            var activeMonsterCount = monstersController.CheckAliveMonsters();

            if (activeMonsterCount < maxCountOfMonsters)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Метод создания нового монстра
        /// </summary>
        private void InstantiateMonster()
        {
            var idMonster = Random.Range(0, monstersPrefabs.Count);
            AbstractMonster monsterInstance = (AbstractMonster)monstersPrefabs[idMonster].GetPoolInstance();

            monsterInstance.transform.position = GetMonsterPosition();

            monstersController.AddNewMonster(monsterInstance);
        }

        /// <summary>
        /// Метод, используемый для получения координаты создания нового монстра
        /// </summary>
        /// <returns>Позиция инстанцирования монстра в виде вектора</returns>
        private Vector3 GetMonsterPosition()
        {
            // 4 границы, вдоль которых может быть создан объект
            var countOfBorders = 4;
            var sideOfSpawn = Random.Range(0, countOfBorders);

            switch (sideOfSpawn)
            {
                // нижняя граница игрового поля
                case 0:
                    {
                        float position = Random.Range(GameData.SceneDataPtr.LeftBorder, GameData.SceneDataPtr.RightBorder);
                        return new Vector3(position, 0, GameData.SceneDataPtr.DownBorder);
                    }

                // верхняя граница игрового поля
                case 1:
                    {
                        float position = Random.Range(GameData.SceneDataPtr.LeftBorder, GameData.SceneDataPtr.RightBorder);
                        return new Vector3(position, 0, GameData.SceneDataPtr.TopBorder);
                    }

                // левая граница игрового поля
                case 2:
                    {
                        float position = Random.Range(GameData.SceneDataPtr.DownBorder, GameData.SceneDataPtr.TopBorder);
                        return new Vector3(GameData.SceneDataPtr.LeftBorder, 0, position);
                    }

                // правая граница игрового поля
                case 3:
                    {
                        float position = Random.Range(GameData.SceneDataPtr.DownBorder, GameData.SceneDataPtr.TopBorder);
                        return new Vector3(GameData.SceneDataPtr.RightBorder, 0, position);
                    }

                // нижняя граница игрового поля
                default:
                    {
                        float position = Random.Range(GameData.SceneDataPtr.LeftBorder, GameData.SceneDataPtr.RightBorder);
                        return new Vector3(position, 0, GameData.SceneDataPtr.DownBorder);
                    }
            }
        }

        /// <summary>
        /// Перезагрузка объекта класса
        /// </summary>
        public void Reboot()
        {
            isInitialized = false;
        }
    }
}