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

        [Space(10), Header("Generated planets")]
        [SerializeField] private List<PlanetMapView> _generatedPlanets = new();

        [Space(10), Header("Selected planet UI")]
        [SerializeField] private SelectedPlanetUI _selectedPlanetUI;

        public event Action OnMapRefreshed;
        public event Action<PlanetInstance> OnPlanetAdded;

        private void OnEnable()
        {
            if (_refreshMapButton != null)
                _refreshMapButton.onClick.AddListener(RefreshMap);

            _selectedPlanetUI.OnPlanetAdded += HandleAddedPlanet;
        }

        private void OnDisable()
        {
            if (_refreshMapButton != null)
                _refreshMapButton.onClick.RemoveListener(RefreshMap);

            _selectedPlanetUI.OnPlanetAdded -= HandleAddedPlanet;
        }

        private void Awake()
        {
            if (_generatedPlanets.Count == 0)
                OnMapRefreshed?.Invoke();
        }

        public void GetNewPlanets(List<PlanetMapView> planets)
        {
            _generatedPlanets.Clear();

            foreach (PlanetMapView view in planets)
            {
                view.OnPlanetSelected -= HandleSelectedPlanet;
                view.OnPlanetSelected += HandleSelectedPlanet;
                _generatedPlanets.Add(view);
            }
        }

        private void RefreshMap()
        {
            OnMapRefreshed?.Invoke();
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            _selectedPlanetUI.Show();
            _selectedPlanetUI.Setup(selectedPlanet);
        }

        private void HandleAddedPlanet(PlanetInstance addedPlanet)
        {
            OnPlanetAdded?.Invoke(addedPlanet);
        }
    }
}