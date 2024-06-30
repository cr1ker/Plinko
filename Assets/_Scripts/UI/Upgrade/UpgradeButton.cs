using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _priceText;

        public Button Button => _button;

        public void UpdateButton(string targetText)
        {
            _priceText.text = targetText;
        }        
    }
}

