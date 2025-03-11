using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.PlayerComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class MoveTowardPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, TransformComponent, MoveComponent> _enemyFilter = null;
        private EcsFilter<PlayerComponent, TransformComponent> _playerFilter = null;

        public void Run()
        {
            foreach (int enemy in _enemyFilter)
            {
                foreach (int player in _playerFilter)
                {
                    ref var playerTransform = ref _playerFilter.Get2(player).Value;
                    
                    ref var move = ref _enemyFilter.Get3(enemy);
                    move.Direction = (playerTransform.position - _enemyFilter.Get2(enemy).Value.position).normalized;
                }
            }
        }
    }
}
