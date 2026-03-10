using Core.Common.MVVM;
using Core.GrowthSystem.HeroStatsUpgradeSystem;
using R3;
using Scripts.GameEntity.DataInstance;
using UI.Base;
using UI.HeroMenu.AdditionalViews;
using UI.HeroMenu.ViewModels;
using UnityEngine;

namespace UI.HeroMenu.Views
{
    public class SelectedHeroInfoView : SimpleUIItem, IView
    {
        private readonly CompositeDisposable _disposables = new();

        [SerializeField] private HeroDetailUIView _detailInfo;
        [SerializeField] private HeroStatsView _statsView;

        private HeroBarracksViewModel _barracksViewModel;
        private RecruitHerosViewModel _recruitViewModel;
        private HeroStatsUpgradeController _heroStatsUpgradeController;

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void BindViewModel(IViewModel viewModel)
        {
            _barracksViewModel = viewModel as HeroBarracksViewModel;
            _barracksViewModel.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);
        }

        public void BindViewModel(RecruitHerosViewModel viewModel)
        {
            _recruitViewModel = viewModel;
            _recruitViewModel.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);
            _recruitViewModel.Refreshed.Subscribe(_ => HandleRefreshedSignal()).AddTo(_disposables);
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            _heroStatsUpgradeController ??= new(selectedHero, _statsView);
            _heroStatsUpgradeController.SetHero(selectedHero);

            _statsView.Setup(selectedHero);
            _statsView.Init(_heroStatsUpgradeController);

            _detailInfo.Setup(selectedHero);
        }

        private void HandleRefreshedSignal()
        {
            _statsView.Clear();
            _detailInfo.Clear();
        }
    }
}