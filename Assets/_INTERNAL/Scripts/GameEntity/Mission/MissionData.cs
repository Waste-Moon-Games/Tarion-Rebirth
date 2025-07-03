using GameEntity.Planet;
using GameEntity.Unit.Data;

namespace GameEntity.Mission
{
    [System.Serializable]
    public struct MissionData
    {
        public MissionType Type;
        public int GainedExperience;
        public float Difficult;
        public float Duration;

        public PlanetData TargetPlanet;
        public HeroData ChosenHero;
    }
}