using Core.GameStates;
using GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Mono.UI.PlanetListUI
{
    public class TargetPlanetsListController : UIListBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private PlanetViewItem _planetItemPrefab;
        [SerializeField] private bool _autoExpand;

        private readonly List<PlanetViewItem> _planetItems = new();
        private readonly Dictionary<PlanetInstance, PlanetViewItem> _planetItemsDict = new();

        private TargetsListState _targetList;
        private ObjectPool<PlanetViewItem> _planetsPool;

        public event Action<PlanetInstance> OnPlanetSelected;

        public void Initialize(TargetsListState targetList)
        {
            _targetList = targetList;

            _planetsPool ??= new(_planetItemPrefab, _targetList.Targets.Count, _contentParent)
            {
                AutoExpand = _autoExpand
            };

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

            _planetItems.Clear();

            for (int i = 0; i < _targetList.Targets.Count; i++)
            {
                if (_targetList.Targets[i].IsBusy)
                    continue;
;
                PlanetViewItem item = _planetsPool?.GetFreeElement();

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
            foreach (var planet in _targetList.Targets)
            {
                if (_planetItemsDict.TryGetValue(planet, out PlanetViewItem item))
                {
                    if (planet.IsBusy)
                    {
                        item.SelectButton.interactable = false;
                        continue;
                    }
                    else
                    {
                        item.SelectButton.interactable = true;
                    }
                }
            }
        }

        private void AddNewItemToList(PlanetInstance newPlanet)
        {
            PlanetViewItem item = _planetsPool?.AddItemToPool();

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
                _planetsPool.ReturnToPool(item);
            }
        }

        private void HandleSelectedPlanet(PlanetInstance planetInstance)
        {
            OnPlanetSelected?.Invoke(planetInstance);
        }
    }
}