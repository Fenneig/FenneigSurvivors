using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Objects;
using FenneigSurvivors.FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons
{
    public class CleanBulletsSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyBulletComponent, BulletComponent> _filter = null;

        private BulletPool _bulletPool;

        public CleanBulletsSystem(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i); 
                ref Bullet bullet = ref _filter.Get2(i).Bullet;
                
                _bulletPool.ReturnToPool(bullet);
                entity.Del<DestroyBulletComponent>();
                entity.Destroy();
            }
        }
    }
}
