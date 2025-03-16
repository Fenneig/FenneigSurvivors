using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class LevelUpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExperienceComponent> _experience;

        private readonly EcsWorld _ecsWorld;
        
        public LevelUpSystem(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

        public void Run()
        {
            foreach (int e in _experience)
            {
                ref var experience = ref _experience.Get1(e);
                if (experience.CurrentXp >= experience.RequiredXp)
                {
                    experience.CurrentXp -= experience.RequiredXp;
                    var pause = _ecsWorld.NewEntity().Get<PauseComponent>();
                }
            }
        }
    }
}
