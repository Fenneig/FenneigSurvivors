using Cinemachine;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.PlayerComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.VisualComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Objects;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private CinemachineVirtualCamera _camera;

        [Inject] private Config _config;
        
        private EcsWorld _world;

        public void Init(EcsWorld ecsWorld)
        {
            _world = ecsWorld;
            SpawnPlayer();
        }
        
        private void SpawnPlayer()
        {
            var entity = _world.NewEntity();
            
            Player player = Instantiate(_playerPrefab);
            entity.Replace(new PlayerComponent { Player = player });

            SetupTransform(entity, player);
            SetupMovement(entity);
            SetupHealth(entity, player);
            SetupCamera(player);
        }
        
        private void SetupTransform(EcsEntity entity, Player player)
        {
            ref var playerTransform = ref entity.Get<TransformComponent>();
            
            playerTransform.Value = player.transform;
        }

        private void SetupMovement(EcsEntity entity)
        {
            ref var move = ref entity.Get<MoveComponent>();

            move.Speed = _config.PlayerSpeed;
        }
        
        private void SetupHealth(EcsEntity entity, Player player)
        {
            entity.Replace(new HealthComponent { MaxHealth = _config.PlayerHealth, CurrentHealth = _config.PlayerHealth });
            
            entity.Replace(new HpBarComponent { View = player.HpBarView });
        }

        private void SetupCamera(Player player)
        {
            _camera.Follow = player.transform;
            _camera.LookAt = player.transform;
        }
    }
}
