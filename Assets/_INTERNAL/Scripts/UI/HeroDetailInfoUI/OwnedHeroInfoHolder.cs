using Core.GrowthSystem.HeroStatsUpgradeSystem;
using Entry.Mono;
using GameEntity.DataInstance.Main;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using UI.Base;
using UnityEngine;

namespace UI.HeroDetailInfoUI
{
    public class OwnedHeroInfoHolder : SimpleUIItem
    {
        [SerializeField] private OwnedHeroListController _heroListUI;
        [SerializeField] private HeroDetailUI _detailInfo;
        [SerializeField] private HeroStatsView _statsView;

        private HeroStatsUpgradeController _heroStatsUpgradeController;
        private ImperiumInstancesHolder _instanceHolder;

        private void OnEnable()
        {
            _instanceHolder = GameWorldStateMono.Instance.GameWorldState.ImperiumState.InstanceHolder;
            ForceUpdateHeroList();
            _heroListUI.OnHeroSelected += HandleSelectedHero;
        }

        private void OnDisable()
        {
            _heroListUI.OnHeroSelected -= HandleSelectedHero;
        }

        private void Awake()
        {
            
        }

        public void ForceUpdateHeroList()
        {
            _heroListUI.Initialize(_instanceHolder);
            if (!_heroListUI.gameObject.activeSelf)
                _heroListUI.Show();
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