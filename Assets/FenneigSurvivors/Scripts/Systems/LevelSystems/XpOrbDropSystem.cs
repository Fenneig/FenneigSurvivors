using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Spawners;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class XpOrbDropSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, TransformComponent, CharacterDieComponent> _filter = null;

        private XpOrbSpawner _xpOrbSpawner;


        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var diedUnitTransformComponent = ref _filter.Get2(i);
                _xpOrbSpawner.CreateAtPosition(diedUnitTransformComponent.Value.position, Vector3.zero);
            }
        }
    }
}
