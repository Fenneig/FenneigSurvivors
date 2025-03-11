using System;
using System.Collections.Generic;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Spawners.Pools
{
    [Serializable]
    public class AbstractPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialCount;
        
        private Queue<T> _pool = new Queue<T>();

        private void Awake()
        {
            for (int i = 0; i < _initialCount; i++)
            {
                var instance = Instantiate(_prefab, transform);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }
        }

        public virtual T Get()
        {
            T instance;
            if (_pool.Count > 0)
            {
                instance = _pool.Dequeue();
                instance.gameObject.SetActive(true);
                return instance;
            }

            instance = Instantiate(_prefab, transform);
            return instance;
        }
        
        public virtual void ReturnToPool(T instance)
        {
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }
}
