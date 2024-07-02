using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingWindow : MonoBehaviour
    {
        #region CONSTANTS

        private const string LOADING = "Loading";

        #endregion
        [SerializeField] private Text _loadingText;

        private float _timer;
        
        #region MONO

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer <= 1)
            {
                _loadingText.text = $"{LOADING}.";
            } else if (_timer <= 2)
            {
                _loadingText.text = $"{LOADING}..";
            } else if (_timer <= 3)
            {
                _loadingText.text = $"{LOADING}...";
            }
            else if (_timer <= 4)
            {
                _timer = 0;
            }
        }

        #endregion
    }
}