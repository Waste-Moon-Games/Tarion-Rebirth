using Scripts.GameEntity.DataInstance;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils.Formatter;

namespace Mono.UI.HeroListUI
{
    public class HeroItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _heroName;
        [SerializeField] private TextMeshProUGUI _heroPower;
        [SerializeField] private TextMeshProUGUI _heroLevel;

        private Button _selectButton;

        private HeroInstance _heroInstance;
        private NumberFormatter _formatter;

        private UnityAction _clickHandler;

        public event Action<HeroInstance> OnHeroSelected;

        private void OnDisable()
        {
            if(_clickHandler != null)
                _selectButton.onClick.RemoveListener(_clickHandler);

            _heroInstance.OnPowerChanged -= HandleChangedPower;
        }

        public void Setup(HeroInstance heroInstance)
        {
            _formatter ??= new();

            _heroInstance = heroInstance;
            _heroInstance.OnPowerChanged += HandleChangedPower;

            _heroName.text = _heroInstance.RuntimeData.Name;
            _heroPower.text = $"Мощь: {_formatter.FormatNumber(_heroInstance.HeroPower)}";
            _heroLevel.text = $"Уровень: {_heroInstance.HeroLevel}";

            _selectButton = GetComponent<Button>();

            if (_clickHandler != null)
                _selectButton.onClick.RemoveListener(_clickHandler);

            _clickHandler = () => OnHeroSelected?.Invoke(heroInstance);
            _selectButton.onClick.AddListener(_clickHandler);
        }

        private void HandleChangedPower(float currentPower)
        {
            _heroPower.text = $"Мощь: {_formatter.FormatNumber(currentPower)}";
        }
    }
}