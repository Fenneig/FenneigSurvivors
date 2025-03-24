using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Fireballs;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Fireballs
{
    public class FireballCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<FireballComponent, TransformComponent>.Exclude<ExplosionComponent> _fireballFilter = null;
        private readonly EcsFilter<EnemyComponent, TransformComponent> _enemyFilter = null;

        public void Run()
        {
            foreach (int i in _fireballFilter)
            {
                ref var fireballEntity = ref _fireballFilter.GetEntity(i);
                ref var fireballTransform = ref _fireballFilter.Get2(i);

                foreach (int j in _enemyFilter)
                {
                    ref var enemyTransform = ref _enemyFilter.Get2(j);

                    if (Vector3.Distance(fireballTransform.Value.position, enemyTransform.Value.position) < 0.5f)
                    {
                        fireballEntity.Del<MoveComponent>();
                        fireballEntity.Replace(new ExplosionComponent { Position = fireballTransform.Value.position });
                    }
                }
            }
        }
    }
}
