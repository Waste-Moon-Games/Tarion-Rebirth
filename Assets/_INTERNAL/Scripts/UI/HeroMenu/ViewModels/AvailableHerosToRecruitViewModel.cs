using Core.Common.MVVM;
using R3;
using Scripts.GameEntity.DataInstance;
using SO.Configs;
using System.Collections.Generic;
using UI.HeroMenu.AdditionalViews;
using UI.HeroMenu.Models;
using UI.HeroMenu.Services;
using UnityEngine;
using Utils;

namespace UI.HeroMenu.ViewModels
{
    public class AvailableHerosToRecruitViewModel : IViewModel
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly Subject<List<HeroItemView>> _requestedHerosSignal = new();

        private readonly List<HeroItemView> _availableHerosToRecruit = new();
        private readonly Dictionary<HeroInstance, HeroItemView> _availableHerosToRecruitDict = new();
        private readonly HeroItemSpawner _spawner = new();

        private readonly List<HeroItemView> _generatedHeros = new();
        private readonly ObjectPool<HeroItemView> _herosPool;

        private RecruitHerosModel _model;

        public Observable<List<HeroItemView>> RequestedNewHeros => _requestedHerosSignal.AsObservable();

        public AvailableHerosToRecruitViewModel(Transform container)
        {
            Transform contentContainer = container.Find("Viewport").Find("Content");
            AvailableHerosToRecruitPoolConfig config = Resources.Load<AvailableHerosToRecruitPoolConfig>("Configs/HeroMenu/AvailableHerosToRecruitPoolConfig");

            _herosPool = new(config.HeroTabPrefab, config.InitCount, contentContainer)
            {
                AutoExpand = true
            };

            _spawner.SetPool(_herosPool);
        }

        public void BindModel(IModel model)
        {
            _model = model as RecruitHerosModel;
            _model.GeneratedHeros.Subscribe(HandleRequestedNewHeros).AddTo(_disposables);
        }

        public void RefreshSubscribes()
        {
            foreach (HeroItemView itemView in _generatedHeros)
                itemView.InitializeButton();
        }

        public void RequestNewHeros()
        {
            Clear();
            _model.RefreshRecruitList();
        }

        public void SetSelectedHero(HeroInstance selectedHero) => _model.SetSelectedHero(selectedHero);

        public void Dispose() => _disposables.Dispose();

        private void Clear()
        {
            foreach (HeroItemView item in _availableHerosToRecruit)
            {
                if (item != null)
                {
                    item.Clear();
                    _herosPool.ReturnToPool(item);
                }
            }

            _availableHerosToRecruit.Clear();
            _availableHerosToRecruitDict.Clear();
        }

        private void HandleRequestedNewHeros(List<HeroInstance> generatedHeros)
        {
            _generatedHeros.Clear();

            List<HeroItemView> heroItems = _spawner.SpawnHeros(generatedHeros);
            _generatedHeros.AddRange(heroItems);
            _requestedHerosSignal.OnNext(heroItems);
        }
    }
}