using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems
{
    public class BulletSpawnerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BulletInitializeComponent> _filter = null;
        private BulletSpawner _bulletSpawner;
        
        public BulletSpawnerSystem(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }

        public void Run()
        {
            if (_filter.IsEmpty())
                return;
            
            foreach (int i in _filter)
            {
                ref var initData = ref _filter.Get1(i); 
                _bulletSpawner.CreateBullet(initData.Position, initData.Direction);
                _filter.GetEntity(i).Del<BulletInitializeComponent>();
            }
        }
    }
}
