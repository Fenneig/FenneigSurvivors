using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Fireballs;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Objects;
using FenneigSurvivors.Scripts.Objects.Effects;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.VFX;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Fireballs
{
    public class FireballExplosionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FireballComponent, ExplosionComponent> _fireballFilter = null;
        private readonly EcsFilter<EnemyComponent, TransformComponent, HealthComponent> _enemiesFilter = null;
        private VFXSpawner _vfxSpawner;

        public void Run()
        {
            foreach (int i in _fireballFilter)
            {
                ref var fireballEntity = ref _fireballFilter.GetEntity(i);
                ref var fireball = ref _fireballFilter.Get1(i);
                ref var explosion = ref _fireballFilter.Get2(i);
                
                foreach (int j in _enemiesFilter)
                {
                    ref var enemyEntity = ref _enemiesFilter.GetEntity(j);
                    ref var enemyTransform = ref _enemiesFilter.Get2(j);
                    
                    if (Vector3.Distance(explosion.Position, enemyTransform.Value.position) <= fireball.ExplosionRadius)
                        enemyEntity.Replace(new DamageComponent { Value = fireball.Damage });
                }

                _vfxSpawner.Spawn(VFXType.Fireball, explosion.Position, fireball.ExplosionRadius);
                    
                fireballEntity.Del<ExplosionComponent>();
                fireballEntity.Replace(new DestroyFireballComponent());
            }
        }
    }

}
