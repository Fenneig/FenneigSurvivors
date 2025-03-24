using UnityEngine;

namespace FenneigSurvivors.Scripts.Objects.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public TrailRenderer Trail { get; private set; }
    }
}
