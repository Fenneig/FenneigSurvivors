using UnityEngine;

namespace FenneigSurvivors.Scripts.Configs
{
    [CreateAssetMenu(fileName = "FireballConfig", menuName = "Configs/FireballConfig")]
    public class FireballConfig : BulletConfig
    {
        [Header("Fireball stats")]
        public float ExplosionRadius;
    }
}
