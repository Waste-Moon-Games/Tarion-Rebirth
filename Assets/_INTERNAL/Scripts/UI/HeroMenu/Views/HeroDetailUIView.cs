using Core.Common.MVVM;
using GameEntity.Unit.Data;
using R3;
using Scripts.GameEntity.DataInstance;
using TMPro;
using UI.HeroMenu.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Utils.Formatter;

namespace UI.HeroMenu.Views
{
    public class HeroDetailUIView : MonoBehaviour, IView
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly NumberFormatter _formatter = new();

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

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void OnEnable()
        {
            Clear();
        }

        private void OnDisable()
        {
            _heroArtPanel.SetActive(false);
        }

        public void BindViewModel(IViewModel viewModel) { }

        public void Setup(HeroInstance heroInstance)
        {
            HeroRuntimeData heroData = heroInstance.RuntimeData;

            _selectedHero = heroInstance;
            heroInstance.PowerChanged.Subscribe(HandleChangedPower).AddTo(_disposables);
            heroInstance.ExpChanged.Subscribe(HandleChangedExp).AddTo(_disposables);

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

        public void Clear()
        {
            _selectedHero = null;

            _name.text = "Имя:";
            _description.text = "Описание";
            _power.text = "Мощь:";
            _rank.text = "Ранг:";
            _level.text = "Уровень:";

            _heroArtPanel.SetActive(false);
        }

        private void SetupMainText(HeroInstance heroInstance, HeroRuntimeData heroData)
        {
            _name.text = $"{heroData.Name}";
            _description.text = $"{heroData.Description}";
            _power.text = $"Мощь: {_formatter.FormatNumber(heroInstance.HeroPower)}";
            SetupRankText(heroData);

            SetupGrowthText(heroInstance, heroData);
        }

        private void SetupRankText(HeroRuntimeData heroData)
        {
            Rank rank = heroData.Rank;
            _rank.text = rank switch
            {
                Rank.Recruit => "Рекрут",
                Rank.Veteran => "Закалённый",
                Rank.Elite => "Ветеран",
                Rank.Champion => "Чемпион Тариона",
                Rank.Guardian => "Рыцарь Тариона",
                _ => "Ранг: нет",
            };
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
            if (_selectedHero == null)
                return;

            string formatedCurrentExp = _formatter
                .FormatNumber(_selectedHero.RuntimeData.Experience);
            string formatedNextLevelExp = _formatter
                .FormatNumber(_selectedHero.GetExperienceToNextLevel());

            _expInfo.text = $"{formatedCurrentExp}/{formatedNextLevelExp}";

            if (!_levelProgression)
                return;

            _levelProgression.fillAmount = _selectedHero.GetExperienceProgress();
        }
    }
}