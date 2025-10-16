using Core.EntityDatas.Unit.Data;
using UnityEngine;

namespace Core.EntityDatas.Unit
{
    [System.Serializable]
    public class HeroCost
    {
        public float BaseCost = 500f;
    }

    public class HeroRuntimeCost
    {
        public float BaseCost { get; }

        public HeroRuntimeCost(HeroCost source)
        {
            BaseCost = source.BaseCost;
        }

        public int CalculateCost(int level, HeroQuality quality)
        {
            float qualityMultiplier = GetQualityMultiplier(quality);
            float levelMultiplier = GetLevelMultiplier(level);

            float totalMultiplier = qualityMultiplier + levelMultiplier;

            float result = BaseCost * totalMultiplier;
            return Mathf.RoundToInt(result);
        }

        private float GetLevelMultiplier(int level)
        {
            float initialMultiplier = 1f;
            float step = 0.35f;

            for (int i = 1; i < level; i++)
            {
                initialMultiplier += step;
            }

            return initialMultiplier;
        }

        private float GetQualityMultiplier(HeroQuality quality)
        {
            return quality switch
            {
                HeroQuality.Common => 1f,
                HeroQuality.Uncommon => 1.25f,
                HeroQuality.Rare => 2.5f,
                HeroQuality.Epic => 3.5f,
                HeroQuality.Legendary => 5f,
                _ => 1f
            };
        }
    }
}