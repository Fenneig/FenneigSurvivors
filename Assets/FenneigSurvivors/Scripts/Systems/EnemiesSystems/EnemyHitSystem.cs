using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.EnemyComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.EnemiesSystems
{
    public class EnemyHitSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, HitComponent>.Exclude<HitEffectTimerComponent> _filter = null;
        private readonly Config _config;
        
        public EnemyHitSystem(Config config)
        {
            _config = config;
        }
        
        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                entity.Replace(new HitEffectTimerComponent { SwitchStateTimeRemaining = 0, EffectTimeRemaining = _config.HitEffectDuration });
                entity.Del<HitComponent>();
            }
        }
    }
}