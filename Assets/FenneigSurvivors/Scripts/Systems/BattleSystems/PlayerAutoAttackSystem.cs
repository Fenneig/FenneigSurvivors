using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Fireballs;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Objects.Weapons;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class PlayerAutoAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, TransformComponent> _playerFilter = null;
        private readonly EcsFilter<EnemyComponent, TransformComponent> _enemyFilter = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;
        
        private readonly EcsFilter<BulletAutoAttackComponent, PlayerComponent> _bulletsCooldownFilter = null;
        private readonly EcsFilter<FireballAutoAttackComponent, PlayerComponent> _fireballsCooldownFilter = null;
        private EcsWorld _ecsWorld;
        private BulletConfig _bulletConfig;
        private FireballConfig _fireballConfig;

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;
            
            foreach (int i in _playerFilter)
            {
                ref EcsEntity playerEntity = ref _playerFilter.GetEntity(i);
                ref Transform playerTransform = ref _playerFilter.Get2(i).Value;
                
                if (_bulletsCooldownFilter.IsEmpty())
                    HandleAutoAttack<BulletAutoAttackComponent, BulletInitializeComponent>(playerEntity, playerTransform, _bulletConfig.AutoAttackCooldown);
                
                if (_fireballsCooldownFilter.IsEmpty())
                    HandleAutoAttack<FireballAutoAttackComponent, FireballInitializeComponent>(playerEntity, playerTransform, _fireballConfig.AutoAttackCooldown);
            }
            
            CountCooldowns(_bulletsCooldownFilter);
            CountCooldowns(_fireballsCooldownFilter);
        }
        
        private void HandleAutoAttack<T, TInit>(EcsEntity player, Transform playerTransform, float cooldown)
            where T : struct, IAttackCooldownComponent
            where TInit : struct, IInitializeComponent
        {
            if (_enemyFilter.IsEmpty()) 
                return;

            var projectileRequest = _ecsWorld.NewEntity();
            ref var init = ref projectileRequest.Get<TInit>();
            init.Position = playerTransform.position;
            init.Direction = CalculateClosestEnemyDirection(playerTransform.position);

            player.Replace(new T { AttackCooldown = cooldown });
        }

        private void CountCooldowns<T>(EcsFilter<T, PlayerComponent> filter)
            where T : struct, IAttackCooldownComponent
        {
            foreach (int i in filter)
            {
                ref var cooldown = ref filter.Get1(i);
                cooldown.AttackCooldown -= Time.deltaTime;
                if (cooldown.AttackCooldown <= 0)
                    filter.GetEntity(i).Del<T>();
            }
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
