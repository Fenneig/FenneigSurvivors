using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems
{
    public class CameraRotateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, MoveComponent, TransformComponent> _filter = null;

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var move = ref _filter.Get2(i);
                ref var transform = ref _filter.Get3(i);

                if (move.Direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(move.Direction);
                    transform.Value.rotation = Quaternion.Slerp(transform.Value.rotation, targetRotation, 2f * Time.deltaTime);
                }
            }
        }
    }
}
