using LOGIC.Level;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Scripts.UI
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] private Text _levelText;

        [Inject] private LevelService _levelService;
        
        #region MONO

        private void Awake()
        {
            _levelService.EventOnLevelComplete += UpdateLevelText;
            UpdateLevelText();
        }

        #endregion

        private void UpdateLevelText() => _levelText.text = $"Level {_levelService.CurrentLevel}";
    }
}