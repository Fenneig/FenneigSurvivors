using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class ApplyExpOrbEffectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExperienceComponent> _playerFilter = null;
        private readonly EcsFilter<ApplyItemEffectComponent, XpOrbComponent> _orbComponent = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;
        
        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;

            foreach (int i in _playerFilter)
            {
                ref var player = ref _playerFilter.Get1(i);
                foreach (int j in _orbComponent)
                {
                    ref var orbEntity = ref _orbComponent.GetEntity(j);
                    ref var orb = ref _orbComponent.Get2(j);
                    player.CurrentXp += orb.XpAmount;
                    orbEntity.Del<ApplyItemEffectComponent>();
                    orbEntity.Replace(new DestroyUsedOrbsComponent());
                }
            }
        }
    }
}
