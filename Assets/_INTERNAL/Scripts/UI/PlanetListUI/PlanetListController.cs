using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.UI.PlanetListUI
{
    public class PlanetListController : UIListBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private PlanetViewItem _planetItemPrefab;

        private readonly List<PlanetViewItem> _planetItems = new();

        private ImperiumInstancesHolder _instanceHolder;

        public event Action<PlanetInstance> OnPlanetSelected;

        public void Initialize(ImperiumInstancesHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;

            CreatePlanetList();
            SubscribeOnItemsEvent();
        }

        private void OnEnable()
        {
            SubscribeOnItemsEvent();

            _instanceHolder.OnPlanetsListUpdated += RefreshItemList;
        }

        private void OnDisable()
        {
            UnsubscribeFromItemsEvent();

            _instanceHolder.OnPlanetsListUpdated -= RefreshItemList;
        }

        private void CreatePlanetList()
        {
            if (_planetItems.Count == _instanceHolder.Planets.Count)
                return;

            foreach (Transform child in _contentParent)
                Destroy(child.gameObject);

            _planetItems.Clear();

            for (int i = 0; i < _instanceHolder.Planets.Count; i++)
            {
                var itemGO = Instantiate(_planetItemPrefab, _contentParent);
                var item = itemGO.GetComponent<PlanetViewItem>();

                item.Setup(_instanceHolder.Planets[i]);
                item.InitializeButton();

                _planetItems.Add(item);
            }
        }

        private void SubscribeOnItemsEvent()
        {
            foreach (PlanetViewItem item in _planetItems)
            {
                item.OnPlanetSelected += HandleSelectedPlanet;
                item.InitializeButton();
            }
        }

        private void UnsubscribeFromItemsEvent()
        {
            foreach (PlanetViewItem planet in _planetItems)
            {
                planet.OnPlanetSelected -= HandleSelectedPlanet;
            }
        }

        private void RefreshItemList(PlanetInstance newPlanet)
        {
            PlanetViewItem item = Instantiate(_planetItemPrefab, _contentParent);

            item.Setup(newPlanet);
            item.OnPlanetSelected += HandleSelectedPlanet;

            _planetItems.Add(item);
        }

        private void HandleSelectedPlanet(PlanetInstance planetInstance)
        {
            OnPlanetSelected?.Invoke(planetInstance);
        }
    }
}