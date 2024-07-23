using LOGIC.Money;
using UnityEngine;
using VContainer;

namespace LOGIC.GameStateMachine
{
    public class GameLoadingState : IGameState
    {
        #region CONSTANTS

        private const float LOADING_TIME = 2.5f;

        #endregion

        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelCollectableMoney _levelCollectableMoney;
        private readonly LevelMoney _levelMoney;
        private readonly WinObserver _winObserver;
        private bool _isFirstLoading = true;
        
        public GameLoadingState(GameStateMachine gameStateMachine, IObjectResolver objectResolver)
        {
            _gameStateMachine = gameStateMachine;
            _levelCollectableMoney = objectResolver.Resolve<LevelCollectableMoney>();
            _levelMoney = objectResolver.Resolve<LevelMoney>();
            _winObserver = objectResolver.Resolve<WinObserver>();
            Application.targetFrameRate = 60;
        }
        
        public async void Enter()
        {
            _levelCollectableMoney.OnLoadingLevel();
            _levelMoney.OnLoadingLevel();
            _winObserver.OnLoadingLevel();
            _gameStateMachine.SetPlayingState();
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
