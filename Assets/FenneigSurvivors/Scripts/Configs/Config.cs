using UnityEngine;

namespace FenneigSurvivors.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "FenneigSurvivors/Config")]
    public class Config : ScriptableObject
    {
        [SerializeField] public int PlayerHealth;
        [SerializeField] public float PlayerSpeed;
        [SerializeField] public float PlayerInvulnerableCooldown;
        [SerializeField] public float HitEffectDuration;
        [SerializeField] public float HitSwitchDuration;
    }
}
