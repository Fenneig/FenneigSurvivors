using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Spawners;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent> _filter = null;
        private readonly EcsFilter<SpawnCooldownComponent> _spawnCooldownFilter = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;

        private EnemySpawner _enemySpawner;
        private Config _config;

        public EnemySpawnSystem(EnemySpawner enemySpawner, Config config)
        {
            _enemySpawner = enemySpawner;
            _config = config;
        }

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;
            
            if (!_spawnCooldownFilter.IsEmpty())
                return;
            
            int enemiesOnScene = _filter.GetEntitiesCount();
            if (enemiesOnScene < _config.MaxEnemiesOnScene)
                _enemySpawner.SpawnEnemy();
        }
    }
}
