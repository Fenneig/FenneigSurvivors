using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Objects;
using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class EnemyDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent, CharacterDieComponent> _enemies;

        private EnemyPool _enemyPool;


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
