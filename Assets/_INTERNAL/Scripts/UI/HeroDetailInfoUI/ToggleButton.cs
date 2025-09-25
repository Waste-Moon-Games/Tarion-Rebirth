using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HeroDetailInfoUI
{
    public class ToggleButton : MonoBehaviour
    {
        private Button _toggleButton;

        public event Action OnButtonClicked;

        private void Awake()
        {
            if(_toggleButton == null)
                _toggleButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _toggleButton.onClick.AddListener(ButtonClicked);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            OnButtonClicked?.Invoke();
        }
    }
}