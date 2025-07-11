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

        [SerializeField] private List<PlanetViewItem> _planetItems = new();

        private InstanceHolder _instanceHolder;

        public event Action<PlanetInstance> OnPlanetSelected;

        public void Initialize(InstanceHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
            GeneratePlanetList();
        }

        private void OnDisable()
        {
            foreach (var itemPlanet in _planetItems)
            {
                itemPlanet.OnPlanetSelected -= HandleSelectedPlanet;
            }
        }

        private void GeneratePlanetList()
        {
            foreach (var planet in _instanceHolder.Planets)
            {
                var itemGO = Instantiate(_planetItemPrefab, _contentParent);
                var item = itemGO.GetComponent<PlanetViewItem>();

                _planetItems.Add(item);

                item.Setup(planet);

                item.OnPlanetSelected += HandleSelectedPlanet;
            }
        }

        private void HandleSelectedPlanet(PlanetInstance planetInstance)
        {
            OnPlanetSelected?.Invoke(planetInstance);
        }
    }
}