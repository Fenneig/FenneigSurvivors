using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class RepelSystem : IEcsRunSystem
    {
        private EcsFilter<PickUpRepelComponent, TransformComponent> _pickUpFilter = null;
        
        public void Run()
        {
            foreach (int pickUps in _pickUpFilter)
            {
                ref var pickUpEntity = ref _pickUpFilter.GetEntity(pickUps);
                ref var pickUpTransform = ref _pickUpFilter.Get2(pickUps);
                ref var pickUpRepel = ref _pickUpFilter.Get1(pickUps);
                
                pickUpTransform.Value.position += pickUpRepel.Direction * pickUpRepel.Speed * Time.deltaTime;
                
                pickUpRepel.TimeRemaining -= Time.deltaTime;
                
                if (pickUpRepel.TimeRemaining <= 0)
                {
                    pickUpEntity.Del<PickUpRepelComponent>();
                    pickUpEntity.Replace(new AttractComponent { Speed = 1f });
                    pickUpEntity.Replace(new PickUpComponent());
                }
            }
        }
    }
}
