using FenneigSurvivors.Scripts.Components.LevelComponents;
using FenneigSurvivors.Scripts.Spawners.Pools;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.LevelSystems
{
    public class CleanUsedOrbsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyUsedOrbsComponent, XpOrbComponent> _filter;

        private readonly XpOrbsPool _xpOrbPool;
        
        public CleanUsedOrbsSystem(XpOrbsPool xpOrbPool)
        {
            _xpOrbPool = xpOrbPool;
        }

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var xpOrbComponent = ref _filter.Get2(i);

                _xpOrbPool.ReturnToPool(xpOrbComponent.XpOrb);
                entity.Del<DestroyUsedOrbsComponent>();
                entity.Destroy();
            }
        }
    }
}
