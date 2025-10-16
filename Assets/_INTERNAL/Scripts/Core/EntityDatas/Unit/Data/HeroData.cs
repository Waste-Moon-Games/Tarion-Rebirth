using Core.EntityDatas.Unit;
using Core.EntityDatas.Unit.Data;
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
        public Rank HeroRank;
        public Sprite HeroArt;
        public int RecruitmentCost;

        [Space(10), Header("Stats")]
        public HeroStats Stats;

        [Space(10), Header("Unique params")]
        public bool IsUnique;
        public HeroGender HeroGender;
        public HeroQuality HeroQuality;
        public HeroCost HeroCost;
    }

    public class HeroRuntimeData
    {
        public string Name;

        public string Description;

        public int BaseHealth;
        public int Level;
        public int Experience;
        public Rank Rank;
        public Sprite HeroArt;
        public int RecruitmentCost;

        public HeroRuntimeStats Stats;
        public bool IsUnique;
        public HeroGender Gender;
        public HeroQuality Quality;
        public HeroRuntimeCost Cost;

        public HeroRuntimeData(HeroData source)
        {
            Name = source.Name;
            Description = source.Description;

            BaseHealth = source.BaseHealth;
            Level = source.Level;
            Experience = source.Experience;
            Rank = source.HeroRank;
            RecruitmentCost = source.RecruitmentCost;

            HeroArt = source.HeroArt;

            Stats = new(source.Stats);
            Cost = new(source.HeroCost);

            IsUnique = source.IsUnique;
            Gender = source.HeroGender;
            Quality = source.HeroQuality;
        }
    }
}