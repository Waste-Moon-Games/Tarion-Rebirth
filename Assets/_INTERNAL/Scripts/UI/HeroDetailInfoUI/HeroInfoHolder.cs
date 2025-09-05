using Core.GrowthSystem.HeroStatsUpgradeSystem;
using Entry.Mono;
using GameEntity.DataInstance.Main;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using UI.Base;
using UnityEngine;

namespace UI.HeroDetailInfoUI
{
    public class HeroInfoHolder : SimpleUIItem
    {
        [SerializeField] private HeroListController _heroListUI;
        [SerializeField] private HeroDetailUI _detailInfo;
        [SerializeField] private HeroStatsView _statsView;

        private HeroStatsUpgradeController _heroStatsUpgradeController;
        private ImperiumInstancesHolder _instanceHolder;

        private void OnEnable()
        {
            _heroListUI.OnHeroSelected += HandleSelectedHero;
        }

        private void OnDisable()
        {
            _heroListUI.OnHeroSelected -= HandleSelectedHero;
        }

        private void Awake()
        {
            _instanceHolder = GameWorldStateMono.Instance.GameWorldState.ImperiumState.InstanceHolder;
            ForceUpdateHeroList();
        }

        public void ForceUpdateHeroList()
        {
            _heroListUI.Initialize(_instanceHolder);
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            _heroStatsUpgradeController ??= new(selectedHero, _statsView);
            _heroStatsUpgradeController.SetHero(selectedHero);

            _statsView.Setup(selectedHero);
            _statsView.Init(_heroStatsUpgradeController);

            _detailInfo.Setup(selectedHero);
        }
    }
}