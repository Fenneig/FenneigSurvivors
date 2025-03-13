using FenneigSurvivors.Scripts.Spawners.Pools;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.DI
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private XpOrbsPool _xpOrbsPool;
        
        public override void InstallBindings()
        {
            Container.Bind<BulletPool>().FromInstance(_bulletPool).AsSingle().NonLazy();
            Container.Bind<EnemyPool>().FromInstance(_enemyPool).AsSingle().NonLazy();
            Container.Bind<XpOrbsPool>().FromInstance(_xpOrbsPool).AsSingle().NonLazy();
        }
    }
}
