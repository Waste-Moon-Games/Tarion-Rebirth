using GameEntity.ScriptableObjects;
using GameEntity.Unit.Data;
using UnityEngine;

namespace Scripts.GameEntity.DataInstance
{
    public class HeroInstance
    {
        private readonly HeroDataContainer _baseData;
        private HeroData _runtimeData;

        public HeroInstance(HeroDataContainer baseData)
        {
            _baseData = baseData;
            _runtimeData = _baseData.HeroData;

            Debug.Log($"Hero instance: {_runtimeData.Name} is initialized");
        }

        public void AddExperience(int amount)
        {
            _runtimeData.Experience += amount;
        }
    }
}