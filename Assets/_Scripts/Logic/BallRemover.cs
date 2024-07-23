using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOGIC
{
    public class BallRemover : MonoBehaviour
    {

        #region CALLBACKS

        private void OnCollisionEnter(Collision other)
        {
            Destroy(other.gameObject);
        }

        #endregion
    }
}
