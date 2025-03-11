using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class InvulnerableSystem : IEcsRunSystem
    {
        private EcsFilter<InvulnerableComponent> _filter;

        public void Run()
        {
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
