using Cinemachine;
using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using FenneigSurvivors.Scripts.Components.VisualComponents;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Objects;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private CinemachineVirtualCamera _camera;
        
        [Inject] private Config _config;
        [Inject] private DiContainer _container;

        private EcsWorld _world;
        
        public void Init(EcsWorld world)
        {
            _world = world;
            Create();
        }

        private void Create()
        {
            var entity = _world.NewEntity();

            Player player = _container.InstantiatePrefab(_playerPrefab).GetComponent<Player>();
            entity.Replace(new PlayerComponent { Player = player });

            SetupTransform(entity, player);
            SetupMovement(entity);
            SetupHealth(entity, player);
            SetupCamera(player);
            SetupExperience(entity, player);
        }

        private void SetupTransform(EcsEntity entity, Player player)
        {
            entity.Replace(new TransformComponent { Value = player.transform });
        }

        private void SetupMovement(EcsEntity entity)
        {
            entity.Replace(new MoveComponent { Speed = _config.PlayerSpeed });
        }

        private void SetupHealth(EcsEntity entity, Player player)
        {
            entity.Replace(new HealthComponent { MaxHealth = _config.PlayerHealth, CurrentHealth = _config.PlayerHealth });

            entity.Replace(new HpBarComponent { View = player.HpBarView });
        }

        private void SetupExperience(EcsEntity entity, Player player)
        {
            entity.Replace(new ExperienceComponent { CurrentXp = 0, RequiredXp = 10 });

            entity.Replace(new XpBarComponent { XpView = player.XpView});
        }

        private void SetupCamera(Player player)
        {
            _camera.Follow = player.transform;
            _camera.LookAt = player.transform;
        }
    }
}
