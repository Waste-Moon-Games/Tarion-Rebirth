using Core.Common.MVVM;
using R3;
using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;
using UI.Base;
using UI.HeroMenu.ViewModels;
using UnityEngine;

namespace UI.HeroMenu.Views
{
    // todo переписать на MVVM - OwnedHeroListView - View, AvailableHerosViewModel - ViewModel, HeroBarracksModel - Model
    public class AvailableHeroListView : UIListBase, IView
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly List<HeroItemView> _availableHeros = new();

        private AvailableHerosViewModel _viewModel;

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void BindViewModel(IViewModel viewModel)
        {
            _viewModel = viewModel as AvailableHerosViewModel;
            _viewModel.RequestedHeros.Subscribe(HandleRequestedHeros).AddTo(_disposables);
            _viewModel.RequestAvailableHeros();

            SubscribeOnItemsEvent();
        }

        private void SubscribeOnItemsEvent()
        {
            foreach (HeroItemView itemView in _availableHeros)
                itemView.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            _viewModel.SetSelectedHero(selectedHero);
        }

        private void HandleRequestedHeros(List<HeroItemView> heroItems)
        {
            foreach (var heroItem in heroItems)
            {
                _availableHeros.Add(heroItem);
                Debug.Log($"Hero name: {heroItem.Hero.RuntimeData.Name}");
            }
        }
    }
}