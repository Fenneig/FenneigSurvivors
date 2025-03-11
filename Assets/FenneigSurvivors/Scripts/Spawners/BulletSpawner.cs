using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Objects;
using FenneigSurvivors.FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Spawners
{
    public class BulletSpawner : MonoBehaviour
    {
        [Inject] private Config _config;

        private EcsWorld _ecsWorld;
        private BulletPool _bulletPool;

        public void Init(EcsWorld ecsWorld, BulletPool bulletPool)
        {
            _ecsWorld = ecsWorld;
            _bulletPool = bulletPool;
        }

        public void CreateBullet(Vector3 bulletPosition, Vector3 bulletDirection)
        {
            var bulletEntity = _ecsWorld.NewEntity();

            Bullet bullet = _bulletPool.Get();
            bullet.transform.position = bulletPosition;

            SetupTransform(bulletEntity, bullet);
            SetupMovement(bulletDirection, bulletEntity);
            SetupBullet(bulletEntity, bullet);

            SetupLifeTime(bulletEntity);
        }

        private void SetupLifeTime(EcsEntity bulletEntity)
        {
            bulletEntity.Replace(new BulletLifeTimeComponent { RemainTime = _config.BulletLifeTime });
        }

        private void SetupBullet(EcsEntity bulletEntity, Bullet bullet)
        {
            bulletEntity.Replace(new BulletComponent { Damage = _config.BulletDamage, Bullet = bullet });
        }

        private void SetupTransform(EcsEntity bulletEntity, Bullet bullet)
        {
            bulletEntity.Replace(new TransformComponent { Value = bullet.transform });
        }

        private void SetupMovement(Vector3 bulletDirection, EcsEntity bulletEntity)
        {
            bulletEntity.Replace(new MoveComponent { Direction = bulletDirection, Speed = _config.BulletsSpeed });
        }
    }
}
