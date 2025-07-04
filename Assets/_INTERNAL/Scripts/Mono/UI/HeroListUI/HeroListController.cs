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

        [SerializeField] private List<HeroItemView> _heroItems = new();

        private InstanceHolder _instanceHolder;

        public event Action<HeroInstance> OnHeroSelected;

        public void Initialize(InstanceHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
            GenerateHeroList();
        }

        private void OnDisable()
        {
            foreach (var heroItem in _heroItems)
            {
                heroItem.OnHeroSelected -= HandleSelectedHero;
            }
        }

        private void GenerateHeroList()
        {
            foreach (Transform child in _contentParent)
            {
                Destroy(child.gameObject);
            }
            _heroItems.Clear();

            foreach (var hero in _instanceHolder.Heros)
            {
                var itemGO = Instantiate(_heroItemPrefab, _contentParent);
                var item = itemGO.GetComponent<HeroItemView>();

                _heroItems.Add(item);

                item.Setup(hero);

                item.OnHeroSelected += HandleSelectedHero;
            }
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            OnHeroSelected?.Invoke(selectedHero);
        }
    }
}