using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class HitEffectSystem : IEcsRunSystem
    {
        private EcsFilter<HitEffectTimerComponent, EnemyComponent> _filter = null;

        private Config _config;
        
        public HitEffectSystem(Config config)
        {
            _config = config;
        }
        
        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var switchState = ref _filter.Get1(i);
                if (switchState.SwitchStateTimeRemaining <= 0)
                {
                    switchState.EffectTimeRemaining = _config.HitSwitchDuration;
                    _filter.Get2(i).Enemy.SwitchHitState();
                }
            }
        }
    }
}
