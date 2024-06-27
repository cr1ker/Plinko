using UnityEngine;

namespace LOGIC.BALL
{
    public class BallSpawnerMachine : MonoBehaviour
    {
        [SerializeField] private BallSpawnerMover _ballSpawnerMover;
        [SerializeField] private Transform _ballSpawnPoint;
        
        public BallSpawnerMover BallSpawnerMover => _ballSpawnerMover;
        public Vector3 BallSpawnPosition => _ballSpawnPoint.position;
    }
}