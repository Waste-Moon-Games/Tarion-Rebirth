using Core.Common.MVVM;
using GameEntity.DataInstance.Main;
using R3;
using Scripts.GameEntity.DataInstance;
using SO.Configs;
using System.Collections.Generic;
using System.Linq;
using UI.HeroMenu.Models;
using UI.HeroMenu.Views;
using UnityEngine;
using Utils;

namespace UI.HeroMenu.ViewModels
{
    public class AvailableHerosViewModel : IViewModel
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly Subject<List<HeroItemView>> _requestedHerosSignal = new();

        private readonly List<HeroItemView> _availableHeros = new();
        private readonly Dictionary<HeroInstance, HeroItemView> _availableHerosDict = new();

        private ObjectPool<HeroItemView> _herosPool;
        private HeroBarracksModel _model;

        public Observable<List<HeroItemView>> RequestedHeros => _requestedHerosSignal.AsObservable();

        public AvailableHerosViewModel(AvailableHerosPoolConfig config)
        {
            Transform container = config.Container.Find("Viewport").Find("Content");
            Debug.Log($"Pool container: {container.GetType().Name}");

            _herosPool = new(config.HeroItemViewPrefab, config.InitCount, container)
            {
                AutoExpand = true
            };
        }

        public void BindModel(IModel model)
        {
            _model = model as HeroBarracksModel;

            _model.AvailableHerosRequest.Subscribe(HandleRequestedHeros).AddTo(_disposables);
            _model.HeroListUpdated.Subscribe(AddNewItemToList).AddTo(_disposables);
            _model.RequestAvailableHeros();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void RequestAvailableHeros()
        {
            _model.RequestAvailableHeros();
        }

        public void SetSelectedHero(HeroInstance selectedHero) => _model.SelectHero(selectedHero);

        private void CreateList(ImperiumInstancesHolder instancesHolder)
        {
            IEnumerable<HeroInstance> freeHeros = instancesHolder.Heros.Where(h => !h.IsBusy);
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

        private void HandleRequestedHeros(ImperiumInstancesHolder instancesHolder)
        {
            if (_availableHeros.Count == 0)
            {
                CreateList(instancesHolder);
            }

            _requestedHerosSignal.OnNext(_availableHeros);
        }

        private void AddNewItemToList(HeroInstance newHero)
        {
            HeroItemView item = _herosPool.AddItemToPool();

            item.Setup(newHero);

            _availableHeros.Add(item);
            _availableHerosDict[newHero] = item;
        }
    }
}