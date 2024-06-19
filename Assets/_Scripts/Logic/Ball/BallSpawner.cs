using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _spawnPoint;

    private float _timer;
    
    #region MONO

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 2)
        {
            Instantiate(_ballPrefab, _spawnPoint.position, Quaternion.identity);

            _timer = 0;
        }
    }

    #endregion
}
