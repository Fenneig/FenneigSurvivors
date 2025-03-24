using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Objects.Weapons;
using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners.Weapons
{
    public class BulletSpawner : MonoBehaviour
    {
        [Inject] private BulletConfig _config;
        [Inject] private ProjectilePool _pool;
        
        private EcsWorld _world;

        public void Init(EcsWorld world)
        {
            _world = world;
            _pool.RegisterProjectileType(ProjectileType.Bullet, _config.Prefab, _config.StartInitialCount);
        }
        
        public void CreateAtPosition(Vector3 position, Vector3 direction)
        {
            var bulletEntity = _world.NewEntity();

            Projectile projectile = _pool.Get(ProjectileType.Bullet);
            projectile.transform.position = position;

            SetupTransform(bulletEntity, projectile);
            SetupMovement(direction, bulletEntity);
            SetupBullet(bulletEntity, projectile);

            SetupLifeTime(bulletEntity);
        }

        private void SetupLifeTime(EcsEntity bulletEntity)
        {
            bulletEntity.Replace(new ProjectileLifeTimeComponent { RemainTime = _config.LifeTime });
        }

        private void SetupBullet(EcsEntity bulletEntity, Projectile projectile)
        {
            bulletEntity.Replace(new BulletComponent { Damage = _config.Damage, Projectile = projectile });
        }

        private void SetupTransform(EcsEntity bulletEntity, Projectile projectile)
        {
            bulletEntity.Replace(new TransformComponent { Value = projectile.transform });
        }

        private void SetupMovement(Vector3 bulletDirection, EcsEntity bulletEntity)
        {
            bulletEntity.Replace(new MoveComponent { Direction = bulletDirection, Speed = _config.Speed });
        }
    }
}
