using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class CollectLightItemsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, TransformComponent> _playerFilter = null;
        private readonly EcsFilter<LightPickUpComponent, TransformComponent> _pickUpFilter = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;
            
            foreach (var p in _playerFilter)
            {
                ref var playerTransform = ref _playerFilter.Get2(p);

                foreach (int pu in _pickUpFilter)
                {
                    ref var pickUpEntity = ref _pickUpFilter.GetEntity(pu);
                    ref var pickUpTransform = ref _pickUpFilter.Get2(pu);


                    if (Vector3.Distance(pickUpTransform.Value.position, playerTransform.Value.position) < 3f)
                    {
                        pickUpEntity.Del<LightPickUpComponent>();
                        
                        Vector3 repelDirection = (pickUpTransform.Value.position - playerTransform.Value.position).normalized;
                        float repelSpeed = 3f;
                        float repelTime = .2f;
                        
                        pickUpEntity.Replace(new PickUpRepelComponent{Direction = repelDirection, Speed = repelSpeed, TimeRemaining = repelTime});
                    }
                }
            }
        }
    }
}
