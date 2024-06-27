using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

namespace LOGIC.BOARD
{
    public class Peg : MonoBehaviour
    {
        #region CONSTANTS

        private const float PUNCH_ANIMATION_DURATION = 0.35f;

        #endregion
        [SerializeField] private Transform _animationPeg;
        
        private void OnCollisionEnter(Collision other)
        {
            Tween.PunchScale(_animationPeg, Vector3.one * 1.2f, PUNCH_ANIMATION_DURATION, 1);
        }
    }
}
