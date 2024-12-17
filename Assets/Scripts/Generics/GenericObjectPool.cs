#region Summary
/// <summary>
/// GenericObjectPool<T> is a generic object pooling class for MonoBehaviour-based objects.
/// It provides an efficient way to manage object instances by recycling objects instead of instantiating and destroying them repeatedly.
/// 
/// Key Features:
/// - Initializes a pool of objects of type T.
/// - Dynamically expands the pool if needed.
/// - Retrieves an object from the pool and activates it.
/// - Returns an object to the pool and deactivates it.
/// 
/// Usage:
/// - Pass a prefab of type T, the initial pool size, and optionally a parent transform.
/// </summary>
/// <typeparam name="T">The MonoBehaviour-based object type to be pooled.</typeparam>
#endregion
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
