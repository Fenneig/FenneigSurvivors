using UnityEngine;
using UnityEngine.VFX;

namespace FenneigSurvivors.Scripts.Objects.Weapons
{
    public class FireballVFX : MonoBehaviour
    {
        [SerializeField] private VisualEffect _effect;

        public void PlayEffect(float diameter)
        {
            _effect.SetFloat("Diameter", diameter * 1.15f);
            _effect.Play();
        }
    }
}
