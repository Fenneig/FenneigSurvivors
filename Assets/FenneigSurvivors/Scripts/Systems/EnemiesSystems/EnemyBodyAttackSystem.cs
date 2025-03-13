using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class EnemyBodyAttackSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, TransformComponent> _enemyFilter;
        private EcsFilter<PlayerComponent, TransformComponent> _playerFilter;

        private Config _config;

        public EnemyBodyAttackSystem(Config config)
        {
            _config = config;
        }

        public void Run()
        {
            foreach (int i in _playerFilter)
            {
                ref var player = ref _playerFilter.GetEntity(i);
                ref var playerTransformPosition = ref _playerFilter.Get2(i).Value;
                foreach (int j in _enemyFilter)
                {
                    ref var enemyTransformPosition = ref _enemyFilter.Get2(j).Value;

                    if (Vector3.Distance(playerTransformPosition.position, enemyTransformPosition.position) < _config.EnemyAttackDistance)
                    {
                        int damageAmount = _config.EnemyAttackDamage;
                        
                        player.Replace(new DamageComponent { Value = damageAmount });
                        break;
                    }
                }

            }
        }

    }
}
