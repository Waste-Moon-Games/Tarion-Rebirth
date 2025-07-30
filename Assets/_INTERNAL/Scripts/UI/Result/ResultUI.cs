using Contex.MissionInfo;
using System;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Result
{
    public class ResultUI : SimpleUIItem
    {
        [SerializeField] private Button _acceptButton;

        [Space(5), Header("Mission Info")]
        [SerializeField] private TextMeshProUGUI _gainedExpText;
        [SerializeField] private TextMeshProUGUI _selectedHeroText;
        [SerializeField] private TextMeshProUGUI _targetPlanetText;

        private MissionContex _contex;

        public event Action OnResultAccepted;

        private void OnEnable()
        {
            _acceptButton.onClick.AddListener(()=> OnResultAccepted?.Invoke());
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveListener(()=> OnResultAccepted?.Invoke());
        }

        public void Initialize(MissionContex contex)
        {
            _contex = contex;
            _gainedExpText.text = $"Опыт: {_contex.PreparedMission.GainedExp}";
            _selectedHeroText.text = $"Герой: {_contex.SelectedHero.RuntimeData.Name}";
            _targetPlanetText.text = $"Планета: {_contex.SelectedPlanet.RuntimeData.PlanetName}";
        }
    }
}