using Contex.MissionInfo;
using GameEntity.DataInstance;
using GameEntity.Mission;
using Scripts.GameEntity.DataInstance;
using TMPro;
using UnityEngine;

namespace Mono.UI.MissionContexUI
{
    public class MissionInfoUI : MonoBehaviour
    {
        [Header("Mission Info")]
        [SerializeField] private TextMeshProUGUI _selectedPlanetText;
        [SerializeField] private TextMeshProUGUI _selectedHeroText;
        [SerializeField] private TextMeshProUGUI _selectedType;

        private MissionContex _missionContex;

        private void OnDisable()
        {
            UnsubscribeFromContexEvents();
        }

        public void Initialize(MissionContex missionContex)
        {
            _missionContex = missionContex;
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
        }

        private void UnsubscribeFromContexEvents()
        {
            _missionContex.OnHeroSelected -= HandleSelectedHero;
            _missionContex.OnPlanetSelected -= HandleSelectedPlanet;
            _missionContex.OnMissionTypeSelected -= HandleSelectedMissionType;
            _missionContex.OnMissionDifficultCalculated -= HandleCalculatedMissionDifficulty;
            _missionContex.OnMissionDurationCalculated -= HandleCalculatedMissionDuration;
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

        }

        private void HandleCalculatedMissionDuration(float calculatedDuration)
        {

        }

        private void Refresh()
        {
            HandleSelectedHero(_missionContex.SelectedHero);
            HandleSelectedPlanet(_missionContex.SelectedPlanet);
        }
    }
}