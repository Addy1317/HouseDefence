using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouseDefence
{
    public class GenericObjectPool<T> where T : MonoBehaviour
    {
        private Queue<T> _pool;
        private T _prefab;
        private Transform _parent;

        public GenericObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new Queue<T>();

            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        private void CreateNewObject()
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        public T Get()
        {
            if (_pool.Count == 0)
            {
                CreateNewObject();
            }

            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}
