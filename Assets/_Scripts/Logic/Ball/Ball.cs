using System;
using CORE.AUDIO;
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

        #region CALLBACKS

        private void OnCollisionEnter(Collision other)
        {
            AudioService.Singleton.PlayAudioOnce(AudioTypes.ContactBallSound, 1, 1.15f);
        }

        #endregion
    }
}