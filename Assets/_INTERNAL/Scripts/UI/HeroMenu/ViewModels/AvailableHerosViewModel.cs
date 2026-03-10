using Core.Common.MVVM;
using GameEntity.DataInstance.Main;
using R3;
using Scripts.GameEntity.DataInstance;
using SO.Configs;
using System.Collections.Generic;
using System.Linq;
using UI.HeroMenu.AdditionalViews;
using UI.HeroMenu.Models;
using UnityEngine;
using Utils;

namespace UI.HeroMenu.ViewModels
{
    public class AvailableHerosViewModel : IViewModel
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly Subject<List<HeroItemView>> _requestedHerosSignal = new();
        private readonly Subject<HeroInstance> _selectedHeroSignal = new();
        private readonly Subject<HeroItemView> _addedNewHeroSignal = new();

        private readonly List<HeroItemView> _availableHeros = new();
        private readonly Dictionary<HeroInstance, HeroItemView> _availableHerosDict = new();

        private HeroBarracksModel _model;
        private ImperiumInstancesHolder _availableHerosModel;
        private ObjectPool<HeroItemView> _herosPool;

        public Observable<List<HeroItemView>> RequestedHeros => _requestedHerosSignal.AsObservable();
        public Observable<HeroInstance> SelectedHero => _selectedHeroSignal.AsObservable();
        public Observable<HeroItemView> AddedNewHero => _addedNewHeroSignal.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as HeroBarracksModel;

            _model.AvailableHerosRequest.Subscribe(HandleRequestedHeros).AddTo(_disposables);
            _model.HeroListUpdated.Subscribe(AddNewItemToList).AddTo(_disposables);
        }

        public void BindObjectPool(ObjectPool<HeroItemView> herosPool)
        {
            _herosPool = herosPool;
        }

        public void Dispose()
        {
            Clear();
            _disposables.Clear();
        }

        public void RequestAvailableHerosFromModel()
        {
            _model.RequestAvailableHeros();
        }

        public void RefreshSubscribes()
        {
            foreach (HeroItemView itemView in _availableHeros)
                itemView.InitializeButton();

            _requestedHerosSignal.OnNext(_availableHeros);
        }

        public IReadOnlyList<HeroItemView> RequestHerosList() => _availableHeros.AsReadOnly();

        public void SetSelectedHero(HeroInstance selectedHero)
        {
            _model.SelectHero(selectedHero);
            _selectedHeroSignal.OnNext(selectedHero);
        }

        private void CreateList(List<HeroInstance> availableheros)
        {
            Clear();

            IEnumerable<HeroInstance> freeHeros = availableheros.Where(h => !h.IsBusy);
            foreach (HeroInstance hero in freeHeros)
            {
                HeroItemView itemView = _herosPool.GetFreeElement();

                itemView.Setup(hero);
                itemView.InitializeButton();

                if (!_availableHeros.Contains(itemView))
                {
                    _availableHeros.Add(itemView);
                    _availableHerosDict[itemView.Hero] = itemView;
                }
            }
        }

        private void Clear()
        {
            foreach (HeroItemView item in _availableHeros)
            {
                if(item != null)
                {
                    item.Clear();
                    _herosPool.ReturnToPool(item);
                }
            }

            _availableHeros.Clear();
            _availableHerosDict.Clear();
        }

        private void HandleRequestedHeros(List<HeroInstance> availableHeros)
        {
            if (_availableHeros.Count == 0)
            {
                CreateList(availableHeros);
            }

            _requestedHerosSignal.OnNext(_availableHeros);
        }

        private void AddNewItemToList(HeroInstance newHero)
        {
            HeroItemView item = _herosPool.AddItemToPool();

            item.Setup(newHero);

            _availableHeros.Add(item);
            _availableHerosDict[newHero] = item;

            _addedNewHeroSignal.OnNext(item);
        }
    }
}