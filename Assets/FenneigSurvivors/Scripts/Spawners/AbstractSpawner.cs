using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Spawners
{
    public abstract class AbstractSpawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected EcsWorld World;
        protected AbstractPool<T> Pool;

        public virtual void Init(EcsWorld world, AbstractPool<T> pool = null)
        {
            World = world;
            Pool = pool;
        }

        public virtual void CreateAtPosition(Vector3 position, Vector3 direction) {}

        public virtual void Create() {}
    }
}
