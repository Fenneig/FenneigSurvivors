using FenneigSurvivors.Scripts.Objects.Weapons;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Objects.Effects
{
    public class VFXSpawner : MonoBehaviour
    {
        [SerializeField] private FireballVFX _fireballVFXPrefab;
        public void Spawn(VFXType type, Vector3 position, float radius = 1)
        {
            if (type == VFXType.Fireball)
            {
                var explossion = Instantiate(_fireballVFXPrefab, position, Quaternion.identity);
                explossion.PlayEffect(radius);
            }
        }
    }
}