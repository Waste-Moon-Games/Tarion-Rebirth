using GameEntity.DataInstance;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils.Formatter;

namespace Mono.UI.PlanetListUI
{
    public class PlanetViewItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _planetNameText;
        [SerializeField] private TextMeshProUGUI _planetPopulation;
        [SerializeField] private TextMeshProUGUI _planetPower;

        private Button _selectButton;

        private PlanetInstance _planetInstance;
        private NumberFormatter _formatter;

        private UnityAction _clickHandler;

        public event Action<PlanetInstance> OnPlanetSelected;

        private void OnDisable()
        {
            if (_clickHandler != null)
                _selectButton.onClick.RemoveListener(_clickHandler);

            _planetInstance.OnPowerChanged -= HandleChangedPower;
        }

        public void Setup(PlanetInstance planetInstance)
        {
            _formatter ??= new();

            _planetInstance = planetInstance;
            _planetInstance.OnPowerChanged += HandleChangedPower;

            SetupText();
        }

        private void SetupText()
        {
            _planetNameText.text = _planetInstance.RuntimeData.PlanetName;
            _planetPopulation.text = $"Популяция:{_formatter.FormatNumber(_planetInstance.RuntimeData.Population)}";
            _planetPower.text = $"Мощь: {_formatter.FormatNumber(_planetInstance.PlanetPower)}";
        }

        public void InitializeButton()
        {
            if(_selectButton == null)
                _selectButton = GetComponent<Button>();

            if (_clickHandler != null)
                _selectButton.onClick.RemoveListener(_clickHandler);

            _clickHandler = () => OnPlanetSelected?.Invoke(_planetInstance);

            _selectButton.onClick.AddListener(_clickHandler);
        }

        private void HandleChangedPower(float power)
        {
            _planetPower.text = $"Мощь: {_formatter.FormatNumber(power)}";
        }
    }
}