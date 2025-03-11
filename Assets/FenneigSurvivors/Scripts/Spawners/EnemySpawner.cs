using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.VisualComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Objects;
using FenneigSurvivors.FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        [Inject] private Config _config;

        private EcsWorld _world;
        private EnemyPool _enemyPool;

        public void Init(EcsWorld ecsWorld, EnemyPool enemyPool)
        {
            _world = ecsWorld;
            _enemyPool = enemyPool;
        }

        public void SpawnEnemy()
        {
            CreateEnemyEntity();
            SetupTimer();
        }

        private void SetupTimer()
        {
            var cooldownEntity = _world.NewEntity();

            cooldownEntity.Replace(new SpawnCooldownComponent { RemainTime = _config.SpawnCooldownDuration });
        }

        private void CreateEnemyEntity()
        {
            var entity = _world.NewEntity();

            Enemy enemy = _enemyPool.Get();
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
            entity.Replace(new MoveComponent { Speed = _config.EnemySpeed });
        }

        private void SetupHealth(EcsEntity entity, Enemy enemy)
        {
            entity.Replace(new HealthComponent { MaxHealth = _config.EnemyHealth, CurrentHealth = _config.EnemyHealth });
            entity.Replace(new HpBarComponent { View = enemy.HpBarView });
        }
    }
}
