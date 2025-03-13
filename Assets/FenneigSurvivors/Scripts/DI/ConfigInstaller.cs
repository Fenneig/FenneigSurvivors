using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.DI
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private Config _config;
        
        public override void InstallBindings()
        {
            Container.Bind<Config>().FromInstance(_config).AsSingle().NonLazy();
        }
    }
}
