using Zenject;

namespace FenneigSurvivors.Scripts.Input
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle();
        }
    }
}
