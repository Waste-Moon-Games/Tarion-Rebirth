using Contex.MissionInfo;
using GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameWorldState
{
    public class MissionRuntimeService
    {
        private readonly List<MissionContex> _activeMissions;

        public IReadOnlyList<MissionContex> ActiveMissions => _activeMissions;
        public bool HasActiveMissions => _activeMissions.Count != 0;

        public event Action<MissionContex> OnActiveMissionSetted;

        public MissionRuntimeService()
        {
            _activeMissions = new();
        }

        public void AddActiveMission(MissionContex contex)
        {
            contex.OnMissionPrepared += HandlePrerapedMission;
            _activeMissions.Add(contex);
            OnActiveMissionSetted?.Invoke(contex);
        }

        public void RemoveFinishedMission(MissionContex contex)
        {
            contex.OnMissionPrepared -= HandlePrerapedMission;
            _activeMissions.Remove(contex);
        }

        private void HandlePrerapedMission(MissionInstance mission)
        {
            var instance = _activeMissions.FirstOrDefault(i => i.PreparedMission == mission);
            if (instance == null)
                return;

            instance.PreparedMission.BeginMission();
        }
    }
}