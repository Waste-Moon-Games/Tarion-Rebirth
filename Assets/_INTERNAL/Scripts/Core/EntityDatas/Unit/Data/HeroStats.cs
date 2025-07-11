namespace GameEntity.Unit.Data
{
    [System.Serializable]
    public class HeroStats
    {
        public float BasePower;
        public float PowerGrowthMultiplier;

        public int Strenght;
        public int Agility;
        public int Intelligence;

        public float CalculatePower(int level)
        {
            return BasePower + 
                (level * PowerGrowthMultiplier) + 
                (Strenght * 0.5f) + 
                (Agility * 0.3f) + 
                (Intelligence * 2f);
        }
    }
}