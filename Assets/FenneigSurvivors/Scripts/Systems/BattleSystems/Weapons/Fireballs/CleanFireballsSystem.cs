using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Fireballs;
using FenneigSurvivors.Scripts.Objects.Weapons;
using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Fireballs
{
    public class CleanFireballsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyFireballComponent, FireballComponent> _filter = null;

        private ProjectilePool _projectilePool;
        
        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i); 
                ref Projectile projectile = ref _filter.Get2(i).Projectile;
                
                _projectilePool.ReturnToPool(ProjectileType.Fireball, projectile);
                projectile.Trail.Clear();
                entity.Del<DestroyFireballComponent>();
                entity.Destroy();
            }
        }
    }
}