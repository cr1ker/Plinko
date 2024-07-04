using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UI;
using UnityEngine;
using VContainer;

public class Tutorial : MonoBehaviour
{
    #region CONSTANTS

    private const string TUTORIAL_1 = nameof(TUTORIAL_1);
    private const string TUTORIAL_2 = nameof(TUTORIAL_2);

    #endregion
    [SerializeField] private GameObject _tutorial1;
    [SerializeField] private GameObject _tutorial2;

    [Inject] private UpgradeButtons _upgradeButtons;
    
    private GameObject _currentOpenedTutorial;
    
    
    
    #region MONO

    private void Awake()
    {
        _tutorial1.transform.localScale = Vector3.zero;
        _tutorial2.transform.localScale = Vector3.zero;

        if (!SaveManager.HasData(TUTORIAL_1))
        {
            SaveManager.SaveData(TUTORIAL_1, false);
            SaveManager.SaveData(TUTORIAL_2, false);
        }

        SaveManager.GetData(TUTORIAL_1, out bool isTutorial1Completed);
        SaveManager.GetData(TUTORIAL_1, out bool isTutorial2Completed);

        if (!isTutorial1Completed)
        {
            _upgradeButtons.BallSpawnMachineUpgradeButton.Button.onClick.AddListener(OnBallSpawnMachineClick);
            OpenTutorial(_tutorial1);
        }

        if (!isTutorial2Completed)
        {
            _upgradeButtons.SpeedSpawnUpgradeButton.Button.onClick.AddListener(OnSpeedBoostClick);

            if (isTutorial1Completed)
            {
                OpenTutorial(_tutorial2);
            }
        }
    }

    #endregion

    public void OpenTutorial(GameObject tutorial)
    {
        if (_currentOpenedTutorial != null)
        {
            CloseTutorial(_currentOpenedTutorial);
        }

        tutorial.SetActive(true);
        _currentOpenedTutorial = tutorial;
        
        Tween.Scale(tutorial.transform, Vector3.one, 0.5f, Ease.OutBack);
    }

    private void CloseTutorial(GameObject tutorial)
    {
        Tween.Scale(tutorial.transform, Vector3.zero, 0.5f, Ease.InBack).OnComplete(() =>
        {
            tutorial.SetActive(false);
        });
    }

    private void CompleteTutorial1()
    {
        SaveManager.SaveData(TUTORIAL_1, true);
    }
    
    private void CompleteTutorial2()
    {
        SaveManager.SaveData(TUTORIAL_2, true);
    }

    private void OnBallSpawnMachineClick()
    {
        CompleteTutorial1();
        OpenTutorial(_tutorial2);
        _upgradeButtons.BallSpawnMachineUpgradeButton.Button.onClick.RemoveListener(OnBallSpawnMachineClick);
    }
    
    private void OnSpeedBoostClick()
    {
        CompleteTutorial2();
        CloseTutorial(_tutorial2);
        _upgradeButtons.SpeedSpawnUpgradeButton.Button.onClick.RemoveListener(OnSpeedBoostClick);
    }
}
