using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets;
using FenneigSurvivors.Scripts.Objects.Weapons;
using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Bullets
{
    public class CleanBulletsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyBulletComponent, BulletComponent> _filter = null;

        private ProjectilePool _projectilePool;
        
        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i); 
                ref Projectile projectile = ref _filter.Get2(i).Projectile;
                
                _projectilePool.ReturnToPool(ProjectileType.Bullet, projectile);
                projectile.Trail.Clear();
                entity.Del<DestroyBulletComponent>();
                entity.Destroy();
            }
        }
    }
}
