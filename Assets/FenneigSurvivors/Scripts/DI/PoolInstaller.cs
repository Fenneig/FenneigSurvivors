using FenneigSurvivors.FenneigSurvivors.Scripts.Spawners.Pools;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.DI
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private EnemyPool _enemyPool;
        
        public override void InstallBindings()
        {
            Container.Bind<BulletPool>().FromInstance(_bulletPool).AsSingle().NonLazy();
            Container.Bind<EnemyPool>().FromInstance(_enemyPool).AsSingle().NonLazy();
        }
    }
}
