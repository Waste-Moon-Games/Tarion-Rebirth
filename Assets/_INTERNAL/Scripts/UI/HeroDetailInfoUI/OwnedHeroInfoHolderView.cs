using Core.GrowthSystem.HeroStatsUpgradeSystem;
using GameEntity.DataInstance.Main;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using UI.Base;
using UnityEngine;

namespace UI.HeroDetailInfoUI
{
    public class OwnedHeroInfoHolderView : SimpleUIItem
    {
        [SerializeField] private OwnedHeroListController _heroListUI;
        [SerializeField] private HeroDetailUIView _detailInfo;
        [SerializeField] private HeroStatsView _statsView;

        private HeroStatsUpgradeController _heroStatsUpgradeController;

        private void OnEnable()
        {
            _heroListUI.OnHeroSelected += HandleSelectedHero;
            _statsView.Clear();
        }

        private void OnDisable()
        {
            _heroListUI.OnHeroSelected -= HandleSelectedHero;
        }

        public void Init(ImperiumInstancesHolder instancesHolder)
        {
            ForceUpdateHeroList(instancesHolder);
        }

        private void ForceUpdateHeroList(ImperiumInstancesHolder instancesHolder)
        {
            _heroListUI.Initialize(instancesHolder);

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