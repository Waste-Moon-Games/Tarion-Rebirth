using GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlanetsMap
{
    public class GalaxyMapUI : MonoBehaviour
    {
        [Header("Map buttons")]
        [SerializeField] private Button _refreshMapButton;
        [SerializeField] private Button _addInTargetListButton;

        [Space(10), Header("Generated planets")]
        [SerializeField] private List<PlanetInstance> _generatedPlanets = new();

        public event Action OnMapRefreshed;

        private void OnEnable()
        {
            if (_refreshMapButton != null)
                _refreshMapButton.onClick.AddListener(RefreshMap);
            if (_addInTargetListButton != null)
                _addInTargetListButton.onClick.AddListener(AddInTargetList);
        }

        private void OnDisable()
        {
            if (_refreshMapButton != null)
                _refreshMapButton.onClick.RemoveListener(RefreshMap);
            if (_addInTargetListButton != null)
                _addInTargetListButton.onClick.RemoveListener(AddInTargetList);
        }

        private void Awake()
        {
            if (_generatedPlanets.Count == 0)
                OnMapRefreshed?.Invoke();
        }

        public void GetNewPlanets(List<PlanetInstance> planets)
        {
            _generatedPlanets.Clear();

            _generatedPlanets.AddRange(planets);
        }

        private void RefreshMap()
        {
            OnMapRefreshed?.Invoke();
        }

        private void AddInTargetList()
        {

        }
    }
}