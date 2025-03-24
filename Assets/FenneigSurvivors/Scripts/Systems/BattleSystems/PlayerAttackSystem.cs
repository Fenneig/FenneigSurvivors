using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, AttackComponent, TransformComponent> _attackFilter = null;
        private readonly EcsFilter<EnemyComponent, TransformComponent> _enemyFilter = null;
        private EcsWorld _ecsWorld;
        
        public PlayerAttackSystem(EcsWorld ecsWorld) => 
            _ecsWorld = ecsWorld;
        
        public void Run()
        {
            foreach (int i in _attackFilter)
            {
                RemoveAttackMark(i);

                CreateBulletAndRequest(_attackFilter.Get3(i).Value);
            }
        }

        private void RemoveAttackMark(int i)
        {
            ref var entity = ref _attackFilter.GetEntity(i);
            entity.Del<AttackComponent>();
        }
        
        private void CreateBulletAndRequest(Transform value)
        {
            var bulletRequest = _ecsWorld.NewEntity();

            ref var bulletInitialize = ref bulletRequest.Get<BulletInitializeComponent>();
            bulletInitialize.Position = value.position;
            bulletInitialize.Direction = CalculateClosestEnemyDirection(value.position);
        }
        
        private Vector3 CalculateClosestEnemyDirection(Vector3 position)
        {
            Vector3 closestEnemyPosition = Vector3.zero;
            float closestEnemy = float.MaxValue; 
            foreach (var i in _enemyFilter)
            {
                ref var enemyTransform = ref _enemyFilter.Get2(i);
                
                if (Vector3.Distance(position, enemyTransform.Value.position) < closestEnemy)
                {
                    closestEnemy = Vector3.Distance(position, enemyTransform.Value.position);
                    closestEnemyPosition = enemyTransform.Value.position;
                }
            }
            Vector3 normalizedDirection = (closestEnemyPosition - position).normalized;
            return normalizedDirection;
        }
    }
}
