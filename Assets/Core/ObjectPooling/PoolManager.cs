using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.ObjectPooling
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private Dictionary<string, Queue<GameObject>> _pools = new();
        private Dictionary<string, Transform> _containers = new();
        
        public T Get<T>(T prefab, Transform parent = null, bool active = true) where T : MonoBehaviour
        {
            var poolType =  prefab.GetType().ToString();
            if (_pools.TryGetValue(poolType, out var pool))
            {
                if (pool.TryDequeue(out var obj))
                {
                    obj.transform.SetParent(parent);
                    obj.SetActive(active);
                    return obj.GetComponent<T>();
                }
                else
                {
                    obj = Instantiate(prefab.gameObject, parent);
                    obj.SetActive(active);
                    return obj.GetComponent<T>();
                }
            }
            else
            {
                _pools.Add(poolType, new Queue<GameObject>());
                var obj = Instantiate(prefab.gameObject, parent);
                obj.SetActive(active);
                return obj.GetComponent<T>();
            }
        }

        public void Release<T>(T obj) where T : MonoBehaviour
        {
            var poolType = obj.GetType().ToString();
            if (_pools.TryGetValue(poolType, out var pool))
            {
                // Create object holder
                if (!_containers.TryGetValue(poolType, out var container))
                {
                    container = new GameObject() { name = poolType }.transform;
                    container.SetParent(transform);
                    _containers[poolType] = container;
                }
                
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(container.transform);
                pool.Enqueue(obj.gameObject);
            }
            else
            {
                _pools.Add(poolType, new Queue<GameObject>());
                // Create object holder
                if (!_containers.TryGetValue(poolType, out var container))
                {
                    container = new GameObject() { name = poolType }.transform;
                    container.SetParent(transform);
                    _containers[poolType] = container;
                }
                
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(container.transform);
                _pools[poolType].Enqueue(obj.gameObject);
            }
        }
    }
}