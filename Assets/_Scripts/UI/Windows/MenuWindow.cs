using LOGIC;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class MenuWindow : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        #region MONO

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        #endregion

        #region CALLBACKS

        #region UICALLBACKS

        private void OnPlayButtonClick()
        {
            _gameManager.SetPlayStatus();
            gameObject.SetActive(false);
        }

        #endregion

        #endregion
    }
}