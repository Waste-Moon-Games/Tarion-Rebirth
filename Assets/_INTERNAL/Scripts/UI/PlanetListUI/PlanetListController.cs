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

        private List<PlanetViewItem> _planetItems = new();

        private InstanceHolder _instanceHolder;

        public event Action<PlanetInstance> OnPlanetSelected;

        private void OnDestroy()
        {
            foreach (PlanetViewItem itemPlanet in _planetItems)
            {
                itemPlanet.OnPlanetSelected -= HandleSelectedPlanet;
            }
        }

        public void Initialize(InstanceHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
            GeneratePlanetList();
        }

        private void GeneratePlanetList()
        {
            foreach (var planet in _instanceHolder.Planets)
            {
                if (_planetItems.Count != _instanceHolder.Planets.Count)
                {
                    var itemGO = Instantiate(_planetItemPrefab, _contentParent);
                    var item = itemGO.GetComponent<PlanetViewItem>();

                    _planetItems.Add(item);

                    item.Setup(planet);

                    item.OnPlanetSelected += HandleSelectedPlanet;
                }
            }
        }

        private void HandleSelectedPlanet(PlanetInstance planetInstance)
        {
            OnPlanetSelected?.Invoke(planetInstance);
        }
    }
}