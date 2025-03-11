using FenneigSurvivors.FenneigSurvivors.Scripts.Visual;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Objects
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _mesh;
        [field: SerializeField] public HpBarView HpBarView { get; private set; }
        private bool _currentHitState = true;
        
        public void SwitchHitState()
        {
            _currentHitState = !_currentHitState;
            _mesh.enabled = _currentHitState;
        }
        
        public void ResetHitState()
        {
            _mesh.enabled = true;
            _currentHitState = true;
        }
    }
}
