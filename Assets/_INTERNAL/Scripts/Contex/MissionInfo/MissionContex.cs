using GameEntity.DataInstance;
using GameEntity.Mission;
using Scripts.GameEntity.DataInstance;
using UnityEngine;

namespace Contex.MissionInfo
{
    public class MissionContex
    {
        [field: SerializeField] public PlanetInstance SelectedPlanet {  get; private set; }
        [field: SerializeField] public HeroInstance SelectedHero { get; private set; }
        [field: SerializeField] public MissionType SelectedMissionType { get; private set; }
        [field: SerializeField] public MissionData CurrentMissionData { get; private set; }

        public MissionContex()
        {
            CurrentMissionData = new();
        }

        public void SetPlanet(PlanetInstance selectedPlanet)
        {
            SelectedPlanet = selectedPlanet;
            CurrentMissionData.TargetPlanet = SelectedPlanet.RuntimeData;
        }

        public void SetHero(HeroInstance selectedHero)
        {
            SelectedHero = selectedHero;
            CurrentMissionData.ChosenHero = SelectedHero.RuntimeData;
        }

        public void SetMissionType(MissionType selectedMissionType)
        {
            SelectedMissionType = selectedMissionType;
            CurrentMissionData.Type = SelectedMissionType;
        }
    }
}