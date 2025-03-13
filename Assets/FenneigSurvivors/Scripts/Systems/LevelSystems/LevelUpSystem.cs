using FenneigSurvivors.Scripts.Components.LevelComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class LevelUpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExperienceComponent> _experience;

        public void Run()
        {
            foreach (int e in _experience)
            {
                ref var experience = ref _experience.Get1(e);
                if (experience.CurrentXp >= experience.RequiredXp)
                {
                    experience.CurrentXp -= experience.RequiredXp;
                    Debug.Log("Player level up!");
                }
            }
        }
    }
}
