using Core.Common.MVVM;
using R3;
using Scripts.GameEntity.DataInstance;
using SO.Configs;
using System.Collections.Generic;
using UI.Base;
using UI.HeroMenu.AdditionalViews;
using UI.HeroMenu.ViewModels;
using UnityEngine;
using Utils;

namespace UI.HeroMenu.Views
{
    public class AvailableHeroListView : UIListBase, IView
    {
        [SerializeField] private Transform _contentContainer;
        [SerializeField] private bool _isEnchantedView;

        private readonly CompositeDisposable _disposables = new();
        private readonly List<HeroItemView> _availableHeros = new();

        private AvailableHerosViewModel _viewModel;

        private HeroItemView _prefab;
        private ObjectPool<HeroItemView> _herosPool;

        private void OnEnable() => _viewModel?.RefreshSubscribes();

        public void BindViewModel(IViewModel viewModel)
        {
            CreatePool();

            _viewModel = viewModel as AvailableHerosViewModel;
            _viewModel.BindObjectPool(_herosPool);
            _viewModel.RequestedHeros.Subscribe(HandleRequestedHeros).AddTo(_disposables);
            _viewModel.AddedNewHero.Subscribe(HandleAddedNewHero).AddTo(_disposables);
            _viewModel.RequestAvailableHerosFromModel();
        }

        public void Clear()
        {
            _disposables.Clear();
            _viewModel = null;
        }

        private void CreatePool()
        {
            var herosPoolConfig = Resources.Load<AvailableHerosPoolConfig>("Configs/HeroMenu/AvailableHerosPoolConfig");
            if (_isEnchantedView)
                _prefab = herosPoolConfig.EnchantedItemViewPrefab;
            else
                _prefab = herosPoolConfig.SimpleItemViewPrefab;

            _herosPool ??= new(_prefab, herosPoolConfig.InitCount, _contentContainer)
            {
                AutoExpand = true
            };
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            Debug.Log(_viewModel != null);
            _viewModel.SetSelectedHero(selectedHero);
        }

        private void HandleAddedNewHero(HeroItemView newHero) => _availableHeros.Add(newHero);

        private void HandleRequestedHeros(List<HeroItemView> heroItems)
        {
            _availableHeros.Clear();
            foreach (HeroItemView heroItem in heroItems)
            {
                heroItem.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);
                _availableHeros.Add(heroItem);
            }
        }
    }
}