using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class InvulnerableSystem : IEcsRunSystem
    {
        private EcsFilter<InvulnerableComponent> _filter;
        private EcsFilter<PauseComponent> _pauseFilter;

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;

            foreach (int i in _filter)
            {
                ref var invulnerable = ref _filter.Get1(i);
                invulnerable.Duration -= Time.deltaTime;
                
                if (invulnerable.Duration <= 0)
                    _filter.GetEntity(i).Del<InvulnerableComponent>();
            }
        }
    }
}
