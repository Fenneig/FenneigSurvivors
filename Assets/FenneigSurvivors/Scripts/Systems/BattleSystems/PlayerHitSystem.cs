using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Components.PlayerComponents;
using Leopotam.Ecs;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class PlayerHitSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, HitComponent>.Exclude<InvulnerableComponent> _filter = null;
        
        private Config _config;
        
        public PlayerHitSystem(Config config)
        {
            _config = config;
        }
        
        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var player = ref _filter.GetEntity(i);
                
                player.Replace(new InvulnerableComponent { Duration = _config.PlayerInvulnerableCooldown });
                player.Del<HitComponent>(); 
            }
        }
    }
}