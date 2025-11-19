using Core.EntityDatas.ImperiumInfo;
using Core.EntityDatas.Resource;
using R3;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils.Formatter;

namespace UI.MainMenu
{
    public class ImperiumHUDView : MonoBehaviour
    {
        private ImperiumViewModel _viewModel;

        [Header("Heros Info")]
        [SerializeField] private TextMeshProUGUI _currentHeroCountText;

        [Space(10), Header("Planets Info")]
        [SerializeField] private TextMeshProUGUI _currentPlanetCountText;

        [Space(10), Header("Resources Info")]
        [SerializeField] private TextMeshProUGUI _voidMatterCount;
        [SerializeField] private TextMeshProUGUI _darkEnergyCount;
        [SerializeField] private TextMeshProUGUI _mineralsCount;

        private NumberFormatter _formatter;

        private int _currentHeroCount;
        private int _currentPlanetCount;

        private int _maxHeroCount;
        private int _maxPlanetCount;

        private Dictionary<ResourceType, TextMeshProUGUI> _resourcesText;

        public void Bind(ImperiumViewModel viewModel)
        {
            _formatter ??= new();

            _resourcesText = new()
            {
                { ResourceType.Void_Matter, _voidMatterCount },
                { ResourceType.Dark_Energy, _darkEnergyCount },
                { ResourceType.Mineral_Crystalls, _mineralsCount }
            };

            _viewModel = viewModel;

            _maxHeroCount = _viewModel.MaxHerosCount;
            _maxPlanetCount = _viewModel.MaxPlanetsCount;

            RefreshAllText();

            _viewModel.ImperiumInstancesInfo
                .Subscribe(v => HandleImperiumInstancesInfoUpdated(v.Item1, v.Item2))
                .AddTo(this);

            _viewModel.ImperiumResourcesInfo
                .Subscribe(v => HandleImperiumResourcesUpdated(v.Item1, v.Item2))
                .AddTo(this);
        }

        private void RefreshAllText()
        {
            UpdatePlanetsCount(_viewModel.CurrentPlanetCount, _maxPlanetCount);
            UpdateHerosCount(_viewModel.CurrentHeroCount, _maxHeroCount);

            UpdateVoidMatterCount(_viewModel.VoidMatterCount);
            UpdateDarkEnergyCount(_viewModel.DarkEnergyCount);
            UpdateMineralsCount(_viewModel.MineralsCount);
        }

        private void UpdateHerosCount(int current, int max)
        {
            _currentHeroCount = current;
            _maxHeroCount = max;
            _currentHeroCountText.text = $"{current}/{max}";
        }

        private void UpdatePlanetsCount(int current, int max)
        {
            _currentPlanetCount = current;
            _maxPlanetCount = max;
            _currentPlanetCountText.text = $"{current}/{max}";
        }

        private void UpdateVoidMatterCount(int count)
        {
            _voidMatterCount.text = _formatter.FormatNumber(count);
        }

        private void UpdateDarkEnergyCount(int count)
        {
            _darkEnergyCount.text = _formatter.FormatNumber(count);
        }

        private void UpdateMineralsCount(int count)
        {
            _mineralsCount.text = _formatter.FormatNumber(count);
        }

        private void HandleImperiumResourcesUpdated(int count, ResourceType type)
        {
            if (_resourcesText.TryGetValue(type, out var text))
                text.text = _formatter.FormatNumber(count);
        }

        private void HandleImperiumInstancesInfoUpdated(int count, InstanceUpdateType type)
        {
            switch (type)
            {
                case InstanceUpdateType.HeroCount:
                    UpdateHerosCount(count, _maxHeroCount);
                    break;
                case InstanceUpdateType.PlanetCount:
                    UpdatePlanetsCount(count, _maxPlanetCount);
                    break;
                case InstanceUpdateType.MaxHeroCount:
                    UpdateHerosCount(_currentHeroCount, count);
                    break;
                case InstanceUpdateType.MaxPlanetCount:
                    UpdatePlanetsCount(_currentPlanetCount, count);
                    break;
            }
        }
    }
}