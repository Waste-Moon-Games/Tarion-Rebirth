using Core.Common.Instances;
using Core.EntityDatas.Unit.Data;
using Core.GrowthSystem;
using GameEntity.ScriptableObjects;
using GameEntity.Unit.Data;
using R3;
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

        private readonly Subject<int> _levelUpSignal = new();
        private readonly Subject<float> _powerChangedSignal = new();
        private readonly Subject<int> _expChangedSignal = new();

        private bool _isBusy = false;

        public HeroRuntimeData RuntimeData => _runtimeData;
        public HeroGrowthSystem GrowthSystem => _growthSystem;
        public int HeroLevel => _runtimeData.Level;
        public bool IsBusy => _isBusy;

        public Observable<int> LevelUp => _levelUpSignal.AsObservable();
        public Observable<float> PowerChanged => _powerChangedSignal.AsObservable();
        public Observable<int> ExpChanged => _expChangedSignal.AsObservable();

        public HeroInstance(HeroDataContainer baseData, RankProgressionConfig config)
        {
            _sourceData = baseData.HeroData;
            _runtimeData = new(_sourceData);
            _growthSystem = new(_runtimeData.Stats);
            _rankSystem = new(_runtimeData, config);

            CalculateCost(_runtimeData);

            HeroPower = CalculatePower();
            _growthSystem.SetLevelFromDataObject(_runtimeData.Level);

            _growthSystem.OnSkillPointsChanged += HandleLevelUp;
            _growthSystem.OnStatChanged += HandleChangedStats;
        }

        public HeroInstance(HeroData sourceData, RankProgressionConfig config)
        {
            _sourceData = sourceData;
            _runtimeData = new(_sourceData);
            _growthSystem = new(_runtimeData.Stats);
            _rankSystem = new(_runtimeData, config);

            CalculateCost(_runtimeData);

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
            _expChangedSignal.OnNext(_runtimeData.Experience);
        }

        public void SetBusyStatus(bool value)
        {
            _isBusy = value;
        }

        public float CalculatePower()
        {
            return _runtimeData.Stats.CalculatePower(_runtimeData.Level, _runtimeData.Rank);
        }

        public float GetExperienceProgress() => (float)_growthSystem.CurrentExperience / _growthSystem.GetRequiredExperienceForNextLevel();

        public int GetExperienceToNextLevel() => _growthSystem.GetRequiredExperienceForNextLevel();

        public int GetCurrentSkillPoints() => _growthSystem.CurrentSkillPoints;

        private void CalculateCost(HeroRuntimeData data)
        {
            data.RecruitmentCost = data.Cost.CalculateCost(data.Level, data.Quality);
        }

        private void HandleLevelUp(int skillPoints)
        {
            _runtimeData.Level = _growthSystem.CurrentLevel;

            _rankSystem.OnHeroLevelChanged(_runtimeData.Level);
            _runtimeData.Rank = _rankSystem.GetCurrentRank();

            HeroPower = CalculatePower();

            _levelUpSignal.OnNext(skillPoints);
        }

        private void HandleChangedStats(HeroRuntimeStats stats)
        {
            HeroPower = CalculatePower();
            _powerChangedSignal.OnNext(HeroPower);
        }
    }
}