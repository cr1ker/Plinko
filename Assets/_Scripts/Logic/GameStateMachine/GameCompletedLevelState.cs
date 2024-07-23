
using LOGIC.Level;
using LOGIC.Money;
using LOGIC.UPGRADES;
using UI;
using UnityEngine;
using VContainer;

namespace LOGIC.GameStateMachine
{
    public class GameCompletedLevelState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelCollectableMoney _levelCollectableMoney;
        private readonly LevelMoney _levelMoney;
        private readonly Upgrades _upgrades;
        private readonly UpgradeButtons _upgradeButtons;
        private readonly LevelService _levelService;
        
        public GameCompletedLevelState(GameStateMachine gameStateMachine, IObjectResolver objectResolver)
        {
            _gameStateMachine = gameStateMachine;
            _levelCollectableMoney = objectResolver.Resolve<LevelCollectableMoney>();
            _levelMoney = objectResolver.Resolve<LevelMoney>();
            _upgrades = objectResolver.Resolve<Upgrades>();
            _upgradeButtons = objectResolver.Resolve<UpgradeButtons>();
            _levelService = objectResolver.Resolve<LevelService>();
        }
        
        public async void Enter()
        {
            _levelCollectableMoney.OnCompleteLevel();
            _levelMoney.OnCompleteLevel();
            _upgrades.OnCompleteLevel();
            _upgradeButtons.OnCompleteLevel();
            _levelService.CompleteLevel();
            
            Debug.Log("completed level state");
            
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