using GameEntity.Unit.Data;
using Scripts.GameEntity.DataInstance;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Formatter;

namespace UI.HeroDetailInfoUI
{
    public class HeroDetailUI : MonoBehaviour
    {
        [Header("Hero Info")]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _power;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _rank;

        [Space(10), Header("Hero Growth info")]
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _expInfo;
        [SerializeField] private Image _levelProgression;

        [Space(10), Header("Hero Art")]
        [SerializeField] private Image _art;
        [SerializeField] private GameObject _heroArtPanel;

        private HeroInstance _selectedHero;
        private NumberFormatter _formatter;

        private void OnDisable()
        {
            if(_selectedHero != null)
            {
                _selectedHero.OnPowerChanged -= HandleChangedPower;
                _selectedHero.OnExpChanged -= HandleChangedExp;
            }

            _heroArtPanel.SetActive(true);
        }

        public void Setup(HeroInstance heroInstance)
        {
            HeroRuntimeData heroData = heroInstance.RuntimeData;
            _formatter ??= new();

            _selectedHero = heroInstance;
            heroInstance.OnPowerChanged += HandleChangedPower;
            heroInstance.OnExpChanged += HandleChangedExp;

            SetupMainText(heroInstance, heroData);

            if (heroData.HeroArt != null)
            {
                _art.enabled = true;
                _art.sprite = heroData.HeroArt;
            }
            else
                _art.enabled = false;

            _heroArtPanel.SetActive(true);
        }

        private void SetupMainText(HeroInstance heroInstance, HeroRuntimeData heroData)
        {
            _name.text = $"Имя: {heroData.Name}";
            _description.text = $"{heroData.Description}";
            _power.text = $"Мощь: {_formatter.FormatNumber(heroInstance.HeroPower)}";
            _rank.text = $"Ранг: {heroData.Rank}";

            SetupGrowthText(heroInstance, heroData);
        }

        private void SetupGrowthText(HeroInstance heroInstance, HeroRuntimeData heroData)
        {
            _level.text = $"{heroData.Level}";

            string formatedCurrentExp = _formatter
                .FormatNumber(heroInstance.RuntimeData.Experience);
            string formatedNextLevelExp = _formatter
                .FormatNumber(heroInstance.GetExperienceToNextLevel());

            _expInfo.text = $"{formatedCurrentExp}/{formatedNextLevelExp}";

            _levelProgression.fillAmount = heroInstance.GetExperienceProgress();
        }

        private void HandleChangedPower(float currentPower)
        {
            _power.text = $"Мощь: {_formatter.FormatNumber(currentPower)}";
        }

        private void HandleChangedExp(int exp)
        {
            string formatedCurrentExp = _formatter
                .FormatNumber(_selectedHero.RuntimeData.Experience);
            string formatedNextLevelExp = _formatter
                .FormatNumber(_selectedHero.GetExperienceToNextLevel());

            _expInfo.text = $"{formatedCurrentExp}/{formatedNextLevelExp}";
            _levelProgression.fillAmount = _selectedHero.GetExperienceProgress();
        }
    }
}