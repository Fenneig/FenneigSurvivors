using Cinemachine;
using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using FenneigSurvivors.Scripts.Components.VisualComponents;
using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Objects;
using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.Spawners
{
    public class PlayerSpawner : AbstractSpawner<Player>
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [Inject] private Config _config;

        public override void Init(EcsWorld world, AbstractPool<Player> pool = null)
        {
            base.Init(world, pool);
            Create();
        }

        public override void Create()
        {
            var entity = World.NewEntity();

            Player player = Instantiate(_playerPrefab);
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
