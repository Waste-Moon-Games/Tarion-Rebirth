using System;
using UnityEngine;

namespace Core.ExpSystem
{
    public class ExperienceSystem
    {
        [field: SerializeField] public int Level { get; private set; } = 1;
        [field: SerializeField] public int CurrentExperience { get; private set; } = 0;

        public event Action<int> OnLevelUp;

        public void SetLevelFromDataObject(int level)
        {
            Level = level;
        }

        public void AddExperience(int amount)
        {
            if (amount <= 0) return;

            CurrentExperience += amount;

            while (CurrentExperience >= GetRequiredExperienceForNextLevel())
            {
                CurrentExperience -= GetRequiredExperienceForNextLevel();
                Level++;
                OnLevelUp?.Invoke(Level);
            }
        }

        public int GetRequiredExperienceForNextLevel()
        {
            return 100 * Level;
        }
    }
}