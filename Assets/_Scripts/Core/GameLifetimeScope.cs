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
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BallSpawnerMover _ballSpawnerMover;
    [SerializeField] private UpgradeButtons _upgradeButtons;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameStateMachine>();
        
        builder.RegisterComponent(_ballSpawner);
        builder.RegisterComponent(_upgradeButtons);
        builder.RegisterComponent(_ballSpawnerMover);

        builder.Register<LevelCollectableMoney>(Lifetime.Singleton).As<ITickable>().AsSelf();
        builder.Register<LevelMoney>(Lifetime.Singleton).As<ITickable>().As<IInitializable>().AsSelf();
        builder.Register<LevelService>(Lifetime.Singleton);
        builder.Register<Upgrades>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }
}
