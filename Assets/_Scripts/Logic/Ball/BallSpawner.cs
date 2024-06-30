using System.Collections.Generic;
using LOGIC.BALL;
using LOGIC.Money;
using LOGIC.UPGRADES;
using NaughtyAttributes;
using UnityEngine;
using VContainer;

public class BallSpawner : MonoBehaviour
{
    #region CONSTANTS

    private readonly Vector2 DEFAULT_MOVE_ZONE = new Vector2(-11.5f, 11.5f);

    #endregion
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private BallSpawnerMachine _spawnerMachine;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _targetTime = 2;
    [SerializeField] private int _targetPrize;

    private float _timer;
    private BallSpawnerMover _ballSpawnerMover;
    private LevelCollectableMoney _levelCollectableMoney;
    private List<BallSpawnerMachine> _spawnerMachines = new List<BallSpawnerMachine>();

    [Inject]
    private void Construct(IObjectResolver objectResolver)
    {
        _levelCollectableMoney = objectResolver.Resolve<LevelCollectableMoney>();
    }
    
    #region MONO

    private void Start()
    {

    }
    
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _targetTime)
        {
            for (int i = 0; i < _spawnerMachines.Count; i++)
            {
                BallSpawnerMachine machine = _spawnerMachines[i];
                Ball ball = Instantiate(_ballPrefab, machine.BallSpawnPosition, Quaternion.identity);
                ball.LevelCollectableMoney = _levelCollectableMoney;
                ball.BallPrizeValue = _targetPrize;
                machine.BallSpawnerMover.DoRandomMoving(_targetTime);
            }
            
            _timer = 0;
        }
    }

    #endregion

    public void SetSpawnSpeed(int upgradeLevel)
    {
        switch (upgradeLevel)
        {
            case 1:
                _targetTime = 1.5f;
                break;
            case 2:
                _targetTime = 1.25f;
                break;
            case 3:
                _targetTime = 1f;
                break;
            case 4:
                _targetTime = 0.85f;
                break;
            case 5:
                _targetTime = 0.75f;
                break;
            case 6:
                _targetTime = 0.6f;
                break;
            case 7:
                _targetTime = 0.5f;
                break;
            case 8:
                _targetTime = 0.4f;
                break;
            default:
                _targetTime = 2;
                break;
        }
    }

    [Button()]
    public void SpawnBallMachine()
    {
        BallSpawnerMachine spawnerMachine = Instantiate(_spawnerMachine, _spawnPoint.position, Quaternion.identity);
        _spawnerMachines.Add(spawnerMachine);
        
        UpdateZones();
    }
    
    private void UpdateZones()
    {
        float start = DEFAULT_MOVE_ZONE.x;
        float end = DEFAULT_MOVE_ZONE.y;
        int zonesCount = _spawnerMachines.Count;
        float padding = 4.0f; // Adjust this value based on your needs
        float totalPadding = padding * (zonesCount - 1);
        float availableSpace = (end - start) - totalPadding;
        float step = availableSpace / zonesCount;

        for (int i = 0; i < zonesCount; i++)
        {
            float zoneStart = start + i * (step + padding);
            float zoneEnd = zoneStart + step;
            Vector2 newZone = new Vector2(zoneStart, zoneEnd);
            _spawnerMachines[i].BallSpawnerMover.UpdateZone(newZone);
        }
    }
}
