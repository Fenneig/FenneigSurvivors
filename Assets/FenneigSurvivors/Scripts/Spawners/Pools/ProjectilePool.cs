using System.Collections.Generic;
using FenneigSurvivors.Scripts.Objects.Weapons;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners.Pools
{
    public class ProjectilePool
    {
        private readonly Dictionary<ProjectileType, Queue<Projectile>> _pools = new();
        private readonly Dictionary<ProjectileType, Projectile> _prefabs = new();

        [Inject] private DiContainer _container;

        public void RegisterProjectileType(ProjectileType type, Projectile prefab, int initialCount)
        {
            _prefabs[type] = prefab;
            _pools[type] = new Queue<Projectile>();

            for (int i = 0; i < initialCount; i++)
            {
                Projectile projectile = _container.InstantiatePrefab(prefab).GetComponent<Projectile>();
                projectile.gameObject.SetActive(false);
                _pools[type].Enqueue(projectile);
            }
        }

        public Projectile Get(ProjectileType type)
        {
            if (!_pools.ContainsKey(type))
            {
                Debug.LogError($"Projectile {type} doesn't registered in pool!");
                return null;
            }

            Projectile instance;
            
            if (_pools[type].Count > 0)
            {
                instance = _pools[type].Dequeue();
                instance.gameObject.SetActive(true);
                return instance;
            }

            instance = _container.InstantiatePrefab(_prefabs[type]).GetComponent<Projectile>();
            return instance;
        }

        public void ReturnToPool(ProjectileType type, Projectile instance)
        {
            instance.gameObject.SetActive(false);
            _pools[type].Enqueue(instance);
        }
    }
}
