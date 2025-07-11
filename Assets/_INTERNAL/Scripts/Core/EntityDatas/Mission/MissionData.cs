using GameEntity.Planet;
using GameEntity.Unit.Data;
using UnityEngine;

namespace GameEntity.Mission
{
    [System.Serializable]
    public class MissionData
    {
        public MissionType Type;
        public int GainedExperience;
        public float Difficult;
        public float Duration;

        public PlanetData TargetPlanet;
        public HeroData ChosenHero;
    }
}