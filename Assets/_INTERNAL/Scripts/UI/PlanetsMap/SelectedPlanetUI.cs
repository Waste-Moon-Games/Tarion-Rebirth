using GameEntity.DataInstance;
using GameEntity.Planet;
using System;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using Utils.Formatter;

namespace UI.PlanetsMap
{
    public class SelectedPlanetUI : SimpleUIItem
    {
        [Header("Planet info")]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _power;
        [SerializeField] private TextMeshProUGUI _type;

        [Space(10), Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _addToTargetListButton;

        private NumberFormatter _formatter;
        private PlanetInstance _selectedPlanet;

        public event Action<PlanetInstance> OnPlanetAdded;

        private void OnEnable()
        {
            transform.SetAsLastSibling();

            _closeButton.onClick.AddListener(Hide);
            _addToTargetListButton.onClick.AddListener(HandleAddedPlanet);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _addToTargetListButton.onClick.RemoveListener(HandleAddedPlanet);
        }

        public void Setup(PlanetInstance planet)
        {
            _formatter ??= new();

            _selectedPlanet = planet;
            _selectedPlanet.CalculatePlanetPower();

            SetupText(planet);
            SetupPlanetType(planet);
        }

        private void SetupText(PlanetInstance planet)
        {
            _name.text = $"{planet.RuntimeData.PlanetName}";
            _power.text = $"{_formatter.FormatNumber(planet.PlanetPower)}";
        }

        private void SetupPlanetType(PlanetInstance planet)
        {
            switch (planet.RuntimeData.Type)
            {
                case PlanetType.Capital:
                    _type.text = "столица";
                    break;
                case PlanetType.Industrial:
                    _type.text = "промышленная";
                    break;
                case PlanetType.Research:
                    _type.text = "научная";
                    break;
                case PlanetType.Military:
                    _type.text = "цитадель";
                    break;
                case PlanetType.Colony:
                    _type.text = "колония";
                    break;
            }
        }

        private void HandleAddedPlanet()
        {
            OnPlanetAdded?.Invoke(_selectedPlanet);
        }
    }
}