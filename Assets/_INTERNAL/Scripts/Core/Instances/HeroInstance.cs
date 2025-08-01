using Core.ExpSystem;
using GameEntity.ScriptableObjects;
using GameEntity.Unit.Data;
using UnityEngine;

namespace Scripts.GameEntity.DataInstance
{
    public class HeroInstance
    {
        [field: SerializeField] public float HeroPower { get; private set; }

        private readonly HeroDataContainer _baseData;
        private readonly HeroData _sourceData;

        private readonly HeroRuntimeData _runtimeData;
        private readonly ExperienceSystem _experienceSystem;

        public HeroRuntimeData RuntimeData => _runtimeData;
        public int HeroLevel => _experienceSystem.Level;

        public HeroInstance(HeroDataContainer baseData)
        {
            _baseData = baseData;
            _sourceData = _baseData.HeroData;
            _runtimeData = new(_sourceData);
            _experienceSystem = new();

            HeroPower = CalculateHeroPower();
            _experienceSystem.SetLevelFromDataObject(_runtimeData.Level);

            _experienceSystem.OnLevelUp += HandleLevelUp;

            Debug.Log($"Hero instance: {_runtimeData.Name} is initialized");
        }

        public void AddExperience(int exp)
        {
            _experienceSystem.AddExperience(exp);

            // Синхронизируем рантайм-данные для UI и сериализации
            _runtimeData.Experience = _experienceSystem.CurrentExperience;
            _runtimeData.Level = _experienceSystem.Level;

            Debug.Log($"Gained experience: {exp}");
        }

        public float CalculateHeroPower()
        {
            return _runtimeData.Stats.CalculatePower(_runtimeData.Level, _runtimeData.Rank);
        }

        public float GetExperienceProgress() => (float)_experienceSystem.CurrentExperience / _experienceSystem.GetRequiredExperienceForNextLevel();

        public int GetExperienceToNextLevel() => _experienceSystem.GetRequiredExperienceForNextLevel();

        private void HandleLevelUp(int level)
        {
            _runtimeData.Stats.ApplyLevelUp();

            HeroPower = CalculateHeroPower();

            Debug.Log($"Level up! Новый уровень: {level}. Базовые характеристики увеличены.");
        }
    }
}