using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Fireballs
{
    public struct FireballInitializeComponent : IInitializeComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
    }
}
