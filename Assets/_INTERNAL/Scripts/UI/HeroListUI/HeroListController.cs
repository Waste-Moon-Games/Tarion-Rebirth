using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using Scripts.GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.UI.HeroListUI
{
    public class HeroListController : UIListBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private HeroItemView _heroItemPrefab;

        private readonly List<HeroItemView> _heroItems = new();

        private ImperiumInstancesHolder _instanceHolder;

        public event Action<HeroInstance> OnHeroSelected;

        public void Initialize(ImperiumInstancesHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;

            CreateHeroList();
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
        }

        private void CreateHeroList()
        {
            if (_heroItems.Count == _instanceHolder.Heros.Count)
                return;

            foreach (Transform child in _contentParent)
                Destroy(child.gameObject);

            _heroItems.Clear();

            for (int i = 0; i < _instanceHolder.Heros.Count; i++)
            {
                var itemGO = Instantiate(_heroItemPrefab, _contentParent);
                var item = itemGO.GetComponent<HeroItemView>();

                item.Setup(_instanceHolder.Heros[i]);
                item.InitializeButton();

                _heroItems.Add(item);
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

        private void RefreshItemList()
        {
            for (int i = 0; i < _instanceHolder.Heros.Count; i++)
            {
                _heroItems[i].Setup(_instanceHolder.Heros[i]);
            }
        }

        private void AddNewItemToList(HeroInstance newHero)
        {
            HeroItemView item = Instantiate(_heroItemPrefab, _contentParent);

            item.Setup(newHero);
            item.OnHeroSelected += HandleSelectedHero;

            _heroItems.Add(item);
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            OnHeroSelected?.Invoke(selectedHero);
        }
    }
}