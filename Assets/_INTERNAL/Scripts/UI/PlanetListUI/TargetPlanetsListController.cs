using Core.GameStates;
using GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.UI.PlanetListUI
{
    public class TargetPlanetsListController : UIListBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private PlanetViewItem _planetItemPrefab;

        private readonly List<PlanetViewItem> _planetItems = new();
        private readonly Dictionary<PlanetInstance, PlanetViewItem> _planetItemsDict = new();

        private TargetsListState _targetList;

        public event Action<PlanetInstance> OnPlanetSelected;

        public void Initialize(TargetsListState targetList)
        {
            _targetList = targetList;

            CreatePlanetList();
            SubscribeOnItemsEvent();
        }

        private void OnEnable()
        {
            SubscribeOnItemsEvent();
            RefreshItemList();

            _targetList.OnTargetAdded += AddNewItemToList;
            _targetList.OnTargetRemoved += RemoveItemFromList;
        }

        private void OnDisable()
        {
            UnsubscribeFromItemsEvent();

            _targetList.OnTargetAdded -= AddNewItemToList;
            _targetList.OnTargetRemoved -= RemoveItemFromList;
        }

        private void CreatePlanetList()
        {
            if (_planetItems.Count == _targetList.Targets.Count)
                return;

            foreach (Transform child in _contentParent)
                Destroy(child.gameObject);

            _planetItems.Clear();

            for (int i = 0; i < _targetList.Targets.Count; i++)
            {
                var itemGO = Instantiate(_planetItemPrefab, _contentParent);
                var item = itemGO.GetComponent<PlanetViewItem>();

                item.Setup(_targetList.Targets[i]);
                item.InitializeButton();

                _planetItems.Add(item);
                _planetItemsDict[_targetList.Targets[i]] = item;
            }
        }

        private void SubscribeOnItemsEvent()
        {
            foreach (PlanetViewItem planet in _planetItems)
            {
                planet.OnPlanetSelected += HandleSelectedPlanet;
                planet.InitializeButton();
            }
        }

        private void UnsubscribeFromItemsEvent()
        {
            foreach (PlanetViewItem planet in _planetItems)
            {
                planet.OnPlanetSelected -= HandleSelectedPlanet;
            }
        }

        private void RefreshItemList()
        {
            for (int i = 0; i < _targetList.Targets.Count; i++)
            {
                _planetItems[i].Setup(_targetList.Targets[i]);
            }
        }

        private void AddNewItemToList(PlanetInstance newPlanet)
        {
            PlanetViewItem item = Instantiate(_planetItemPrefab, _contentParent);

            item.Setup(newPlanet);
            item.OnPlanetSelected += HandleSelectedPlanet;

            _planetItems.Add(item);
            _planetItemsDict[newPlanet] = item;
        }

        private void RemoveItemFromList(PlanetInstance capturedPlanet)
        {
            if (_planetItemsDict.TryGetValue(capturedPlanet, out PlanetViewItem item))
            {
                _planetItems.Remove(item);
                _planetItemsDict.Remove(capturedPlanet);
                Destroy(item.gameObject);
            }
        }

        private void HandleSelectedPlanet(PlanetInstance planetInstance)
        {
            OnPlanetSelected?.Invoke(planetInstance);
        }
    }
}