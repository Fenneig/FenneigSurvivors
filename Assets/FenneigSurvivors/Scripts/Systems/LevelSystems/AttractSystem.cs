using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class AttractSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttractComponent, TransformComponent> _attractFilter = null;
        private readonly EcsFilter<ExperienceComponent, TransformComponent> _playerFilter = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;
            
            foreach (int p in _playerFilter)
            {
                ref var playerTransform = ref _playerFilter.Get2(p);

                foreach (int i in _attractFilter)
                {
                    ref var attractTransform = ref _attractFilter.Get2(i);
                    ref var attract = ref _attractFilter.Get1(i);

                    Vector3 direction = (playerTransform.Value.position - attractTransform.Value.position).normalized;
                    attract.Speed += Time.deltaTime * 2f;
                    attractTransform.Value.position += direction * attract.Speed * Time.deltaTime;
                }
            }
        }
    }
}
