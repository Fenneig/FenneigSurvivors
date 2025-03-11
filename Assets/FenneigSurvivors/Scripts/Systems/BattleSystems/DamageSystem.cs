using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageComponent, HealthComponent>.Exclude<InvulnerableComponent> _hitFilter;

        public void Run()
        {
            foreach (int hit in _hitFilter)
            {
                ref var entity = ref _hitFilter.GetEntity(hit);
                ref HealthComponent health = ref _hitFilter.Get2(hit);
                int damageAmount = _hitFilter.Get1(hit).Value;
                health.CurrentHealth -= damageAmount;

                _hitFilter.GetEntity(hit).Del<DamageComponent>();
                
                if (health.CurrentHealth <= 0)
                {
                    health.CurrentHealth = 0;
                    entity.Get<CharacterDieComponent>();
                }
                else
                {
                    entity.Replace(new HitComponent());
                }
            }
        }
    }
}
