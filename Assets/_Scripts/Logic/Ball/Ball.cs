using LOGIC.Money;
using UnityEngine;

namespace LOGIC.BALL
{
    public class Ball : MonoBehaviour
    {
        public float BallPrizeValue;
        public LevelCollectableMoney LevelCollectableMoney;

        public void OnPrize(float multiplier)
        {
            BallPrizeValue *= multiplier;
            LevelCollectableMoney.AddMoney(BallPrizeValue);
        }
    }
}