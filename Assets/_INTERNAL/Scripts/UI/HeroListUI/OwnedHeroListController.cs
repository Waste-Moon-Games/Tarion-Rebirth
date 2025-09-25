using GameEntity.DataInstance.Main;
using Scripts.GameEntity.DataInstance;
using System;
using System.Collections.Generic;
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

            _herosPool = new(_heroItemPrefab, instanceHolder.Heros.Count, _contentParent)
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

            Debug.Log($"Hero added: {newHero.RuntimeData.Name}");
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
            if (_heroItems.Count == holder.Heros.Count)
                return;

            _heroItems.Clear();

            for (int i = 0; i < holder.Heros.Count; i++)
            {
                if (holder.Heros[i].IsBusy)
                    continue;

                HeroItemView item = _herosPool?.GetFreeElement();

                item.Setup(holder.Heros[i]);
                item.InitializeButton();

                _heroItems.Add(item);
                _heroItemsDict[holder.Heros[i]] = item;
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
            foreach (var item in _herosPool)
            {
                _herosPool.ReturnToPool(item);
            }
        }

        private void RefreshItemList()
        {
            foreach (HeroInstance hero in _instanceHolder.Heros)
            {
                if (_heroItemsDict.TryGetValue(hero, out HeroItemView item))
                {
                    if (hero.IsBusy)
                    {
                        item.SelectButton.interactable = false;
                        continue;
                    }
                    else
                    {
                        item.SelectButton.interactable = true;
                    }
                }
            }
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            OnHeroSelected?.Invoke(selectedHero);
        }
    }
}