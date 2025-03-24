using FenneigSurvivors.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.DI
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private Config _config;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private FireballConfig _fireballConfig;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<Config>().FromInstance(_config).AsSingle().NonLazy();
            Container.Bind<BulletConfig>().FromInstance(_bulletConfig).AsSingle().NonLazy();
            Container.Bind<FireballConfig>().FromInstance(_fireballConfig).AsSingle().NonLazy();
            Container.Bind<EnemiesConfig>().FromInstance(_enemiesConfig).AsSingle().NonLazy();
        }
    }
}
