using Contex.MissionInfo;
using System;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.Events;
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
        [SerializeField] private TextMeshProUGUI _resultText;

        private UnityAction _clickHandler;

        public event Action OnResultAccepted;

        private void OnEnable()
        {
            _clickHandler = () => OnResultAccepted?.Invoke();

            _acceptButton.onClick.AddListener(_clickHandler);
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveListener(_clickHandler);
        }

        private void OnDestroy()
        {
            _clickHandler?.Invoke();
        }

        public void Initialize(MissionContex contex)
        {
            SetupText(contex);
            _resultText.text = CheckResultStatus(contex);
        }

        private void SetupText(MissionContex contex)
        {
            _gainedExpText.text = $"Опыт: {contex.PreparedMission.GainedExp}";
            _selectedHeroText.text = $"Герой: {contex.SelectedHero.RuntimeData.Name}";
            _targetPlanetText.text = $"Планета: {contex.SelectedPlanet.RuntimeData.PlanetName}";
        }

        private string CheckResultStatus(MissionContex contex)
        {
            if (contex.PreparedMission.MissionSuccessful)
                return "Успех!";

            return "Провал";
        }
    }
}