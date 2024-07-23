using LOGIC.Money;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LOGIC
{
    public class WinObserver
    {
        [Inject] private LevelCollectableMoney _levelCollectableMoney;
        [Inject] private GameStateMachine.GameStateMachine _gameStateMachine;

        private CompositeDisposable _disposable;
        
        #region CALLBACKS

        public void OnLoadingLevel()
        {
        }
        
        public void OnCompletedLevel()
        {
            
        }

        #endregion
    }
}