using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LOGIC.Money
{
    public class LevelMoney : IInitializable, ITickable, IMoneyHandler
    {
        #region CONSTANTS

        private const string LEVEL_MONEY = nameof(LEVEL_MONEY);
        private const float WAIT_SAVE_INTERVAL = 0.5f;

        #endregion
        public readonly ReactiveProperty<float> Money = new ReactiveProperty<float>();
        
        private float _saverTimerTime;

        private readonly LevelCollectableMoney _levelCollectableMoney;
        
        public LevelMoney(LevelCollectableMoney levelCollectableMoney)
        {
            _levelCollectableMoney = levelCollectableMoney;
        }
        
        #region VCONTAINER

        public void Initialize()
        {
            _levelCollectableMoney.EventOnMoneyAdd += AddMoney;

            if (!SaveManager.HasData(LEVEL_MONEY))
            {
                SaveManager.SaveData(LEVEL_MONEY, 1000f);
                Money.Value = 1000f;
            }
        }
        
        public void Tick()
        {
            _saverTimerTime += Time.deltaTime;

            var isSaveAvailable = _saverTimerTime >= WAIT_SAVE_INTERVAL; 
            if (isSaveAvailable)
            {
                SaveManager.SaveData(LEVEL_MONEY, Money.CurrentValue);
                _saverTimerTime = 0;
            }
        }

        #endregion
        
        public void AddMoney(float addValue)
        {
            Money.Value += addValue;
        }

        public bool TrySubtractMoney(int subtractValue)
        {
            if (IsEnoughMoney(subtractValue))
            {
                Money.Value -= subtractValue;
                return true;
            }

            return false;
        }

        public bool IsEnoughMoney(int subtractValue) => Money.CurrentValue >= subtractValue;

        #region CALLBACKS

        public void OnLoadingLevel()
        {
            SaveManager.GetData(LEVEL_MONEY, out float money);
            Money.Value = money;
        }

        public void OnPlayingLevel()
        {
            return;
        }

        public void OnCompleteLevel()
        {
            SaveManager.SaveData(LEVEL_MONEY, 0f);
            Money.Value = 0f;
        }

        #endregion
    }
}