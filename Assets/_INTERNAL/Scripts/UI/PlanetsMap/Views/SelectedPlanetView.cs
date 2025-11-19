using Core.Common.MVVM;
using Core.EntityDatas.Resource;
using GameEntity.DataInstance;
using GameEntity.Planet;
using R3;
using System;
using System.Collections.Generic;
using TMPro;
using UI.Base;
using UI.PlanetsMap.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Utils.Formatter;

namespace UI.PlanetsMap
{
    public class SelectedPlanetView : SimpleUIItem, IView
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

        private readonly CompositeDisposable _disposables = new();

        private SelectedPlanetViewModel _viewModel;

        private NumberFormatter _formatter;
        private PlanetInstance _selectedPlanet;

        private Dictionary<ResourceType, TextMeshProUGUI> _resourcesText;

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

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void OnEnable()
        {
            transform.SetAsLastSibling();

            _closeButton.onClick.AddListener(Hide);
            _addToTargetListButton.onClick.AddListener(AddPlanetToTargetList);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _addToTargetListButton.onClick.RemoveListener(AddPlanetToTargetList);
            ClearPanel();
        }

        public void BindViewModel(IViewModel viewModel)
        {
            _viewModel = viewModel as SelectedPlanetViewModel;

            _viewModel.OnPlanetAdded.Subscribe(_ => Hide()).AddTo(_disposables);
            _viewModel.OnPlanetSelected.Subscribe(planet =>
            {
                Setup(planet);

                if(!gameObject.activeSelf)
                    Show();
            }).AddTo(_disposables);
        }

        private void Setup(PlanetInstance planet)
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
                    textField.text = $"{resource.BaseExtaction}";
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

        private void AddPlanetToTargetList()
        {
            _viewModel.AddPlanetToTargetList(_selectedPlanet);
        }
    }
}