using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, MoveComponent, TransformComponent> _playerFilter = null;

        public void Run()
        {
            foreach (int i in _playerFilter)
            {
                ref var move = ref _playerFilter.Get2(i);
                ref var transform = ref _playerFilter.Get3(i);

                if (move.Direction.sqrMagnitude > 0)
                    transform.Value.position += move.Direction * move.Speed * Time.deltaTime;
            }
            
        }
    }
}
