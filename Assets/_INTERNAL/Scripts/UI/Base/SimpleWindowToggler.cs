using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class SimpleWindowToggler : MonoBehaviour
    {
        [Header("Target window")]
        [SerializeField] private GameObject _window;

        [Space(10), Header("Toggle button")]
        [SerializeField] protected Button _toggleButton;

        private void OnEnable()
        {
            _toggleButton.onClick.AddListener(ToggleWindow);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(ToggleWindow);
        }

        private void ToggleWindow()
        {
            if (_window != null && _toggleButton != null)
                _window.SetActive(!_window.activeSelf);
        }
    }
}