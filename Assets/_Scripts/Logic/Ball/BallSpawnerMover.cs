using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawnerMover : MonoBehaviour
{
    private Vector2 _moveableZone;
    
    public void DoRandomMoving(float intervalTime)
    {
        Tween.PositionX(transform, GetRandomXPosition(), intervalTime, Ease.InOutBack);
    }

    public void UpdateZone(Vector2 newZone) => _moveableZone = newZone;
    
    private float GetRandomXPosition() => Random.Range(_moveableZone.x, _moveableZone.y);
}