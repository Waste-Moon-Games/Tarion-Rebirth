using Core.Instances.RecruitSystem;
using Mono.UI;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.HeroListUI
{
    public class CandidateHeroListController : UIListBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private HeroItemView _heroItemPrefab;
        [SerializeField] private bool _autoExpand;

        private readonly List<HeroItemView> _heroItems = new();
        private readonly Dictionary<HeroInstance, HeroItemView> _heroItemsDict = new();

        private RecruitSystemInstance _instanceHolder;

        public IReadOnlyList<HeroItemView> HeroItems => _heroItems.AsReadOnly();

        public event Action<HeroInstance> OnHeroSelected;

        public void Initialize(RecruitSystemInstance instanceHolder, List<HeroItemView> newHeros)
        {
            _instanceHolder = instanceHolder;

            CreateHeroList(_instanceHolder, newHeros);
            SubscribeOnItemsEvent();
        }

        private void OnEnable()
        {
            SubscribeOnItemsEvent();
        }

        private void OnDisable()
        {
            UnsubscribeFromItemsEvent();
        }

        private void OnDestroy()
        {
            Clear();
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

        private void CreateHeroList(RecruitSystemInstance holder, List<HeroItemView> newHeros)
        {
            if (_heroItems.Count == holder.Heros.Count)
                return;

            Clear();

            for (int i = 0; i < holder.Heros.Count; i++)
            {
                HeroItemView item = newHeros[i];

                item.Setup(holder.Heros[i]);
                item.InitializeButton();

                _heroItems.Add(item);
                _heroItemsDict[holder.Heros[i]] = item;
            }
        }

        public void AddNewItemToList(HeroItemView newHero)
        {
            newHero.Setup(newHero.Hero);
            newHero.OnHeroSelected += HandleSelectedHero;

            _heroItems.Add(newHero);
            _heroItemsDict[newHero.Hero] = newHero;
        }

        public void RemoveItemFromList(HeroInstance hero)
        {
            if (_heroItemsDict.TryGetValue(hero, out HeroItemView item))
            {
                _heroItems.Remove(item);
                _heroItemsDict.Remove(hero);

                item.OnHeroSelected -= HandleSelectedHero;
                item.Clear();
            }
        }

        public void Clear()
        {
            UnsubscribeFromItemsEvent();

            _heroItems.Clear();
            _heroItemsDict.Clear();
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            OnHeroSelected?.Invoke(selectedHero);
        }
    }
}