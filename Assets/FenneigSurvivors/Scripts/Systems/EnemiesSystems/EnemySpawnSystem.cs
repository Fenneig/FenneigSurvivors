using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Spawners;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent> _filter = null;
        private readonly EcsFilter<SpawnCooldownComponent> _spawnCooldownFilter = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;
        private readonly EcsFilter<DifficultyLevelComponent> _difficultyLevel = null;

        private EnemySpawner _enemySpawner;
        private EnemiesConfig _enemiesConfig;

        public EnemySpawnSystem(EnemySpawner enemySpawner, EnemiesConfig enemiesConfig)
        {
            _enemySpawner = enemySpawner;
            _enemiesConfig = enemiesConfig;
        }

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;
            
            if (!_spawnCooldownFilter.IsEmpty())
                return;

            foreach (int i in _difficultyLevel)
            {
                ref var difficulty = ref _difficultyLevel.Get1(i);
                
                int enemiesOnScene = _filter.GetEntitiesCount();
                var currentLevelValues = _enemiesConfig.MeleeEnemyStats[difficulty.CurrentLevel];
                if (enemiesOnScene < currentLevelValues.MaxEnemiesOnScene)
                {
                    for (int j = 0; j < currentLevelValues.EnemiesSpawnsPerSpawn; j++)
                        _enemySpawner.SpawnEnemy(difficulty.CurrentLevel);
                }
            }
        }
    }
}
