using GameEntity.ScriptableObjects;
using GameEntity.Unit.Data;
using UnityEngine;

namespace Scripts.GameEntity.DataInstance
{
    public class HeroInstance
    {
        [field: SerializeField] public float HeroPower { get; private set; }
        [field: SerializeField] public int HeroLevel { get; private set; }  

        private readonly HeroDataContainer _baseData;
        private readonly HeroData _sourceData;

        private readonly HeroRuntimeData _runtimeData;

        public HeroRuntimeData RuntimeData => _runtimeData;

        public HeroInstance(HeroDataContainer baseData)
        {
            _baseData = baseData;
            _sourceData = _baseData.HeroData;
            _runtimeData = new(_sourceData);

            HeroPower = CalculateHeroPower();
            HeroLevel = _runtimeData.Level;
            Debug.Log($"Hero instance: {_runtimeData.Name} is initialized");
        }

        public void AddExperience(int exp)
        {
            if (exp < 0) return;

            _runtimeData.Experience += exp;

            Debug.Log($"Gained experience: {exp}");
        }

        public float CalculateHeroPower()
        {
            return _runtimeData.Stats.CalculatePower(_runtimeData.Level, _runtimeData.Rank);
        }
    }
}