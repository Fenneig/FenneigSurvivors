using FenneigSurvivors.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.CommonSystems
{
    public class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, MoveComponent> _filter = null;

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var transform = ref _filter.Get1(i);
                ref var move = ref _filter.Get2(i);
                
                transform.Value.position += move.Direction * move.Speed * Time.deltaTime;
            }
        }
    }
}
