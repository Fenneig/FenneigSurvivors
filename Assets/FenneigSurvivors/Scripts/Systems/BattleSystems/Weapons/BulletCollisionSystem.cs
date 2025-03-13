using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons
{
    public class BulletCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BulletComponent, TransformComponent> _bulletFilter = null;
        private readonly EcsFilter<EnemyComponent, TransformComponent, HealthComponent> _enemyFilter = null;

        public void Run()
        {
            foreach (int bullet in _bulletFilter)
            {
                ref var bulletEntity = ref _bulletFilter.GetEntity(bullet);
                ref var bulletTransform = ref _bulletFilter.Get2(bullet);

                foreach (int enemy in _enemyFilter)
                {
                    ref var enemyEntity = ref _enemyFilter.GetEntity(enemy);
                    ref var enemyTransform = ref _enemyFilter.Get2(enemy);

                    if (Vector3.Distance(bulletTransform.Value.position, enemyTransform.Value.position) < 0.5f)
                    {
                        int damageAmount = bulletEntity.Get<BulletComponent>().Damage;
                        
                        enemyEntity.Replace(new DamageComponent{Value = damageAmount});

                        bulletEntity.Get<DestroyBulletComponent>();
                        break;
                    }
                }
            }
        }
    }
}
