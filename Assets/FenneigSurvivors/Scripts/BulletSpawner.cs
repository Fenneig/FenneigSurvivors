using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletSpeed;

        private EcsWorld _ecsWorld;
        
        public void Init(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

        public void CreateBullet(Vector3 bulletPosition, Vector3 bulletDirection)
        {
            var bulletEntity = _ecsWorld.NewEntity();
            ref var bullet = ref bulletEntity.Get<BulletComponent>();
            ref var bulletTransform = ref bulletEntity.Get<TransformComponent>();
            ref var bulletMove = ref bulletEntity.Get<MoveComponent>();
            
            GameObject bulletGO = Instantiate(_bulletPrefab, bulletPosition, Quaternion.identity);
            
            bulletTransform.Value = bulletGO.transform;

            bulletMove.Direction = bulletDirection;
            bulletMove.Speed = _bulletSpeed;
        }
    }
}
