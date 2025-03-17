using System;
using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Objects;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners
{
    public class BulletSpawner : AbstractSpawner<Bullet>
    {
        [Inject] private BulletConfig _config;
        public override void CreateAtPosition(Vector3 position, Vector3 direction)
        {
            var bulletEntity = World.NewEntity();

            Bullet bullet = Pool.Get();
            bullet.transform.position = position;

            SetupTransform(bulletEntity, bullet);
            SetupMovement(direction, bulletEntity);
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
