
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DATASAKURA
{
    /// <summary>
    /// Пулл объектов
    /// </summary>
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Stack<T> _pool;
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly DiContainer _container;

        public ObjectPool(T prefab, DiContainer container, int initialSize = 0)
        {
            _prefab = prefab;
            _container = container;
            _parent = new GameObject($"Pool_{typeof(T).Name}").transform;
            
            _pool = new Stack<T>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        public T Get()
        {
            T obj = _pool.Count > 0 ? _pool.Pop() : CreateNewObject();

            if (obj is IPoolableObject poolable)
                poolable.OnSpawned();

            return obj;
        }

        public void Return(T obj)
        {
            if (obj is IPoolableObject poolable)
                poolable.OnDespawned();

            obj.transform.SetParent(_parent);

            _pool.Push(obj);
        }

        private T CreateNewObject()
        {
            T obj = _container.InstantiatePrefabForComponent<T>(_prefab);
            obj.transform.SetParent(_parent);
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
            return obj;
        }
    }
}