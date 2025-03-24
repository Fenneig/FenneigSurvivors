using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using FenneigSurvivors.Scripts.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class EnemyBodyAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent, TransformComponent> _enemyFilter = null;
        private readonly EcsFilter<PlayerComponent, TransformComponent> _playerFilter = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;
        private readonly EcsFilter<GameStateComponent> _gameStateFilter = null;

        private EnemiesConfig _enemiesConfig;
        
        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;

            foreach (int i in _gameStateFilter)
            {
                ref var state = ref _gameStateFilter.Get1(i);
                foreach (int j in _playerFilter)
                {
                    ref var player = ref _playerFilter.GetEntity(j);
                    ref var playerTransformPosition = ref _playerFilter.Get2(j).Value;
                    foreach (int k in _enemyFilter)
                    {
                        ref var enemyTransformPosition = ref _enemyFilter.Get2(k).Value;

                        if (Vector3.Distance(playerTransformPosition.position, enemyTransformPosition.position) < _enemiesConfig.MeleeEnemyStats[state.CurrentWave].EnemyAttackDistance)
                        {
                            int damageAmount = _enemiesConfig.MeleeEnemyStats[state.CurrentWave].EnemyAttackDamage;

                            player.Replace(new DamageComponent { Value = damageAmount });
                            break;
                        }
                    }
                }
            }
        }

    }
}
