using FenneigSurvivors.Scripts.Objects.Effects;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts.DI
{
    public class VFXInstaller : MonoInstaller
    {
        [SerializeField] private VFXSpawner _vfxSpawner;
        
        public override void InstallBindings()
        {
            Container.Bind<VFXSpawner>().FromInstance(_vfxSpawner).AsSingle().NonLazy();
        }
    }
}
