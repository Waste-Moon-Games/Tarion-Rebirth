using Core.Common;
using Core.EntityDatas.Unit.Data;
using GameEntity.Unit.Data;
using Scripts.GameEntity.DataInstance;
using System;

namespace Core.GrowthSystem.HeroStatsUpgradeSystem
{
    public class HeroStatsUpgradeController
    {
        private readonly IStatsUpgradeView _view;
        private HeroInstance _heroInstace;

        private Action _onStrengthClicked;
        private Action _onDexterityClicked;
        private Action _onIntelligenceClicked;

        public HeroStatsUpgradeController(HeroInstance heroInstance, IStatsUpgradeView view)
        {
            _heroInstace = heroInstance;
            _view = view;
        }

        public void SetHero(HeroInstance selectedHero)
        {
            _heroInstace = selectedHero;
        }

        public void SubsribeOnEvents()
        {
            _onStrengthClicked = () => _heroInstace.GrowthSystem.TryIncreaseStats(HeroStatType.Strength);
            _onDexterityClicked = () => _heroInstace.GrowthSystem.TryIncreaseStats(HeroStatType.Dexterity);
            _onIntelligenceClicked = () => _heroInstace.GrowthSystem.TryIncreaseStats(HeroStatType.Intelligence);

            _view.OnStrengthClicked += _onStrengthClicked;
            _view.OnDexterityClicked += _onDexterityClicked;
            _view.OnIntelligenceClicked += _onIntelligenceClicked;

            _heroInstace.GrowthSystem.OnStatChanged += HandleChangedStat;
            _heroInstace.GrowthSystem.OnSkillPointsChanged += HandleChangedSkillPoints;
        }
        
        public void Dispose()
        {
            if(_onStrengthClicked != null)
                _view.OnStrengthClicked -= _onStrengthClicked;
            if(_onDexterityClicked != null)
                _view.OnDexterityClicked -= _onDexterityClicked;
            if(_onIntelligenceClicked != null)
                _view.OnIntelligenceClicked -= _onIntelligenceClicked;

            _heroInstace.GrowthSystem.OnStatChanged -= HandleChangedStat;
            _heroInstace.GrowthSystem.OnSkillPointsChanged -= HandleChangedSkillPoints;
        }

        private void HandleChangedStat(HeroRuntimeStats stats)
        {
            _view.SetStats(stats);
        }

        private void HandleChangedSkillPoints(int value)
        {
            _view.SetSkillPoints(value);
        }
    }
}