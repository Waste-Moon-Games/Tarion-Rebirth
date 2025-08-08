using Core.EntityDatas.Unit.Data;
using GameEntity.Unit.Data;
using System;
using UnityEngine;

namespace Core.GrowthSystem
{
    public class HeroGrowthSystem
    {
        [field: SerializeField] public int Level { get; private set; } = 1;
        [field: SerializeField] public int CurrentExperience { get; private set; } = 0;
        [field: SerializeField] public int CurrentSkillPoints { get; private set; } = 0;

        private readonly HeroStatsRuntime _heroStats;

        public event Action<int> OnSkillPointsChanged;
        public event Action<HeroStatsRuntime> OnStatChanged;

        public HeroGrowthSystem(HeroStatsRuntime heroStats)
        {
            _heroStats = heroStats;
        }

        public void SetLevelFromDataObject(int level)
        {
            Level = level;
        }

        public void TryIncreaseStats(HeroStatType statType)
        {
            if (CurrentSkillPoints <= 0)
                return;

            switch (statType)
            {
                case HeroStatType.Strength:
                    _heroStats.Strenght += 1;
                    break;
                case HeroStatType.Dexterity:
                    _heroStats.Dexterity += 1;
                    break;
                case HeroStatType.Intelligence:
                    _heroStats.Intelligence += 1;
                    break;
            }

            CurrentSkillPoints--;
            OnStatChanged?.Invoke(_heroStats);
            OnSkillPointsChanged?.Invoke(CurrentSkillPoints);
        }

        public void AddExperience(int amount)
        {
            if (amount <= 0) 
                return;

            CurrentExperience += amount;

            while (CurrentExperience >= GetRequiredExperienceForNextLevel())
            {
                CurrentExperience -= GetRequiredExperienceForNextLevel();
                Level++;
                AddSkillPoints();
                OnSkillPointsChanged?.Invoke(CurrentSkillPoints);
            }
        }

        public int GetRequiredExperienceForNextLevel()
        {
            return 100 * Level;
        }

        private void AddSkillPoints()
        {
            if (Level % 3 == 1)
            {
                CurrentSkillPoints += 2;
            }
            else
            {
                CurrentSkillPoints++;
            }
        }
    }
}