using UnityEngine;

namespace FenneigSurvivors.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] public int BulletDamage;
        [SerializeField] public float BulletsSpeed;
        [SerializeField] public float BulletLifeTime;
        [SerializeField] public float PlayerAutoAttackCooldown;
    }
}
