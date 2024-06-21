using LOGIC.Money;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.ProgressBar
{
    public class LevelCollectableMoneyProgressBar : ProgressBar
    {
        [SerializeField] private Text _moneyText;
        [Inject] private LevelCollectableMoney _levelCollectableMoney;

        protected override void OnProgressValueChange()
        {
            base.OnProgressValueChange();
            
            var currentMoneyText = ProgressValue.CurrentValue;
            var maxMoneyText = MaxValue.CurrentValue;

            _moneyText.text = $"{currentMoneyText} / {maxMoneyText}";
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _levelCollectableMoney.Money.Subscribe(currentMoney =>
            {
                ProgressValue.Value = currentMoney;
            });

            _levelCollectableMoney.TargetLevelMoney.Subscribe(currentMaxMoney =>
            {
                MaxValue.Value = currentMaxMoney;
            });
        }
    }
}