using UnityEngine;

namespace GameEntity.Unit.Data
{
    [System.Serializable]
    public class HeroStats
    {
        public float BasePower;
        public float PowerGrowthMultiplier;

        public int Strength;
        public int Dexterity;
        public int Intelligence;

        public float StrengthMultiplier;
        public float DexterityMultiplier;
        public float IntelligenceMultiplier;
    }

    public class HeroRuntimeStats
    {
        public float BasePower;
        public float PowerGrowthMultiplier;

        public int Strength;
        public int Dexterity;
        public int Intelligence;

        public float StrengthMultiplier;
        public float DexterityMultiplier;
        public float IntelligenceMultiplier;

        private float _strengthMultiplier;
        private float _dexterityMultiplier;
        private float _intelligenceMultiplier;

        public HeroRuntimeStats(HeroStats source)
        {
            BasePower = source.BasePower;
            PowerGrowthMultiplier = source.PowerGrowthMultiplier;

            Strength = source.Strength;
            Dexterity = source.Dexterity;
            Intelligence = source.Intelligence;

            StrengthMultiplier = source.StrengthMultiplier;
            DexterityMultiplier = source.DexterityMultiplier;
            IntelligenceMultiplier = source.IntelligenceMultiplier;
        }

        public float CalculatePower(int level, Rank rank)
        {
            SetupMultipliers(rank);

            float scaledStr = (Strength * level) * _strengthMultiplier;
            float scaledAgi = (Dexterity * level) * _dexterityMultiplier;
            float scaledInt = (Intelligence * level) * _intelligenceMultiplier;

            float stats = scaledStr + scaledAgi + scaledInt;

            return BasePower + Mathf.Sqrt(level) * PowerGrowthMultiplier * stats;
        }

        private void SetupMultipliers(Rank rank)
        {
            float value = rank switch
            {
                Rank.Recruit => 0.5f,
                Rank.Veteran => 1f,
                Rank.Elite => 2f,
                Rank.Champion => 3.5f,
                Rank.Guardian => 5f,
                _ => 0.5f
            };

            _strengthMultiplier = value * StrengthMultiplier;
            _dexterityMultiplier = value * DexterityMultiplier;
            _intelligenceMultiplier = value * IntelligenceMultiplier;
        }
    }
}