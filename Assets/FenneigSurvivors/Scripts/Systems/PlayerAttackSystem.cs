using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, AttackComponent, TransformComponent> _attackFilter = null;
        private EcsWorld _ecsWorld;
        
        public PlayerAttackSystem(EcsWorld ecsWorld) => 
            _ecsWorld = ecsWorld;
        
        public void Run()
        {
            foreach (int i in _attackFilter)
            {
                RemoveAttackMark(i);

                CreateBulletRequest(_attackFilter.Get3(i).Value);
            }
        }

        private void RemoveAttackMark(int i)
        {
            ref var entity = ref _attackFilter.GetEntity(i);
            entity.Del<AttackComponent>();
        }
        
        private void CreateBulletRequest(Transform value)
        {
            var bulletRequest = _ecsWorld.NewEntity();

            ref var bulletInitialize = ref bulletRequest.Get<BulletInitializeComponent>();
            bulletInitialize.Direction = value.forward;
            bulletInitialize.Position = value.position;
        }
    }
}
