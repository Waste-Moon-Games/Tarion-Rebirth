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
            RefreshHeroList();
        }

        private void Start()
        {
            RefreshHeroList();
        }

        private void OnDisable()
        {
            if (_heroItems.Count == _instanceHolder.Heros.Count)
            {
                foreach (HeroItemView heroItem in _heroItems)
                {
                    heroItem.OnHeroSelected -= HandleSelectedHero;
                }
            }
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

                _heroItems.Add(item);
            }
        }

        private void RefreshHeroList()
        {
            for (int i = 0; i < _instanceHolder.Heros.Count; i++)
            {
                _heroItems[i].Setup(_instanceHolder.Heros[i]);
                _heroItems[i].OnHeroSelected += HandleSelectedHero;
            }
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            OnHeroSelected?.Invoke(selectedHero);
        }
    }
}