using GameEntity.Unit.Data;
using Scripts.GameEntity.DataInstance;
using TMPro;
using UnityEngine;
using Utils.Formatter;

namespace UI.HeroDetailInfoUI
{
    public class HeroDetailUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _power;
        [SerializeField] private TextMeshProUGUI _description;

        private HeroInstance _selectedHero;
        private NumberFormatter _formatter;

        private void OnDisable()
        {
            _selectedHero.OnPowerChanged -= HandleChangedPower;
        }

        public void Setup(HeroInstance heroInstance)
        {
            HeroRuntimeData heroData = heroInstance.RuntimeData;
            _formatter ??= new();

            _selectedHero = heroInstance;
            heroInstance.OnPowerChanged += HandleChangedPower;

            _name.text = $"Имя: {heroData.Name}";
            _level.text = $"Уровень: {heroData.Level}";
            _description.text = $"{heroData.Description}";
            _power.text = $"Мощь: {_formatter.FormatNumber(heroInstance.HeroPower)}";
        }

        private void HandleChangedPower(float currentPower)
        {
            _power.text = $"Мощь: {_formatter.FormatNumber(currentPower)}";
        }
    }
}