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

        private float _strenghtMultiplier;
        private float _agilityMultiplier;
        private float _intelligenceMultiplier;

        public float CalculatePower(int level, Rank rank)
        {
            SetupMultipliers(rank);

            float scaledStr = (Strenght + level * _strenghtMultiplier);
            float scaledAgi = (Agility + level * _agilityMultiplier);
            float scaledInt = (Intelligence + level * _intelligenceMultiplier);

            return BasePower + (level * PowerGrowthMultiplier) + scaledStr + scaledAgi + scaledInt;
        }

        private void SetupMultipliers(Rank rank)
        {
            float value = rank switch
            {
                Rank.Recruit => 0.20f,
                Rank.Veteran => 0.35f,
                Rank.Elite => 0.40f,
                Rank.Champion => 0.45f,
                Rank.Guardian => 0.50f,
                _ => 0.2f
            };

            _strenghtMultiplier = value;
            _agilityMultiplier = value;
            _intelligenceMultiplier = value;
        }
    }
}