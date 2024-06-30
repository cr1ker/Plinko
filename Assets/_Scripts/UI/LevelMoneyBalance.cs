using LOGIC.Money;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.ProgressBar
{
    public class LevelMoneyBalance : MonoBehaviour
    {
        [SerializeField] private Text _moneyValueText;

        [Inject] private LevelMoney _levelMoney;
        
        #region MONO

        private void Awake()
        {
            _levelMoney.Money.Subscribe(money =>
            {
                _moneyValueText.text = money.ToString("F2");
            });
        }

        #endregion
    }
}