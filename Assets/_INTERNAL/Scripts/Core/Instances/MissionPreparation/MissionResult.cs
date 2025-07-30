namespace Core.EntityDatas.Mission
{
    public class MissionResult
    {
        public bool IsMissionSuccessful { get; set; }
        public int HeroExperience { get; set; }
        public int HeroHealth { get; set; }
        public bool PlanetStatus {  get; set; }
        public float Difficult { get; set; }
        public float Duration { get; set; }
    }
}