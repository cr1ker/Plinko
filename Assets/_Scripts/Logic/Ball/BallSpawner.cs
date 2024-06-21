using LOGIC.BALL;
using LOGIC.Money;
using UnityEngine;
using VContainer;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _targetTime = 2;
    [SerializeField] private int _targetPrize;


    private float _timer;
    private BallSpawnerMover _ballSpawnerMover;
    private LevelCollectableMoney _levelCollectableMoney;

    [Inject]
    private void Construct(IObjectResolver objectResolver)
    {
        _ballSpawnerMover = objectResolver.Resolve<BallSpawnerMover>();
        _levelCollectableMoney = objectResolver.Resolve<LevelCollectableMoney>();
    }
    
    #region MONO

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _targetTime)
        {
            Ball ball = Instantiate(_ballPrefab, _spawnPoint.position, Quaternion.identity);
            ball.LevelCollectableMoney = _levelCollectableMoney;
            ball.BallPrizeValue = _targetPrize;

            _timer = 0;
            _ballSpawnerMover.DoRandomMoving(_targetTime);
        }
    }

    #endregion
}
