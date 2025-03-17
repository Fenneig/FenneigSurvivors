using System.Collections.Generic;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        public List<MeleeEnemyStats> MeleeEnemyStats;
    }
}
