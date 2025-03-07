using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Input
{
    public interface IInputService
    {
        Vector2 MoveDirection { get; }
        bool IsAttacking { get; }
    }
    
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
