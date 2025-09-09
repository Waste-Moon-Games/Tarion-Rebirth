using GameEntity.DataInstance.Main;
using TMPro;
using UI.GameUIBridges;
using UnityEngine;

namespace UI.ImperiumStateUI
{
    public class ImperiumPlanetsUI : MonoBehaviour
    {
        [Header("Bridge")]
        [SerializeField] private ImperiumStateUIBridge _bridge;

        [Space(10), Header("Info")]
        [SerializeField] private TextMeshProUGUI _planets;

        private int _currentCount;
        private int _maxCount;

        private ImperiumInstancesHolder _instance;

        private void Start()
        {
            if (_bridge.HasImperiumState)
                Setup(_bridge.ImperiumState.InstanceHolder);
        }

        private void OnDestroy()
        {
            _instance.OnPlanetsCountChanged -= HandleChangedCurrentPlanetsCount;
            _instance.OnPlanetsLimitUpgraded -= HandleUpgradedLimit;
        }

        private void Setup(ImperiumInstancesHolder imperiumStateHolder)
        {
            _instance = imperiumStateHolder;

            _instance.OnPlanetsCountChanged += HandleChangedCurrentPlanetsCount;
            _instance.OnPlanetsLimitUpgraded += HandleUpgradedLimit;

            _currentCount = imperiumStateHolder.Planets.Count;
            _maxCount = imperiumStateHolder.MaxPlanets;

            _planets.text = $"{_currentCount}/{_maxCount}";
        }

        private void HandleUpgradedLimit(int maxCount)
        {
            _maxCount = maxCount;
            _planets.text = $"{_currentCount}/{_maxCount}";
        }

        private void HandleChangedCurrentPlanetsCount(int currentCount)
        {
            _currentCount = currentCount;
            _planets.text = $"{_currentCount}/{_maxCount}";
        }
    }
}