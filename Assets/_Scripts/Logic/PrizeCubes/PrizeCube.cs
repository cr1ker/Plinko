using System;
using System.Collections;
using System.Collections.Generic;
using CORE.AUDIO;
using LOGIC.BALL;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCube : MonoBehaviour
{
    [SerializeField] private float _targetMultiplyValue;
    [SerializeField] private Text _valueText;

    #region MONO

    private void Awake()
    {
        UpdateText();
    }

    #endregion

    private void UpdateText() 
        => _valueText.text = $"{_targetMultiplyValue}x";
    
    #region CALLBACKS

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            ball.OnPrize(_targetMultiplyValue);
            AudioService.Singleton.PlayAudioOnce(AudioTypes.PrizeZoneBallSound, 1, 1.05f);
            ball.gameObject.SetActive(false);
            Destroy(ball.gameObject);
            Tween.PunchLocalPosition(transform, Vector3.down, 0.5f);
        }
    }

    #endregion
}
