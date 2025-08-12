using Core.GrowthSystem.HeroStatsUpgradeSystem;
using GameEntity.DataInstance.Main;
using GameEntity.Unit.Data;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using System;
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
        private InstanceHolder _instanceHolder;

        private void OnEnable()
        {
            _heroListUI.OnHeroSelected += HandleSelectedHero;
        }

        private void OnDisable()
        {
            _heroListUI.OnHeroSelected -= HandleSelectedHero;
        }

        private void Start()
        {
            ForceUpdateHeroList();
        }

        public void SetInstanceHolder(InstanceHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
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

            Debug.Log($"{selectedHero.RuntimeData.Name} have {selectedHero.GetCurrentSkillPoints()} skill points");
        }
    }
}