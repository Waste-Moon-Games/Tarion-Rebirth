using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class SimpleWindowToggler : MonoBehaviour
    {
        [Header("Target window")]
        [SerializeField] private SimpleUIItem _window;

        [Space(10), Header("Toggle button")]
        [SerializeField] protected Button _toggleButton;

        private void OnEnable()
        {
            _toggleButton.onClick.AddListener(OpenWindow);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(OpenWindow);
        }

        private void OpenWindow()
        {
            if (_window != null && _toggleButton != null)
                _window.Show();
        }
    }
}