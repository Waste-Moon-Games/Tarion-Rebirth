using Core.Common.MVVM;
using R3;
using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;
using UI.Base;
using UI.HeroMenu.AdditionalViews;
using UI.HeroMenu.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HeroMenu.Views
{
    public class AvailableHerosToRecruitListView : UIListBase, IView
    {
        [SerializeField] private Button _refreshButton;

        private readonly CompositeDisposable _disposables = new();
        private readonly List<HeroItemView> _heroItems = new();

        private AvailableHerosToRecruitViewModel _viewModel;

        private void OnEnable()
        {
            if (_refreshButton != null)
                _refreshButton.onClick.AddListener(HandleRefreshButtonClick);

            _viewModel?.RefreshSubscribes();
        }

        private void OnDisable()
        {
            if (_refreshButton != null)
                _refreshButton.onClick.RemoveListener(HandleRefreshButtonClick);
        }

        private void OnDestroy() => _disposables.Dispose();

        public void BindViewModel(IViewModel viewModel)
        {
            _viewModel = viewModel as AvailableHerosToRecruitViewModel;
            _viewModel.RequestedNewHeros.Subscribe(HandleRequestedNewHeros).AddTo(_disposables);
            _viewModel.RequestNewHeros();

            SubscribeOnItemsEvent();
        }

        private void SubscribeOnItemsEvent()
        {
            foreach (HeroItemView itemView in _heroItems)
                itemView.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);
        }

        private void HandleRequestedNewHeros(List<HeroItemView> heroItemViews)
        {
            Clear();

            foreach (HeroItemView newHeroItem in heroItemViews)
            {
                newHeroItem.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);
                _heroItems.Add(newHeroItem);
            }
        }

        private void Clear() => _heroItems.Clear();
        private void HandleSelectedHero(HeroInstance selectedhero) => _viewModel.SetSelectedHero(selectedhero);
        private void HandleRefreshButtonClick() => _viewModel.RequestNewHeros();
    }
}