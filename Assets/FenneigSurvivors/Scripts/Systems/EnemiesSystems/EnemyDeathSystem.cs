using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Objects;
using FenneigSurvivors.FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class EnemyDeathSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, CharacterDieComponent> _enemies;

        private EnemyPool _enemyPool;

        public EnemyDeathSystem(EnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }

        public void Run()
        {
            foreach (int i in _enemies)
            {
                ref var entity = ref _enemies.GetEntity(i);
                ref Enemy enemy = ref _enemies.Get1(i).Enemy;

                _enemyPool.ReturnToPool(enemy);
                entity.Del<CharacterDieComponent>();
                entity.Destroy();
            }
        }
    }
}
