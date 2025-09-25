using Core.EntityDatas.Resource;
using GameEntity.DataInstance;
using GameEntity.Planet;
using System;
using System.Collections.Generic;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using Utils.Formatter;

namespace UI.PlanetsMap
{
    public class SelectedPlanetUI : SimpleUIItem
    {
        [Header("General info")]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _power;
        [SerializeField] private TextMeshProUGUI _type;
        [SerializeField] private TextMeshProUGUI _level;

        [Space(10), Header("Resources info")]
        [SerializeField] private TextMeshProUGUI _voidMatter;
        [SerializeField] private TextMeshProUGUI _darkEnergy;
        [SerializeField] private TextMeshProUGUI _minerals;

        [Space(10), Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _addToTargetListButton;

        private NumberFormatter _formatter;
        private PlanetInstance _selectedPlanet;

        private Dictionary<ResourceType, TextMeshProUGUI> _resourcesText;

        public event Action<PlanetInstance> OnPlanetAdded;

        private void Awake()
        {
            if (gameObject.activeSelf)
                Hide();

            _resourcesText = new()
            {
                { ResourceType.Dark_Energy, _darkEnergy },
                { ResourceType.Void_Matter, _voidMatter },
                { ResourceType.Mineral_Crystalls, _minerals }
            };
        }

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
            SetupResourcesText(planet);
        }

        private void SetupText(PlanetInstance planet)
        {
            _name.text = $"{planet.RuntimeData.PlanetName}";
            _power.text = $"Мощь: {_formatter.FormatNumber(planet.PlanetPower)}";
            _level.text = $"Уровень: {planet.RuntimeData.Level}";
        }

        private void SetupResourcesText(PlanetInstance planet)
        {
            foreach (var resource in planet.RuntimeData.Resources)
            {
                var type = resource.Type;
                if (_resourcesText.TryGetValue(type, out var textField))
                {
                    textField.text = $"{resource.Name}: {resource.BaseExtaction}";
                }
            }
        }

        private void ClearPanel()
        {
            _name.text = $"None";
            _power.text = $"Мощь: None";
            _level.text = $"Уровень: None";
        }

        private void SetupPlanetType(PlanetInstance planet)
        {
            _type.text = planet.RuntimeData.Type switch
            {
                PlanetType.Capital => "Тип: столица",
                PlanetType.Industrial => "Тип: промышленная",
                PlanetType.Research => "Тип: научная",
                PlanetType.Military => "Тип: цитадель",
                PlanetType.Colony => "Тип: колония",
                _ => "Тип: неизвестно"
            };
        }

        private void HandleAddedPlanet()
        {
            OnPlanetAdded?.Invoke(_selectedPlanet);
        }
    }
}