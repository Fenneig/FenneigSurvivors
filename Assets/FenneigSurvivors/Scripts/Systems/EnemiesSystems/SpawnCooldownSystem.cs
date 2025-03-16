using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class SpawnCooldownSystem : IEcsRunSystem
    {
        private EcsFilter<SpawnCooldownComponent> _filter;
        private EcsFilter<PauseComponent> _pauseFilter;
        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;

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
