using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI 
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private LoadingWindow _loadingWindow;
        //[SerializeField] private MainMenuWindow _mainMenuWindow;

        public async UniTask ActivateLoadingWindow(float time)
        {
            _loadingWindow.gameObject.SetActive(true);

            await UniTask.WaitForSeconds(time);
            
            _loadingWindow.gameObject.SetActive(false);
        }
    }
}
