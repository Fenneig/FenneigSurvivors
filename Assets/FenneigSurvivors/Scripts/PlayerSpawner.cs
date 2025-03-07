using Cinemachine;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private CinemachineVirtualCamera _camera;
        private EcsWorld _world;

        public void Init(EcsWorld ecsWorld)
        {
            _world = ecsWorld;
            SpawnPlayer();
        }
        
        private void SpawnPlayer()
        {
            var entity = _world.NewEntity();

            ref var player = ref entity.Get<PlayerComponent>();
            ref var move = ref entity.Get<MoveComponent>();
            ref var playerTransform = ref entity.Get<TransformComponent>();

            move.Speed = 5f;

            GameObject playerGO = Instantiate(_playerPrefab);
            playerTransform.Value = playerGO.transform;
            _camera.Follow = playerGO.transform;
            _camera.LookAt = playerGO.transform;
        }
    }
}
