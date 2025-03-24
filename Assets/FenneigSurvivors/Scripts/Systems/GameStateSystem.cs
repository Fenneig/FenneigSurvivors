using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems
{
    public class GameStateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameStateComponent> _filter = null;
        
        private EnemiesConfig _enemiesConfig;
        private EcsWorld _ecsWorld;

        public GameStateSystem(EcsWorld ecsWorld, EnemiesConfig enemiesConfig)
        {
            _ecsWorld = ecsWorld;
            _enemiesConfig = enemiesConfig;
        }

        public void Run()
        {
            if (_filter.IsEmpty())
            {
                _ecsWorld.NewEntity().Replace(new GameStateComponent
                {
                    IsGamePaused = false, 
                    GameTime = 0, 
                    CurrentWave = 0
                }); 
            }
            ref var gameState = ref _filter.Get1(0);

            if (!gameState.IsGamePaused)
            {
                gameState.GameTime += Time.deltaTime;
            }

            int newDifficult = Mathf.FloorToInt(gameState.GameTime / _enemiesConfig.MeleeEnemyStats[gameState.CurrentWave].PhaseTime);
            
            if (newDifficult >= _enemiesConfig.MeleeEnemyStats.Count)
                _ecsWorld.NewEntity().Replace(new GameOverComponent { IsWin = true });
            else
                gameState.CurrentWave = newDifficult;
        }
    }
}
