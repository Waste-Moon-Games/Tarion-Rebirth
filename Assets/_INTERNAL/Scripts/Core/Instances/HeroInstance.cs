using Core.GrowthSystem;
using GameEntity.ScriptableObjects;
using GameEntity.Unit.Data;
using System;
using UnityEngine;

namespace Scripts.GameEntity.DataInstance
{
    public class HeroInstance
    {
        [field: SerializeField] public float HeroPower { get; private set; }

        private readonly HeroData _sourceData;

        private readonly HeroRuntimeData _runtimeData;
        private readonly HeroGrowthSystem _growthSystem;

        public HeroRuntimeData RuntimeData => _runtimeData;
        public HeroGrowthSystem GrowthSystem => _growthSystem;
        public int HeroLevel => _runtimeData.Level;

        public event Action<int> OnLevelUp;
        public event Action<float> OnPowerChanged;

        public HeroInstance(HeroDataContainer baseData)
        {
            _sourceData = baseData.HeroData;
            _runtimeData = new(_sourceData);
            _growthSystem = new(_runtimeData.Stats);

            HeroPower = CalculateHeroPower();
            _growthSystem.SetLevelFromDataObject(_runtimeData.Level);

            _growthSystem.OnSkillPointsChanged += HandleLevelUp;
            _growthSystem.OnStatChanged += HandleChangedStats;
        }

        public void AddExperience(int exp)
        {
            _growthSystem.AddExperience(exp);

            _runtimeData.Experience = _growthSystem.CurrentExperience;
            _growthSystem.SetLevelFromDataObject(_runtimeData.Level);
        }

        public float CalculateHeroPower()
        {
            return _runtimeData.Stats.CalculatePower(_runtimeData.Level, _runtimeData.Rank);
        }

        public float GetExperienceProgress() => (float)_growthSystem.CurrentExperience / _growthSystem.GetRequiredExperienceForNextLevel();

        public int GetExperienceToNextLevel() => _growthSystem.GetRequiredExperienceForNextLevel();

        public int GetCurrentSkillPoints() => _growthSystem.CurrentSkillPoints;

        private void HandleLevelUp(int skillPoints)
        {
            _runtimeData.Level = _growthSystem.Level;

            HeroPower = CalculateHeroPower();

            OnLevelUp?.Invoke(skillPoints);
        }

        private void HandleChangedStats(HeroStatsRuntime stats)
        {
            HeroPower = CalculateHeroPower();
            OnPowerChanged?.Invoke(HeroPower);
        }
    }
}