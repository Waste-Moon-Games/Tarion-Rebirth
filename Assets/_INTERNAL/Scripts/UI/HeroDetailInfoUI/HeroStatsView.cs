using Core.Common;
using Core.GrowthSystem.HeroStatsUpgradeSystem;
using GameEntity.Unit.Data;
using Scripts.GameEntity.DataInstance;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.HeroDetailInfoUI
{
    public class HeroStatsView : MonoBehaviour, IStatsUpgradeView
    {
        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI _strenght;
        [SerializeField] private TextMeshProUGUI _dexterity;
        [SerializeField] private TextMeshProUGUI _intelligence;

        [Space(10), Header("Increase buttons")]
        [SerializeField] private Button _increaseSTRButton;
        [SerializeField] private Button _increaseDEXButton;
        [SerializeField] private Button _increaseINTButton;

        [Space(10), Header("Skill points text")]
        [SerializeField] private TextMeshProUGUI _avaliablePointsText;

        private HeroStatsUpgradeController _heroStatsUpgradeController;

        private UnityAction _onStrengthButtonClick;
        private UnityAction _onDexterityButtonClick;
        private UnityAction _onIntelligenceButtonClick;

        public event Action OnStrengthClicked;
        public event Action OnDexterityClicked;
        public event Action OnIntelligenceClicked;

        public void SubscribeOnButtonEvents()
        {
            _onStrengthButtonClick = () => OnStrengthClicked?.Invoke();
            _onDexterityButtonClick = () => OnDexterityClicked?.Invoke();
            _onIntelligenceButtonClick = () => OnIntelligenceClicked?.Invoke();

            _increaseSTRButton.onClick.AddListener(_onStrengthButtonClick);
            _increaseDEXButton.onClick.AddListener(_onDexterityButtonClick);
            _increaseINTButton.onClick.AddListener(_onIntelligenceButtonClick);
        }

        public void UnsubscribeFromButtonEvents()
        {
            if(_onStrengthButtonClick != null)
                _increaseSTRButton.onClick.RemoveListener(_onStrengthButtonClick);
            if(_onDexterityButtonClick != null)
                _increaseDEXButton.onClick.RemoveListener(_onDexterityButtonClick);
            if(_onIntelligenceButtonClick != null)
                _increaseINTButton.onClick.RemoveListener(_onIntelligenceButtonClick);
        }

        public void Init(HeroStatsUpgradeController controller)
        {
            if (_heroStatsUpgradeController != null)
                return;

            _heroStatsUpgradeController = controller;
            _heroStatsUpgradeController.SubsribeOnEvents();
        }

        public void Setup(HeroInstance selectedHero)
        {
            Cleanup();
            UnsubscribeFromButtonEvents();
            SubscribeOnButtonEvents();

            _strenght.text = $"STR: {selectedHero.RuntimeData.Stats.Strength}";
            _dexterity.text = $"DEX: {selectedHero.RuntimeData.Stats.Dexterity}";
            _intelligence.text = $"INT: {selectedHero.RuntimeData.Stats.Intelligence}";
            _avaliablePointsText.text = $"{selectedHero.GetCurrentSkillPoints()}";
        }

        public void SetSkillPoints(int value)
        {
            _avaliablePointsText.text = $"{value}";
        }

        public void SetStats(HeroRuntimeStats stats)
        {
            _strenght.text = $"STR: {stats.Strength}";
            _dexterity.text = $"DEX: {stats.Dexterity}";
            _intelligence.text = $"INT: {stats.Intelligence}";
        }

        private void Cleanup()
        {
            _heroStatsUpgradeController?.Dispose();
            _heroStatsUpgradeController = null;
        }
    }
}