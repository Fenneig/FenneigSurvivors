using System;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Configs
{
    [Serializable]
    public class MeleeEnemyStats
    {
        [SerializeField] public float PhaseTime;
        [SerializeField] public int EnemyHealth;
        [SerializeField] public float EnemySpeed;
        [SerializeField] public int MaxEnemiesOnScene;
        [SerializeField] public int EnemiesSpawnsPerSpawn;
        [SerializeField] public float SpawnCooldownDuration;
        [SerializeField] public int EnemyAttackDamage;
        [SerializeField] public float EnemyAttackDistance;
        [SerializeField] public Material EnemyMaterial;
    }
}
