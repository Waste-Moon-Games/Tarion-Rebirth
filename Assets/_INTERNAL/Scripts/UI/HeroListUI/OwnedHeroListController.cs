using GameEntity.DataInstance.Main;
using Scripts.GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Mono.UI.HeroListUI
{
    public class OwnedHeroListController : UIListBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private HeroItemView _heroItemPrefab;
        [SerializeField] private bool _autoExpand;

        private readonly List<HeroItemView> _heroItems = new();
        private readonly Dictionary<HeroInstance, HeroItemView> _heroItemsDict = new();

        private ImperiumInstancesHolder _instanceHolder;

        private ObjectPool<HeroItemView> _herosPool;

        public event Action<HeroInstance> OnHeroSelected;

        public void Initialize(ImperiumInstancesHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;

            _herosPool ??= new(_heroItemPrefab, instanceHolder.Heros.Count, _contentParent)
            {
                AutoExpand = _autoExpand
            };

            CreateHeroList(_instanceHolder);
            SubscribeOnItemsEvent();
        }

        private void OnEnable()
        {
            SubscribeOnItemsEvent();

            RefreshItemList();
            _instanceHolder.OnHerosListUpdated += AddNewItemToList;
        }

        private void OnDisable()
        {
            UnsubscribeFromItemsEvent();

            _instanceHolder.OnHerosListUpdated -= AddNewItemToList;

            Clear();
        }

        public void AddNewItemToList(HeroInstance newHero)
        {
            HeroItemView item = _herosPool?.AddItemToPool();

            item.Setup(newHero);
            item.OnHeroSelected += HandleSelectedHero;

            _heroItems.Add(item);
            _heroItemsDict[newHero] = item;
        }

        public void RemoveItemFromList(HeroInstance hero)
        {
            if (_heroItemsDict.TryGetValue(hero, out HeroItemView item))
            {
                _heroItems.Remove(item);
                _heroItemsDict.Remove(hero);
                item.Clear();
            }
        }

        private void CreateHeroList(ImperiumInstancesHolder holder)
        {
            Clear();

            IEnumerable<HeroInstance> freeHeros = _instanceHolder.Heros.Where(h => !h.IsBusy);
            foreach (HeroInstance hero in freeHeros)
            {
                HeroItemView item = _herosPool?.GetFreeElement();

                item.Setup(hero);
                item.InitializeButton();

                if (!_heroItems.Contains(item) && !_heroItemsDict.ContainsKey(item.Hero))
                {
                    _heroItems.Add(item);
                    _heroItemsDict[item.Hero] = item;
                }
            }
        }

        private void SubscribeOnItemsEvent()
        {
            foreach (HeroItemView item in _heroItems)
            {
                item.OnHeroSelected += HandleSelectedHero;
                item.InitializeButton();
            }
        }

        private void UnsubscribeFromItemsEvent()
        {
            foreach (HeroItemView hero in _heroItems)
            {
                hero.OnHeroSelected -= HandleSelectedHero;
            }
        }

        private void Clear()
        {
            foreach (HeroItemView item in _heroItems)
            {
                if (item != null)
                {
                    item.Clear();
                    _herosPool?.ReturnToPool(item);
                }
            }

            _heroItems.Clear();
            _heroItemsDict.Clear();
        }

        private void RefreshItemList()
        {
            foreach (HeroInstance hero in _instanceHolder.Heros)
            {
                if (_heroItemsDict.TryGetValue(hero, out HeroItemView item))
                {
                    item.SelectButton.interactable = !hero.IsBusy;
                }
            }
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            OnHeroSelected?.Invoke(selectedHero);
        }
    }
}