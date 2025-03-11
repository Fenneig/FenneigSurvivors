using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Input
{
    public interface IInputService
    {
        Vector2 MoveDirection { get; }
        bool IsAttacking { get; }
    }
}