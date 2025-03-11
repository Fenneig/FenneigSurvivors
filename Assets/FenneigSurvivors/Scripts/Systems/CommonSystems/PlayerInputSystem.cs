using FenneigSurvivors.FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.FenneigSurvivors.Scripts.Components.PlayerComponents;
using FenneigSurvivors.FenneigSurvivors.Scripts.Input;
using Leopotam.Ecs;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.CommonSystems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, MoveComponent> _playerFilter = null;
        private readonly EcsFilter<PlayerComponent>.Exclude<AttackComponent> _attackFilter = null;
        private readonly IInputService _inputService;

        public PlayerInputSystem(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Run()
        {
            HandleMovement();

            HandleAttack();
        }

        private void HandleMovement()
        {
            Vector2 input = _inputService.MoveDirection;

            foreach (int i in _playerFilter)
            {
                ref var move = ref _playerFilter.Get2(i);
                move.Direction = new Vector3(input.x, 0, input.y).normalized;
            }
        }

        private void HandleAttack()
        {
            if (_inputService.IsAttacking)
                foreach (int i in _attackFilter)
                    _attackFilter.GetEntity(i).Get<AttackComponent>();
        }
    }
}
