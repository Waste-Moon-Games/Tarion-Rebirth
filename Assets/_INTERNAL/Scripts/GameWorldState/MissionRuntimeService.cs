using Contex.MissionInfo;
using System;
using UnityEngine;

namespace Entry.Mono
{
    public class MissionRuntimeService : MonoBehaviour
    {
        private MissionContex _activeMission;

        public MissionContex ActiveMission => _activeMission;
        public bool HasActiveMission => _activeMission != null;

        public event Action<MissionContex> OnActiveMissionSetted;

        public void SetMissionContex(MissionContex contex)
        {
            if (HasActiveMission)
            {
                Debug.LogWarning("Mission is already coming");
                return;
            }

            _activeMission = contex;
            OnActiveMissionSetted?.Invoke(contex);
        }

        public void SubscribeOnMissionContexEvents()
        {
            _activeMission.OnMissionPrepared += HandlePrerapedMission;
        }

        public void Dispose()
        {
            if (_activeMission != null)
                _activeMission.OnMissionPrepared -= HandlePrerapedMission;

            _activeMission = null;
        }

        private void HandlePrerapedMission()
        {
            _activeMission.PreparedMission.BeginMission();
        }
    }
}