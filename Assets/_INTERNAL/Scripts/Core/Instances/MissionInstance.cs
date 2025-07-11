using GameEntity.Mission;
using UnityEngine;

namespace GameEntity.DataInstance
{
    public class MissionInstance
    {
        private MissionData _runtimeData;

        public MissionData RuntimeData => _runtimeData;

        public MissionInstance(MissionData data)
        {
            _runtimeData = data;

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