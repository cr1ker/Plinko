using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BallSpawnerMover _ballSpawnerMover;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_ballSpawner);
        builder.RegisterComponent(_ballSpawnerMover);
    }
}
