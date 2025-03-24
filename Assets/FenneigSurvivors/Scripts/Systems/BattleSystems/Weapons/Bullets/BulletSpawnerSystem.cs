using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets;
using FenneigSurvivors.Scripts.Spawners.Weapons;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Bullets
{
    public class BulletSpawnerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BulletInitializeComponent> _filter = null;
        private BulletSpawner _bulletSpawner;

        public void Run()
        {
            if (_filter.IsEmpty())
                return;
            
            foreach (int i in _filter)
            {
                ref var initData = ref _filter.Get1(i); 
                _bulletSpawner.CreateAtPosition(initData.Position, initData.Direction);
                
                _filter.GetEntity(i).Del<BulletInitializeComponent>();
            }
        }
    }
}
