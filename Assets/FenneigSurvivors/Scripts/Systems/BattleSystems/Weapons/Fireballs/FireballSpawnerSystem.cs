using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Fireballs;
using FenneigSurvivors.Scripts.Spawners.Weapons;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Fireballs
{
    public class FireballSpawnerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FireballInitializeComponent> _filter = null;
        private FireballSpawner _fireballSpawner;

        public void Run()
        {
            if (_filter.IsEmpty())
                return;
            
            foreach (int i in _filter)
            {
                ref var initData = ref _filter.Get1(i); 
                _fireballSpawner.CreateAtPosition(initData.Position, initData.Direction);
                _filter.GetEntity(i).Del<FireballInitializeComponent>();
            }
        }
    }
}
