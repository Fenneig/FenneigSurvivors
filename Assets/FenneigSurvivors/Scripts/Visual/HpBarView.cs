using UnityEngine;
using UnityEngine.UI;

namespace FenneigSurvivors.Scripts.Visual
{
    public class HpBarView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetHp(int currentHp, int maxHp)
        {
            _image.fillAmount = currentHp / (float)maxHp;
            _image.enabled = currentHp > 0;
        }
    }
}