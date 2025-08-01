using Contex.MissionInfo;
using GameEntity.DataInstance;
using GameEntity.Mission;
using Scripts.GameEntity.DataInstance;
using TMPro;
using UI.MissionContexUI;
using UnityEngine;
using Utils.Formatter;

namespace Mono.UI.MissionContexUI
{
    public class MissionInfoUI : MonoBehaviour
    {
        [Header("Mission Info")]
        [SerializeField] private TextMeshProUGUI _selectedPlanetText;
        [SerializeField] private TextMeshProUGUI _selectedHeroText;
        [SerializeField] private TextMeshProUGUI _selectedType;
        [SerializeField] private TextMeshProUGUI _successChanceText;
        [SerializeField] private MissionPreparationUI _missionPrepareUI;

        private MissionContex _missionContex;
        private DurationFormatter _durationFormatter;

        private void OnDisable()
        {
            UnsubscribeFromContexEvents();
        }

        public void Initialize(MissionContex missionContex)
        {
            _missionContex = missionContex;
            _durationFormatter = new();

            SubscribeOnContexEvents();

            Refresh();
        }

        private void SubscribeOnContexEvents()
        {
            _missionContex.OnPlanetSelected += HandleSelectedPlanet;
            _missionContex.OnHeroSelected += HandleSelectedHero;
            _missionContex.OnMissionTypeSelected += HandleSelectedMissionType;
            _missionContex.OnMissionDifficultCalculated += HandleCalculatedMissionDifficulty;
            _missionContex.OnMissionDurationCalculated += HandleCalculatedMissionDuration;
            _missionContex.OnMissionSuccessChanceCalculated += HandlePreparedMission;
        }

        private void UnsubscribeFromContexEvents()
        {
            _missionContex.OnHeroSelected -= HandleSelectedHero;
            _missionContex.OnPlanetSelected -= HandleSelectedPlanet;
            _missionContex.OnMissionTypeSelected -= HandleSelectedMissionType;
            _missionContex.OnMissionDifficultCalculated -= HandleCalculatedMissionDifficulty;
            _missionContex.OnMissionDurationCalculated -= HandleCalculatedMissionDuration;
            _missionContex.OnMissionSuccessChanceCalculated -= HandlePreparedMission;
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            if(selectedHero != null)
                _selectedHeroText.text = selectedHero.RuntimeData.Name;
            else
            {
                _selectedHeroText.text = "None";
            }
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            if(selectedPlanet != null)
                _selectedPlanetText.text = selectedPlanet.RuntimeData.PlanetName;
            else
            {
                _selectedPlanetText.text = "None";
            }
        }

        private void HandleSelectedMissionType(MissionType missionType)
        {
            _selectedType.text = $"{missionType}";
        }

        private void HandleCalculatedMissionDifficulty(float calculatedDifficulty)
        {
            _missionPrepareUI.DifficultyText.text = $"Сложность: {calculatedDifficulty}";
        }

        private void HandleCalculatedMissionDuration(float calculatedDuration)
        {
            string duration = _durationFormatter.FormatDuraion(calculatedDuration);
            _missionPrepareUI.DurationText.text = $"Длительность: {duration}";
        }

        private void HandlePreparedMission(float chance)
        {
            _successChanceText.text = $"Шанс на успех: {(chance * 100f):F1}%";
        }

        private void Refresh()
        {
            HandleSelectedHero(_missionContex.SelectedHero);
            HandleSelectedPlanet(_missionContex.SelectedPlanet);
        }
    }
}