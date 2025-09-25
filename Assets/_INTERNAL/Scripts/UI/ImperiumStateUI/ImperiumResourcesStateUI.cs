using Core.EntityDatas.Resource;
using Core.GameStates;
using System.Collections.Generic;
using TMPro;
using UI.GameUIBridges;
using UnityEngine;
using Utils.Formatter;

namespace UI.ImperiumStateUI
{
    public class ImperiumResourcesStateUI : MonoBehaviour
    {
        [Header("Brigde")]
        [SerializeField] private ImperiumStateUIBridge _bridge;

        [Space(10), Header("Resources")]
        [SerializeField] private TextMeshProUGUI _voidMatter;
        [SerializeField] private TextMeshProUGUI _darkEnergy;
        [SerializeField] private TextMeshProUGUI _minerals;

        private Dictionary<ResourceType, TextMeshProUGUI> _resourcesText;
        private ImperiumResourceState _resourcesState;
        private NumberFormatter _formatter;

        private void Awake()
        {
            _resourcesText = new()
            {
                { ResourceType.Dark_Energy, _darkEnergy },
                { ResourceType.Mineral_Crystalls, _minerals },
                { ResourceType.Void_Matter, _voidMatter },
            };

            _formatter ??= new();
        }

        private void Start()
        {
            if (_bridge.HasImperiumState)
                Setup(_bridge.ImperiumState.ImperiumResource);

            _resourcesState.OnResourceChanged += HandleChangedResources;
        }

        private void OnDestroy()
        {
            _resourcesState.OnResourceChanged -= HandleChangedResources;
        }

        private void Setup(ImperiumResourceState resourceState)
        {
            _resourcesState = resourceState;

            _voidMatter.text = $"{_formatter.FormatNumber(resourceState.Get(ResourceType.Void_Matter))}";
            _darkEnergy.text = $"{_formatter.FormatNumber(resourceState.Get(ResourceType.Dark_Energy))}";
            _minerals.text = $"{_formatter.FormatNumber(resourceState.Get(ResourceType.Mineral_Crystalls))}";
        }

        private void HandleChangedResources(ResourceType type, int value)
        {
            if (_resourcesText.TryGetValue(type, out var text))
                text.text = $"{_formatter.FormatNumber(value)}";
            else
                Debug.LogWarning($"No UI text found for resource of type: {type}");
        }
    }
}