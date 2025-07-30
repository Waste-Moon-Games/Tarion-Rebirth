using UnityEngine;

namespace GameEntity.Unit.Data
{
    [System.Serializable]
    public class HeroData
    {
        public string Name;

        [TextArea]
        public string Description;

        public int BaseHealth;
        public int Level;
        public int Experience;
        public Rank Rank;

        public HeroStats Stats;
    }

    public class HeroRuntimeData
    {
        public string Name;

        [TextArea]
        public string Description;

        public int BaseHealth;
        public int Level;
        public int Experience;
        public Rank Rank;

        public HeroStats Stats;

        public HeroRuntimeData(HeroData source)
        {
            Name = source.Name;
            Description = source.Description;
            BaseHealth = source.BaseHealth;
            Level = source.Level;
            Experience = source.Experience;
            Rank = source.Rank;
            Stats = source.Stats;
        }
    }
}