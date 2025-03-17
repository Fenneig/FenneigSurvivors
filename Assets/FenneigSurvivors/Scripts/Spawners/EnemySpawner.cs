using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.VisualComponents;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Objects;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners
{
    public class EnemySpawner : AbstractSpawner<Enemy>
    {
        [SerializeField] private Transform _minSpawnPosition;
        [SerializeField] private Transform _maxSpawnPosition;
        [Inject] private EnemiesConfig _enemiesConfig;
        int _currentLevel;
        
        public void SpawnEnemy(int currentLevel)
        {
            _currentLevel = currentLevel;
            Create();
            SetupTimer();
        }

        private void SetupTimer()
        {
            var cooldownEntity = World.NewEntity();

            cooldownEntity.Replace(new SpawnCooldownComponent { RemainTime = _enemiesConfig.MeleeEnemyStats[_currentLevel].SpawnCooldownDuration });
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
            enemy.SetMaterial(_enemiesConfig.MeleeEnemyStats[_currentLevel].EnemyMaterial);
        }

        private void SetupTransform(EcsEntity entity, Enemy enemy)
        {
            entity.Replace(new TransformComponent { Value = enemy.transform });

            float randomXPosition = Mathf.Lerp(_minSpawnPosition.position.x, _maxSpawnPosition.position.x, Random.Range(0, 1f));
            float randomZPosition = Mathf.Lerp(_minSpawnPosition.position.z, _maxSpawnPosition.position.z, Random.Range(0, 1f));

            Vector3 randomSpawnPosition = new Vector3(randomXPosition, 1, randomZPosition);

            enemy.transform.position = randomSpawnPosition;
        }

        private void SetupMovement(EcsEntity entity)
        {
            entity.Replace(new MoveComponent { Speed = _enemiesConfig.MeleeEnemyStats[_currentLevel].EnemySpeed });
        }

        private void SetupHealth(EcsEntity entity, Enemy enemy)
        {
            entity.Replace(new HealthComponent
            {
                MaxHealth = _enemiesConfig.MeleeEnemyStats[_currentLevel].EnemyHealth, 
                CurrentHealth = _enemiesConfig.MeleeEnemyStats[_currentLevel].EnemyHealth
            });
            entity.Replace(new HpBarComponent { View = enemy.HpBarView });
        }
    }
}
