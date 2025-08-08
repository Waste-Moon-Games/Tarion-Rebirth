namespace GameEntity.Unit.Data
{
    [System.Serializable]
    public class HeroStats
    {
        public float BasePower;
        public float PowerGrowthMultiplier;

        public int Strenght;
        public int Dexterity;
        public int Intelligence;
    }

    public class HeroStatsRuntime
    {
        public float BasePower;
        public float PowerGrowthMultiplier;

        public int Strenght;
        public int Dexterity;
        public int Intelligence;

        private float _strenghtMultiplier;
        private float _agilityMultiplier;
        private float _intelligenceMultiplier;

        public HeroStatsRuntime(HeroStats source)
        {
            BasePower = source.BasePower;
            PowerGrowthMultiplier = source.PowerGrowthMultiplier;

            Strenght = source.Strenght;
            Dexterity = source.Dexterity;
            Intelligence = source.Intelligence;
        }

        public float CalculatePower(int level, Rank rank)
        {
            SetupMultipliers(rank);

            float scaledStr = (Strenght + level * _strenghtMultiplier);
            float scaledAgi = (Dexterity + level * _agilityMultiplier);
            float scaledInt = (Intelligence + level * _intelligenceMultiplier);

            return BasePower + (level * PowerGrowthMultiplier * (scaledStr + scaledAgi + scaledInt));
        }

        private void SetupMultipliers(Rank rank)
        {
            float value = rank switch
            {
                Rank.Recruit => 0.20f,
                Rank.Veteran => 0.50f,
                Rank.Elite => 1f,
                Rank.Champion => 1.2f,
                Rank.Guardian => 1.5f,
                _ => 0.2f
            };

            _strenghtMultiplier = value;
            _agilityMultiplier = value;
            _intelligenceMultiplier = value;
        }
    }
}