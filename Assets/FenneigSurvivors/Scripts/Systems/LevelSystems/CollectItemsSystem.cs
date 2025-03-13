using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class CollectItemsSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, TransformComponent> _playerFilter = null;
        private EcsFilter<PickUpComponent, TransformComponent> _pickUpFilter = null;

        public void Run()
        {
            foreach (int i in _playerFilter)
            {
                ref var playerTransform = ref _playerFilter.Get2(i);
                foreach (int p in _pickUpFilter)
                {
                    ref var pickUpTransform = ref _pickUpFilter.Get2(p);

                    if (Vector3.Distance(pickUpTransform.Value.position, playerTransform.Value.position) < .5f)
                    {
                        ref var pickUpEntity = ref _pickUpFilter.GetEntity(p);
                        pickUpEntity.Del<PickUpComponent>();
                        pickUpEntity.Replace(new ApplyItemEffectComponent());
                    }
                }
            }
        }
    }
}
