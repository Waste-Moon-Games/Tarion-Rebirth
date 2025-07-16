using GameEntity.Planet;
using GameEntity.Unit.Data;

namespace GameEntity.Mission
{
    [System.Serializable]
    public class MissionData
    {
        public MissionType Type;
        public int GainedExperience {  get; set; }
        public float Difficulty {  get; set; }
        public float Duration { get; set; }

        public PlanetRuntimeData TargetPlanet { get; set; }
        public HeroRuntimeData ChosenHero { get; set; }
    }
}