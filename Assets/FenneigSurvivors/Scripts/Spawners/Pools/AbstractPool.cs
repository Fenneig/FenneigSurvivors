using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners.Pools
{
    [Serializable]
    public class AbstractPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialCount;
        [Inject] private DiContainer _container;

        private Queue<T> _pool = new Queue<T>();

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _container = diContainer;
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialCount; i++)
            {
                T instance = _container.InstantiatePrefabForComponent<T>(_prefab);
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

            instance = _container.InstantiatePrefabForComponent<T>(_prefab);
            return instance;
        }
        
        public virtual void ReturnToPool(T instance)
        {
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }
}
