using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawnerMover : MonoBehaviour
{
    [SerializeField] private float _rangeXPosition;
    [SerializeField] private int _movingSpeed;
    [SerializeField] private Transform _ballspawner;

    #region MONO



    #endregion

    public void DoRandomMoving(float intervalTime)
    {
        Tween.PositionX(_ballspawner, GetRandomXPosition(), intervalTime, Ease.InOutBack);
    }
    
    private float GetRandomXPosition() => Random.Range(-_rangeXPosition, _rangeXPosition);
}
