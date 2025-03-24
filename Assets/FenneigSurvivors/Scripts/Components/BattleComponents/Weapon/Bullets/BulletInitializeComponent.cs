using UnityEngine;

namespace FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets
{
    public struct BulletInitializeComponent : IInitializeComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
    }
}
