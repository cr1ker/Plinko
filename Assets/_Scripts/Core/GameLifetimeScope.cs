using LOGIC.GameStateMachine;
using LOGIC.Level;
using LOGIC.Money;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BallSpawnerMover _ballSpawnerMover;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameStateMachine>();
        
        builder.RegisterComponent(_ballSpawner);
        builder.RegisterComponent(_ballSpawnerMover);

        builder.Register<LevelCollectableMoney>(Lifetime.Singleton).As<ITickable>().AsSelf();
        builder.Register<LevelService>(Lifetime.Singleton);
    }
}
