using UnityEngine;
using VContainer;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _targetTime = 2;


    private float _timer;
    private BallSpawnerMover _ballSpawnerMover;

    [Inject]
    private void Construct(IObjectResolver objectResolver)
    {
        _ballSpawnerMover = objectResolver.Resolve<BallSpawnerMover>();
    }
    
    #region MONO

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _targetTime)
        {
            Instantiate(_ballPrefab, _spawnPoint.position, Quaternion.identity);

            _timer = 0;
            _ballSpawnerMover.DoRandomMoving(_targetTime);
        }
    }

    #endregion
}
