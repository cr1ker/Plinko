
using LOGIC.BOARD;
using LOGIC.Money;
using LOGIC.UPGRADES;
using VContainer;

namespace LOGIC.GameStateMachine
{
    public class GamePlayingState : IGameState
    {
        private readonly PlinkoBoardGenerator _plinkoBoardGenerator;
        private readonly BallSpawner _ballSpawner;
        private readonly Upgrades _upgrades;
        private readonly LevelCollectableMoney _levelCollectableMoney;
        
        public GamePlayingState(GameStateMachine gameStateMachine, IObjectResolver objectResolver)
        {
            _plinkoBoardGenerator = objectResolver.Resolve<PlinkoBoardGenerator>();
            _ballSpawner = objectResolver.Resolve<BallSpawner>();
            _upgrades = objectResolver.Resolve<Upgrades>();
            _levelCollectableMoney = objectResolver.Resolve<LevelCollectableMoney>();
        }
        
        public void Enter()
        {
            _plinkoBoardGenerator.GenerateRandomPlinkoBoard();
            _ballSpawner.IsSpawnAvailable = true;
            _upgrades.OnPlayingLevel();
            _levelCollectableMoney.OnPlayingLevel();
        }

        public void Update()
        {
        }

        public void Exit()
        {
            _ballSpawner.IsSpawnAvailable = false;
            _plinkoBoardGenerator.DestroyBoard();
            _ballSpawner.DestroyBallMachines();
        }
    }
}
