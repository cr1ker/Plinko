using LOGIC;
using LOGIC.BOARD;
using LOGIC.GameStateMachine;
using LOGIC.Level;
using LOGIC.Money;
using LOGIC.UPGRADES;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BallSpawnerMover _ballSpawnerMover;
    [SerializeField] private UpgradeButtons _upgradeButtons;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlinkoBoardGenerator _plinkoBoardGenerator;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_gameManager);
        builder.RegisterComponent(_uiManager);
        builder.RegisterEntryPoint<GameStateMachine>().AsSelf();
        
        builder.RegisterComponent(_ballSpawner).AsSelf();
        builder.RegisterComponent(_upgradeButtons);
        builder.RegisterComponent(_ballSpawnerMover);
        builder.RegisterComponent(_plinkoBoardGenerator).AsSelf();

        builder.Register<WinObserver>(Lifetime.Singleton).AsSelf();
        builder.Register<LevelCollectableMoney>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<LevelMoney>(Lifetime.Singleton).As<ITickable>().As<IInitializable>().AsSelf();
        builder.Register<LevelService>(Lifetime.Singleton).AsSelf();
        builder.Register<Upgrades>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }
}
