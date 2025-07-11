namespace GameEntity.Unit.Data
{
    [System.Serializable]
    public class HeroData
    {
        public string Name;
        public string Description;
        public int Level;
        public int Experience;
        public Rank Rank;

        public HeroStats Stats;
    }
}