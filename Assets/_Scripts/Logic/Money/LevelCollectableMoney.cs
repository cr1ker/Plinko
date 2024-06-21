using System;
using LOGIC.Level;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LOGIC.Money
{
    public class LevelCollectableMoney : ITickable, IMoneyHandler
    {
        #region CONSTANTS

        private const string LEVEL_COLLECTABLE_MONEY = nameof(LEVEL_COLLECTABLE_MONEY);
        private const float WAIT_SAVE_INTERVAL = 0.5f;
        private const int START_COLLECTABLE_TARGET = 5000;
        private const int MAX_LEVEL = 50; 
        
        #endregion
        #region EVENTS

        public event Action<float> EventOnMoneyAdd; 

        #endregion
        public readonly ReactiveProperty<float> Money = new ReactiveProperty<float>();
        public readonly ReactiveProperty<int> TargetLevelMoney = new ReactiveProperty<int>();
        [Header("DEPENDENCIES")]
        [Inject] private LevelService _levelService;

        private float _saverTimerTime;
        private bool _isCountingActive;

        #region MONO

        public void Tick()
        {
            _saverTimerTime += Time.deltaTime;

            var isSaveAvailable = _saverTimerTime >= WAIT_SAVE_INTERVAL; 
            if (isSaveAvailable)
            {
                SaveManager.SaveData(LEVEL_COLLECTABLE_MONEY, Money.Value);
                _saverTimerTime = 0;
            }
        }
        
        #endregion
        
        public void AddMoney(float addValue)
        {
            var targetLevelMoney = TargetLevelMoney.CurrentValue;

            if (Money.Value < targetLevelMoney)
            {
                var futureValue = Money.CurrentValue + Mathf.Abs(addValue);

                if (futureValue <= targetLevelMoney)
                {
                    Money.Value = futureValue;
                    EventOnMoneyAdd?.Invoke(addValue);
                }
                else
                {
                    Money.Value = targetLevelMoney;
                    var a = futureValue - targetLevelMoney;
                    addValue -= a;
                    EventOnMoneyAdd?.Invoke(addValue);
                }
            }
            
            var isTargetMoneyReached = Money.Value >= targetLevelMoney;
            if (isTargetMoneyReached)
            {
                Money.Value = targetLevelMoney;

                if (_isCountingActive)
                {
                    _levelService.CompleteLevel();
                }
            }
        }

        public bool TrySubtractMoney(int subtractValue)
        {
            return false;
        }

        public void SetTargetLevelMoneyByCurrentLevel()
        {
            var targetLevelMoney = GetTargetLevelMoneyByCurrentLevel();
            TargetLevelMoney.Value = targetLevelMoney;
        }

        public int GetPersentOfTarget() => ((int)Money.CurrentValue * 100) / TargetLevelMoney.CurrentValue;
        
        private int GetTargetLevelMoneyByCurrentLevel()
        {
            var currentLevel = _levelService.CurrentLevel;

            var newTargetLevelMoney = currentLevel <= MAX_LEVEL ? 
                START_COLLECTABLE_TARGET * currentLevel 
                : 
                START_COLLECTABLE_TARGET * MAX_LEVEL;
            
            return newTargetLevelMoney;
        }

        #region CALLBACKS

        public void OnLoadingLevel()
        {
            _isCountingActive = true;
            SaveManager.GetData(LEVEL_COLLECTABLE_MONEY, out int money);
            Money.Value = money;
            
            SetTargetLevelMoneyByCurrentLevel();
        }

        public void OnPlayingLevel()
        {
            return;
        }

        public void OnCompleteLevel()
        {
            _isCountingActive = false;
            SaveManager.SaveData(LEVEL_COLLECTABLE_MONEY, 0);
            Money.Value = 0;
        }
        
        #endregion
    }
}

