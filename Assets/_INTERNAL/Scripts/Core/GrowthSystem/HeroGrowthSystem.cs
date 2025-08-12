using Core.EntityDatas.Unit.Data;
using GameEntity.Unit.Data;
using System;
using UnityEngine;

namespace Core.GrowthSystem
{
    public class HeroGrowthSystem
    {
        [field: SerializeField] public int MaxLevel { get; private set; } = 50;
        [field: SerializeField] public int CurrentLevel { get; private set; } = 1;
        [field: SerializeField] public int CurrentExperience { get; private set; } = 0;
        [field: SerializeField] public int CurrentSkillPoints { get; private set; } = 0;

        private readonly HeroRuntimeStats _heroStats;

        public event Action<int> OnSkillPointsChanged;
        public event Action<HeroRuntimeStats> OnStatChanged;

        public HeroGrowthSystem(HeroRuntimeStats heroStats)
        {
            _heroStats = heroStats;
        }

        public void SetLevelFromDataObject(int level)
        {
            CurrentLevel = level;
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
            if (amount <= 0 || CurrentLevel >= MaxLevel) 
                return;

            CurrentExperience += amount;

            while (CurrentExperience >= GetRequiredExperienceForNextLevel())
            {
                CurrentExperience -= GetRequiredExperienceForNextLevel();
                CurrentLevel++;
                AddSkillPoints();
                OnSkillPointsChanged?.Invoke(CurrentSkillPoints);
            }
        }

        public int GetRequiredExperienceForNextLevel()
        {
            return 100 * CurrentLevel;
        }

        private void AddSkillPoints()
        {
            if (CurrentLevel % 3 == 1)
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