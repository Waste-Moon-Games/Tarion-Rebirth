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

        private InstanceHolder _instanceHolder;

        public event Action<PlanetInstance> OnPlanetSelected;

        public void Initialize(InstanceHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;

            CreatePlanetList();
            RefreshPlanetList();
        }

        private void OnEnable()
        {
            RefreshPlanetList();
        }

        private void OnDisable()
        {
            if (_planetItems.Count == _instanceHolder.Planets.Count)
            {
                foreach (PlanetViewItem planet in _planetItems)
                {
                    planet.OnPlanetSelected -= HandleSelectedPlanet;
                }
            }
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

                _planetItems.Add(item);
            }
        }

        private void RefreshPlanetList()
        {
            for (int i = 0; i < _planetItems.Count; i++)
            {
                _planetItems[i].Setup(_instanceHolder.Planets[i]);
                _planetItems[i].OnPlanetSelected += HandleSelectedPlanet;
            }
        }

        private void HandleSelectedPlanet(PlanetInstance planetInstance)
        {
            OnPlanetSelected?.Invoke(planetInstance);
        }
    }
}