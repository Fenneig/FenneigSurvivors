using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.VisualComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.CommonSystems
{
    public class UpdateHpBarsSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, HpBarComponent> _filter = null;

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var health = ref _filter.Get1(i);
                ref var hpBar = ref _filter.Get2(i);
                hpBar.View.SetHp(health.CurrentHealth, health.MaxHealth);
            }
        }
    }
}
