using UnityEngine;

namespace FenneigSurvivors.Scripts
{
    [CreateAssetMenu(fileName = "Config", menuName = "FenneigSurvivors/Config")]
    public class Config : ScriptableObject
    {
        [field: SerializeField, Header("Player settings")] public int PlayerHealth { get; set; }
        [field: SerializeField] public int BulletDamage { get; set; }
        [field: SerializeField] public float BulletsSpeed { get; set; }
        [field: SerializeField] public float PlayerSpeed { get; set; }
        [field: SerializeField] public float BulletLifeTime { get; set; }
        [field: SerializeField] public float PlayerAutoAttackCooldown { get; set; }
        [field: SerializeField] public float PlayerInvulnerableCooldown { get; set; }
        
        [field: SerializeField, Header("Enemy settings")] public int EnemyHealth { get; set; }
        [field: SerializeField] public float EnemySpeed { get; set; }
        [field: SerializeField] public int MaxEnemiesOnScene { get; set; }
        [field: SerializeField] public float HitEffectDuration { get; set; }
        [field: SerializeField] public float HitSwitchDuration { get; set; }
        [field: SerializeField] public float SpawnCooldownDuration { get; set; }
        [field: SerializeField] public float EnemyAttackDistance { get; set; }
        [field: SerializeField] public int EnemyAttackDamage { get; set; }
    }
}
