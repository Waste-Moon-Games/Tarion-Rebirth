using GameEntity.DataInstance.Main;
using Scripts.GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.UI.HeroListUI
{
    public class HeroListController : MonoBehaviour
    {
        [SerializeField] private Transform _contentparent;
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
            foreach (var hero in _instanceHolder.Heros)
            {
                var itemGO = Instantiate(_heroItemPrefab, _contentparent);
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