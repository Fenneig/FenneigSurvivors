using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.EnemyComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using FenneigSurvivors.Scripts.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class PlayerAutoAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, TransformComponent>.Exclude<AutoAttackComponent> _playerFilter = null;
        private readonly EcsFilter<EnemyComponent, TransformComponent> _enemyFilter = null;
        private readonly EcsFilter<AutoAttackComponent, PlayerComponent> _autoAttackFilter = null;
        private readonly EcsFilter<PauseComponent> _pauseFilter = null;
        private readonly EcsWorld _ecsWorld;
        private readonly BulletConfig _config;

        public PlayerAutoAttackSystem(EcsWorld ecsWorld, BulletConfig config)
        {
            _ecsWorld = ecsWorld;
            _config = config;
        }

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;
            
            foreach (int i in _playerFilter)
            {
                ref EcsEntity playerEntity = ref _playerFilter.GetEntity(i);
                
                CreateBulletRequest(playerEntity);
            }

            foreach (int i in _autoAttackFilter)
            {
                CountAutoAttackCooldown(_autoAttackFilter.GetEntity(i));
            }
        }
        private void CountAutoAttackCooldown(EcsEntity playerEntity)
        {
            foreach (int j in _autoAttackFilter)
            {
                ref var autoAttackTimer = ref _autoAttackFilter.Get1(j);
                autoAttackTimer.AttackCooldown -= Time.deltaTime;

                if (autoAttackTimer.AttackCooldown <= 0)
                    playerEntity.Del<AutoAttackComponent>();
            }
        }

        private void CreateBulletRequest(EcsEntity playerEntity)
        {
            if (_enemyFilter.IsEmpty())
                return;
            
            ref var playerTransform = ref playerEntity.Get<TransformComponent>().Value;
            var bulletRequest = _ecsWorld.NewEntity();

            SetBulletInitValues(bulletRequest, playerTransform);

            playerEntity.Replace(new AutoAttackComponent { AttackCooldown = _config.PlayerAutoAttackCooldown });
        }

        private void SetBulletInitValues(EcsEntity bulletRequest, Transform playerTransform)
        {
            ref var bulletInitialize = ref bulletRequest.Get<BulletInitializeComponent>();
            bulletInitialize.Position = playerTransform.position;
            bulletInitialize.Direction = CalculateClosestEnemyDirection(playerTransform.position);
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
