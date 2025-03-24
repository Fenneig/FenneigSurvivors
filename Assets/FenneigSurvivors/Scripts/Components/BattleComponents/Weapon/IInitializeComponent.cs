using UnityEngine;

namespace FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets
{
    public interface IInitializeComponent
    {
        Vector3 Position { get; set; }
        Vector3 Direction { get; set; }
    }
}
