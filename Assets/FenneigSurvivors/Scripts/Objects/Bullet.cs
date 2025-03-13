using UnityEngine;

namespace FenneigSurvivors.Scripts.Objects
{
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField] public TrailRenderer Trail { get; private set; }
    }
}
