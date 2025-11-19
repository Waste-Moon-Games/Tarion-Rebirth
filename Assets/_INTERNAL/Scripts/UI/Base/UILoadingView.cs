using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class UILoadingView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Image _progressBar;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
            SetLoadingProgress(0f);
        }

        public void SetLoadingProgress(float progress)
        {
            _progressBar.fillAmount = progress;
        }
    }
}