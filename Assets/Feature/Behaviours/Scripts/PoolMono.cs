using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FoolCardGame.Behaviours
{
    /// <summary>
    /// Пул объектов
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    public class PoolMono<T> where T: MonoBehaviour
    {
        private List<T> _pool;
        
        /// <summary>
        /// Префаб
        /// </summary>
        public T Prefab { get; }
        
        /// <summary>
        /// Сам может создать один объект, если в пулле закончились свободный объекты?
        /// </summary>
        public bool AutoExpand { get; set; }
        
        /// <summary>
        /// Контейнер где хранитятся объекты пулла 
        /// </summary>
        public Transform Container { get; }

        /// <summary>
        /// Конструктор пулла
        /// </summary>
        /// <param name="count">Количество объектов в пулле</param>
        /// <param name="prefab">Префаб объекта</param>
        public PoolMono(int count, T prefab)
        {
            Prefab = prefab;
            Container = null;
        
            CreatePool(count);
        }
    
        /// <summary>
        /// Конструктор пулла
        /// </summary>
        /// <param name="count">Количество объектов в пулле</param>
        /// <param name="prefab">Префаб объекта</param>
        /// <param name="container">Родитель-контейнер</param>
        public PoolMono(int count, T prefab, Transform container)
        {
            Prefab = prefab;
            Container = container;
        
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();
            for (var i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault=false)
        {
            var createdObject = Object.Instantiate(Prefab, Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        private bool TryGetObject(out T gameObject)
        {
            foreach (var mono in _pool.Where(mono => !mono.gameObject.activeInHierarchy))
            {
                mono.gameObject.SetActive(true);
                gameObject = mono;
                return true;
            }
            gameObject = null;
            return false;
        }

        /// <summary>
        /// Получить свободный объект
        /// </summary>
        /// <returns>Свободный объект</returns>
        /// <exception cref="Exception">Ошибка, что в пулле нет пустных объектов</exception>
        public T GetFreeObject()
        {
            if (TryGetObject(out var element))
                return element;
            if (AutoExpand)
                return CreateObject();
            throw new Exception($"There is no free element in pool of type {typeof(T)}");
        }
    }
}