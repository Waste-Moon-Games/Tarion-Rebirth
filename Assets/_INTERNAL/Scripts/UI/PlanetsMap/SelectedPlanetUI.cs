using GameEntity.DataInstance;
using GameEntity.Planet;
using System;
using System.Numerics;
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
        [SerializeField] private TextMeshProUGUI _level;

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
            ClearPanel();
        }

        public void Setup(PlanetInstance planet)
        {
            _formatter ??= new();

            _selectedPlanet = planet;
            _selectedPlanet.CalculatePower();

            SetupText(planet);
            SetupPlanetType(planet);
        }

        private void SetupText(PlanetInstance planet)
        {
            _name.text = $"{planet.RuntimeData.PlanetName}";
            _power.text = $"Мощь: {_formatter.FormatNumber(planet.PlanetPower)}";
            _level.text = $"Уровень: {planet.RuntimeData.Level}";
        }

        private void ClearPanel()
        {
            _name.text = $"None";
            _power.text = $"Мощь: None";
            _level.text = $"Уровень: None";
        }

        private void SetupPlanetType(PlanetInstance planet)
        {
            switch (planet.RuntimeData.Type)
            {
                case PlanetType.Capital:
                    _type.text = "Тип: столица";
                    break;
                case PlanetType.Industrial:
                    _type.text = "Тип: промышленная";
                    break;
                case PlanetType.Research:
                    _type.text = "Тип: научная";
                    break;
                case PlanetType.Military:
                    _type.text = "Тип: цитадель";
                    break;
                case PlanetType.Colony:
                    _type.text = "Тип: колония";
                    break;
            }
        }

        private void HandleAddedPlanet()
        {
            OnPlanetAdded?.Invoke(_selectedPlanet);
        }
    }
}