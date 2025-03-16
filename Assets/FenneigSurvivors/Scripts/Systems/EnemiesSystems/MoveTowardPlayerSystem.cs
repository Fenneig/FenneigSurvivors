using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class MoveTowardPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, TransformComponent, MoveComponent> _enemyFilter = null;
        private EcsFilter<PlayerComponent, TransformComponent> _playerFilter = null;
        private EcsFilter<PauseComponent> _pauseFilter = null;

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;
            
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
