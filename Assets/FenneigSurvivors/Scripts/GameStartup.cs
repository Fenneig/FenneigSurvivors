using FenneigSurvivors.FenneigSurvivors.Scripts.Input;
using FenneigSurvivors.FenneigSurvivors.Scripts.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.FenneigSurvivors.Scripts
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private BulletSpawner _bulletSpawner;

        [Inject] private IInputService _inputService;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _playerSpawner.Init(_world);
            _bulletSpawner.Init(_world);

            _systems
                .Add(new PlayerInputSystem(_inputService))
                .Add(new MoveSystem())
                .Add(new PlayerAttackSystem(_world))
                .Add(new BulletSpawnerSystem(_bulletSpawner))
                //.Add(new CameraRotateSystem())
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _world.Destroy();
        }
    }
}
