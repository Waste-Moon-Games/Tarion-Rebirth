using GameEntity.DataInstance;
using GameEntity.Mission;
using Scripts.GameEntity.DataInstance;
using System;
using UnityEngine;

namespace Contex.MissionInfo
{
    public class MissionContex
    {
        [field: SerializeField] public PlanetInstance SelectedPlanet {  get; private set; }
        [field: SerializeField] public HeroInstance SelectedHero { get; private set; }
        [field: SerializeField] public MissionType SelectedMissionType { get; private set; }
        [field: SerializeField] public MissionData CurrentMissionData { get; private set; }

        public event Action<PlanetInstance> OnPlanetSelected;
        public event Action<HeroInstance> OnHeroSelected;
        public event Action<MissionType> OnMissionTypeSelected;

        public event Action<float> OnMissionDifficultCalculated;
        public event Action<float> OnMissionDurationCalculated;

        public MissionContex()
        {
            CurrentMissionData = new();
        }

        public void SetPlanet(PlanetInstance selectedPlanet)
        {
            SelectedPlanet = selectedPlanet;
            CurrentMissionData.TargetPlanet = SelectedPlanet.RuntimeData;

            OnPlanetSelected?.Invoke(SelectedPlanet);
        }

        public void SetHero(HeroInstance selectedHero)
        {
            SelectedHero = selectedHero;
            CurrentMissionData.ChosenHero = SelectedHero.RuntimeData;

            OnHeroSelected?.Invoke(SelectedHero);
        }

        public void SetMissionType(MissionType selectedMissionType)
        {
            SelectedMissionType = selectedMissionType;
            CurrentMissionData.Type = SelectedMissionType;

            OnMissionTypeSelected?.Invoke(SelectedMissionType);
        }

        public void SetMissionDifficult(float missionDifficult)
        {
            CurrentMissionData.Difficult = missionDifficult;

            OnMissionDifficultCalculated?.Invoke(missionDifficult);
        }

        public void SetMissionDuration(float missionDuration)
        {
            CurrentMissionData.Duration = missionDuration;

            OnMissionDurationCalculated?.Invoke(missionDuration);
        }
    }
}