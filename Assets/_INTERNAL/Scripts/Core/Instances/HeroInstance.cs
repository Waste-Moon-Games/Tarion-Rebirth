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
        private readonly HeroData _runtimeData;

        public HeroData RuntimeData => _runtimeData;

        public HeroInstance(HeroDataContainer baseData)
        {
            _baseData = baseData;
            _runtimeData = _baseData.HeroData;

            HeroPower = CalculateHeroPower();
            HeroLevel = _runtimeData.Level;
            Debug.Log($"Hero instance: {_runtimeData.Name} is initialized");
        }

        public float CalculateHeroPower()
        {
            return _runtimeData.Stats.CalculatePower(_runtimeData.Level, _runtimeData.Rank);
        }
    }
}