using FenneigSurvivors.Scripts.Components.EnemyComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class SpawnCooldownSystem : IEcsRunSystem
    {
        private EcsFilter<SpawnCooldownComponent> _filter;
        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var spawnCoolDown = ref _filter.Get1(i);
                spawnCoolDown.RemainTime -= Time.deltaTime;
                if (spawnCoolDown.RemainTime <= 0)
                    entity.Del<SpawnCooldownComponent>();
            }
        }
    }
}
