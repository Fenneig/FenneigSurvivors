using FenneigSurvivors.Scripts.Components.LevelComponents;
using FenneigSurvivors.Scripts.Components.VisualComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class UpdateXpViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExperienceComponent, XpBarComponent> _filter = null;

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var xp = ref _filter.Get1(i);
                ref var xpBar = ref _filter.Get2(i);
                xpBar.XpView.SetXp(xp.CurrentXp, xp.RequiredXp);
            }
        }
    }
}
