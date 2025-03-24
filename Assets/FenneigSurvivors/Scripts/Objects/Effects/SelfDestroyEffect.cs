using UnityEngine;
using UnityEngine.VFX;

namespace FenneigSurvivors.Scripts.Objects.Effects
{
    public class SelfDestroyEffect : MonoBehaviour
    {
        [SerializeField] private VisualEffect effect;
        private bool _effectPlayed = false;
        
        private void Update()
        {
            if (effect.aliveParticleCount > 0 && !_effectPlayed)
            {
                _effectPlayed = true;
            }

            if (effect.aliveParticleCount == 0 && _effectPlayed)
            {
                Destroy(gameObject);
            }
        }
    }
}
