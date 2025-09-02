using Core.Common.Instances;
using Core.GrowthSystem;
using GameEntity.ScriptableObjects;
using GameEntity.Unit.Data;
using SO.Containers.GameEntity;
using System;
using UnityEngine;

namespace Scripts.GameEntity.DataInstance
{
    public class HeroInstance : IInstance
    {
        [field: SerializeField] public float HeroPower { get; private set; }

        private readonly HeroData _sourceData;

        private readonly HeroRuntimeData _runtimeData;
        private readonly HeroGrowthSystem _growthSystem;
        private readonly HeroRankUpSystem _rankSystem;

        public HeroRuntimeData RuntimeData => _runtimeData;
        public HeroGrowthSystem GrowthSystem => _growthSystem;
        public int HeroLevel => _runtimeData.Level;

        public event Action<int> OnLevelUp;
        public event Action<float> OnPowerChanged;
        public event Action<int> OnExpChanged;

        public HeroInstance(HeroDataContainer baseData, RankProgressionConfig config)
        {
            _sourceData = baseData.HeroData;
            _runtimeData = new(_sourceData);
            _growthSystem = new(_runtimeData.Stats);
            _rankSystem = new(_runtimeData, config);

            HeroPower = CalculatePower();
            _growthSystem.SetLevelFromDataObject(_runtimeData.Level);

            _growthSystem.OnSkillPointsChanged += HandleLevelUp;
            _growthSystem.OnStatChanged += HandleChangedStats;
        }

        public void AddExperience(int exp)
        {
            _growthSystem.AddExperience(exp);

            _runtimeData.Experience = _growthSystem.CurrentExperience;
            _growthSystem.SetLevelFromDataObject(_runtimeData.Level);
            OnExpChanged?.Invoke(_runtimeData.Experience);
        }

        public float CalculatePower()
        {
            return _runtimeData.Stats.CalculatePower(_runtimeData.Level, _runtimeData.Rank);
        }

        public float GetExperienceProgress() => (float)_growthSystem.CurrentExperience / _growthSystem.GetRequiredExperienceForNextLevel();

        public int GetExperienceToNextLevel() => _growthSystem.GetRequiredExperienceForNextLevel();

        public int GetCurrentSkillPoints() => _growthSystem.CurrentSkillPoints;

        private void HandleLevelUp(int skillPoints)
        {
            _runtimeData.Level = _growthSystem.CurrentLevel;

            _rankSystem.OnHeroLevelChanged(_runtimeData.Level);
            _runtimeData.Rank = _rankSystem.GetCurrentRank();

            HeroPower = CalculatePower();

            OnLevelUp?.Invoke(skillPoints);
        }

        private void HandleChangedStats(HeroRuntimeStats stats)
        {
            HeroPower = CalculatePower();
            OnPowerChanged?.Invoke(HeroPower);
        }
    }
}