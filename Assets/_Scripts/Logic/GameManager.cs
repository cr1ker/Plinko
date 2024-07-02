using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace LOGIC
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _board;
        [Inject] private UIManager _uiManager;

        #region MONO

        private void Awake()
        {
            _uiManager.ActivateLoadingWindow(4).Forget();
            _board.SetActive(false);
        }

        #endregion
        
        public void SetPlayStatus()
        {
            _board.SetActive(true);
        }

        public void SetMenuStatus()
        {
            _board.SetActive(false);
        }
    }
}
