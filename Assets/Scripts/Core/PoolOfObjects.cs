using System.Collections.Generic;
using UnityEngine;
using Common.Interfaces;

namespace Core
{
    /// <summary>
    /// Класс используется для создания пула объектов, реализующих интерфейс IPoolable,
    /// хранения объектов и создания новых. Класс обеспечивает оптимальный алгоритм работы с памятью,
    /// так как объекты пула не удаляются, а отключаются на период неактивности
    /// </summary>
    public class PoolOfObjects : MonoBehaviour
    {
        /// <summary>
        /// Поле содержит префаб объекта того типа, для которого был создан пул
        /// </summary>
        private IPoolable prefabOfObject = null;

        /// <summary>
        /// Коллекция, используемая для хранения объектов пула
        /// (объекты неактивны и доступны для использования)
        /// </summary>
        private List<IPoolable> availableInstances = new List<IPoolable>();

        /// <summary>
        /// Метод, используемый для создания нового пула объектов
        /// </summary>
        /// <param name="prefab">Параметр содержит префаб объекта
        /// определенного типа, для которого нужно создать пул</param>
        /// <returns>Ссылка на пул объектов заданного типа</returns>
        public static PoolOfObjects GetPool<T>(T prefab) where T: MonoBehaviour, IPoolable
        {
            var objPool = new GameObject("Pool " + prefab.name);
            var pool = objPool.AddComponent<PoolOfObjects>();

            pool.prefabOfObject = prefab;
            return pool;
        }

        /// <summary>
        /// Метод, используемый для получения экземпляра объекта из пула
        /// </summary>
        /// <returns>Экземпляр объекта пула</returns>
        public T GetObject<T>() where T : MonoBehaviour, IPoolable
        {
            T instance;

            var idLastAvailable = availableInstances.Count - 1;

            if (idLastAvailable >= 0)
            {
                instance = (T)availableInstances[idLastAvailable];
                availableInstances.RemoveAt(idLastAvailable);
                instance.gameObject.SetActive(true);
            }
            // Создание нового объекта пула
            else
            {
                instance = Instantiate((T)prefabOfObject);
                instance.transform.SetParent(transform, false);
                instance.PoolInstance = this;
            }

            return instance;
        }

        /// <summary>
        /// Метод используется для возврата освободившегося объекта пула назад в пул
        /// </summary>
        /// <param name="instance">Объект пула</param>
        public void AddObject<T>(T instance) where T : MonoBehaviour, IPoolable
        {
            instance.gameObject.SetActive(false);
            availableInstances.Add(instance);
        }
    }
}