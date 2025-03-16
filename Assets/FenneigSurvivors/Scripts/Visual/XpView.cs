using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FenneigSurvivors.Scripts.Visual
{
    public class XpView : MonoBehaviour
    {
        [SerializeField] private Image _xpBarFill;
        [SerializeField] private TMP_Text _xpText;
        
        private const string XP_FORMAT = "XP : ";

        public void SetXp(int currentXp, int maxXp)
        {
            _xpBarFill.fillAmount = (float)currentXp / maxXp;
            _xpText.text = $"{XP_FORMAT}{currentXp} / {maxXp}";
        }
    }
}
