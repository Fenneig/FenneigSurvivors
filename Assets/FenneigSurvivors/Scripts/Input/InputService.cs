using UnityEngine;

namespace FenneigSurvivors.Scripts.Input
{

    public class InputService : IInputService
    {
        private PlayerInputActions _playerInputActions;
        
        public Vector2 MoveDirection => _playerInputActions.Player.Move.ReadValue<Vector2>();
        public bool IsAttacking => _playerInputActions.Player.Attack.WasPressedThisFrame();

        public InputService()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
        }
    }
}
