using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Fireballs;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Objects.Weapons;
using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners.Weapons
{
    public class FireballSpawner : MonoBehaviour
    {
        [Inject] private FireballConfig _config;
        [Inject] private ProjectilePool _pool;

        private EcsWorld _world;

        public void Init(EcsWorld world)
        {
            _world = world;
            _pool.RegisterProjectileType(ProjectileType.Fireball, _config.Prefab, _config.StartInitialCount);
        }

        public void CreateAtPosition(Vector3 position, Vector3 direction)
        {
            var fireballEntity = _world.NewEntity();

            Projectile projectile = _pool.Get(ProjectileType.Fireball);
            projectile.transform.position = position;

            SetupTransform(fireballEntity, projectile);
            SetupMovement(direction, fireballEntity);
            SetupFireball(fireballEntity, projectile);

            SetupLifeTime(fireballEntity);
        }

        private void SetupLifeTime(EcsEntity fireballEntity)
        {
            fireballEntity.Replace(new ProjectileLifeTimeComponent { RemainTime = _config.LifeTime });
        }

        private void SetupFireball(EcsEntity fireballEntity, Projectile projectile)
        {
            fireballEntity.Replace(new FireballComponent { Damage = _config.Damage, ExplosionRadius = _config.ExplosionRadius, Projectile = projectile });
        }

        private void SetupTransform(EcsEntity fireballEntity, Projectile projectile)
        {
            fireballEntity.Replace(new TransformComponent { Value = projectile.transform });
        }

        private void SetupMovement(Vector3 fireballEntity, EcsEntity bulletEntity)
        {
            bulletEntity.Replace(new MoveComponent { Direction = fireballEntity, Speed = _config.Speed });
        }
    }
}
