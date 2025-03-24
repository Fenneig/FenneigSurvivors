using FenneigSurvivors.Scripts.Objects.Weapons;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        public int Damage;
        public float Speed;
        public float LifeTime;
        public float AutoAttackCooldown;
        [Header("Pool info")]
        public Projectile Prefab;
        public int StartInitialCount;
    }
}
