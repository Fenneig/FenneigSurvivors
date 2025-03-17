using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class DifficultSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BattleTimeComponent> _timeFilter;
        private readonly EcsFilter<DifficultyLevelComponent> _difficultFilter;

        private EnemiesConfig _config;
        
        public DifficultSystem(EnemiesConfig config)
        {
            _config = config;
        }

        public void Run()
        {
            foreach (int i in _timeFilter)
            {
                ref var time = ref _timeFilter.Get1(i);
                time.PhaseTime -= Time.deltaTime;

                if (time.PhaseTime <= 0)
                {
                    foreach (int j in _difficultFilter)
                    {
                        ref var currentDifficult = ref _difficultFilter.Get1(j);
                        float currentPhaseTime = _config.MeleeEnemyStats[currentDifficult.CurrentLevel].PhaseTime;
                        time.PhaseTime += currentPhaseTime;
                        if (currentDifficult.CurrentLevel + 1 >= _config.MeleeEnemyStats.Count)
                        {
                            ref EcsEntity timeEntity = ref _timeFilter.GetEntity(i);
                            timeEntity.Destroy();
                        }
                        else
                        {
                            currentDifficult.CurrentLevel++;
                        }
                    }
                }
            }
        }
    }
}
