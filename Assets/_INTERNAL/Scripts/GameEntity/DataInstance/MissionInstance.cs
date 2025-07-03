using GameEntity.Mission;
using GameEntity.ScriptableObjects;
using UnityEngine;

namespace GameEntity.DataInstance
{
    public class MissionInstance
    {
        private readonly MissionDataContainer _baseData;
        private MissionData _runtimeData;

        public MissionInstance(MissionDataContainer baseData)
        {
            _baseData = baseData;
            _runtimeData = _baseData.MissionData;

            Debug.Log($"Mission instance: {_runtimeData.Type} is initialized");
        }

        public void CalculateDifficult()
        {

        }

        public void CalculateDuration()
        {

        }
    }
}