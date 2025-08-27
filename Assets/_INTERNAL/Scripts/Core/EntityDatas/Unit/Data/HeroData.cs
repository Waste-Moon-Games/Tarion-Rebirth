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
        public Sprite HeroArt;

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
        public Sprite HeroArt;

        public HeroRuntimeStats Stats;

        public HeroRuntimeData(HeroData source)
        {
            Name = source.Name;
            Description = source.Description;
            BaseHealth = source.BaseHealth;
            Level = source.Level;
            Experience = source.Experience;
            Rank = source.Rank;
            HeroArt = source.HeroArt;
            Stats = new(source.Stats);
        }
    }
}