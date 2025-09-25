using Contex.MissionInfo;
using GameEntity.DataInstance;
using GameEntity.Mission;
using Scripts.GameEntity.DataInstance;
using TMPro;
using UI.Base;
using UI.MissionContexUI;
using UnityEngine;
using UnityEngine.UI;
using Utils.Formatter;

namespace Mono.UI.MissionContexUI
{
    public class MissionInfoUI : SimpleUIItem
    {
        [Header("Mission Info")]
        [SerializeField] private TextMeshProUGUI _selectedPlanetText;
        [SerializeField] private TextMeshProUGUI _selectedHeroText;
        [SerializeField] private TextMeshProUGUI _selectedType;
        [SerializeField] private TextMeshProUGUI _successChanceText;
        [SerializeField] private MissionPreparationUI _missionPrepareUI;

        [Space(10), Header("Close Button")]
        [SerializeField] private Button _closeButton;

        private MissionContex _missionContex;
        private DurationFormatter _durationFormatter;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
            Clear();
        }

        public void Initialize(MissionContex missionContex)
        {
            _missionContex = missionContex;
            _durationFormatter ??= new();

            UnsubscribeFromContexEvents();
            SubscribeOnContexEvents();

            Refresh();
        }

        private void SubscribeOnContexEvents()
        {
            if (_missionContex != null)
            {
                _missionContex.OnPlanetSelected += HandleSelectedPlanet;
                _missionContex.OnHeroSelected += HandleSelectedHero;
                _missionContex.OnMissionTypeSelected += HandleSelectedMissionType;
                _missionContex.OnMissionDifficultCalculated += HandleCalculatedMissionDifficulty;
                _missionContex.OnMissionDurationCalculated += HandleCalculatedMissionDuration;
                _missionContex.OnMissionSuccessChanceCalculated += HandlePreparedMission;
            }
        }

        private void UnsubscribeFromContexEvents()
        {
            if (_missionContex != null)
            {
                _missionContex.OnHeroSelected -= HandleSelectedHero;
                _missionContex.OnPlanetSelected -= HandleSelectedPlanet;
                _missionContex.OnMissionTypeSelected -= HandleSelectedMissionType;
                _missionContex.OnMissionDifficultCalculated -= HandleCalculatedMissionDifficulty;
                _missionContex.OnMissionDurationCalculated -= HandleCalculatedMissionDuration;
                _missionContex.OnMissionSuccessChanceCalculated -= HandlePreparedMission;
            }
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
            _selectedType.text = missionType switch
            {
                MissionType.Force => "Захват",
                MissionType.Diplomacy => "Дипломатия",
                MissionType.Sabotage => "Саботаж",
                MissionType.Recon => "Развездка",
                _ => "None"
            };
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
            _successChanceText.text = $"Шанс успеха: {chance * 100f:F1}%";
        }

        private void Clear()
        {
            _missionContex = null;

            _selectedPlanetText.text = "None";
            _selectedHeroText.text = "None";
            _selectedType.text = "None";
            _successChanceText.text = "0%";
            _missionPrepareUI.DifficultyText.text = "None";
            _missionPrepareUI.DurationText.text = "None";
        }

        private void Refresh()
        {
            if (_missionContex == null || _missionContex.PreparedMission == null)
                return;

            HandleSelectedPlanet(_missionContex.SelectedPlanet);
            HandleSelectedHero(_missionContex.SelectedHero);
            HandleSelectedMissionType(_missionContex.SelectedMissionType);
            HandleCalculatedMissionDifficulty(_missionContex.PreparedMission.Difficulty);
            HandleCalculatedMissionDuration(_missionContex.PreparedMission.Duration);
            HandlePreparedMission(_missionContex.PreparedMission.SuccessChance);
        }
    }
}