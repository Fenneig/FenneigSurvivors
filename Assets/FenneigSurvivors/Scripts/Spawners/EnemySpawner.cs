using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.VisualComponents;
using FenneigSurvivors.Scripts.Objects;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Spawners
{
    public class EnemySpawner : AbstractSpawner<Enemy>
    {
        [SerializeField] private Transform[] _spawnPoints;
        
        public void SpawnEnemy()
        {
            Create();
            SetupTimer();
        }

        private void SetupTimer()
        {
            var cooldownEntity = World.NewEntity();

            cooldownEntity.Replace(new SpawnCooldownComponent { RemainTime = Config.SpawnCooldownDuration });
        }

        public override void Create()
        {
            var entity = World.NewEntity();

            Enemy enemy = Pool.Get();
            enemy.ResetHitState();

            SetupEnemy(entity, enemy);
            SetupTransform(entity, enemy);
            SetupMovement(entity);
            SetupHealth(entity, enemy);
        }

        private void SetupEnemy(EcsEntity entity, Enemy enemy)
        {
            entity.Replace(new EnemyComponent { Enemy = enemy });
        }

        private void SetupTransform(EcsEntity entity, Enemy enemy)
        {
            entity.Replace(new TransformComponent { Value = enemy.transform });

            enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        }

        private void SetupMovement(EcsEntity entity)
        {
            entity.Replace(new MoveComponent { Speed = Config.EnemySpeed });
        }

        private void SetupHealth(EcsEntity entity, Enemy enemy)
        {
            entity.Replace(new HealthComponent { MaxHealth = Config.EnemyHealth, CurrentHealth = Config.EnemyHealth });
            entity.Replace(new HpBarComponent { View = enemy.HpBarView });
        }
    }
}
