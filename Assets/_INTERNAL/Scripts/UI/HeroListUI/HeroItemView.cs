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
            SetupText(_heroInstance);

            _selectButton = GetComponent<Button>();

            if (_clickHandler != null)
                _selectButton.onClick.RemoveListener(_clickHandler);

            _clickHandler = () => OnHeroSelected?.Invoke(heroInstance);
            _selectButton.onClick.AddListener(_clickHandler);
        }

        private void SetupText(HeroInstance hero)
        {
            _heroName.text = hero.RuntimeData.Name;
            _heroPower.text = $"Мощь: {_formatter.FormatNumber(hero.HeroPower)}";
            _heroLevel.text = $"Уровень: {hero.HeroLevel}";
        }

        private void HandleChangedPower(float currentPower)
        {
            _heroPower.text = $"Мощь: {_formatter.FormatNumber(currentPower)}";
        }
    }
}