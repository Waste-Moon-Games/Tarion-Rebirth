using Contex.MissionInfo;
using GameEntity.DataInstance;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GameStates
{
    public class MissionRuntimeService
    {
        private readonly Subject<MissionContex> _activeMissionSettedSignal = new();

        private readonly List<MissionContex> _activeMissions = new();

        public IReadOnlyList<MissionContex> ActiveMissions => _activeMissions;
        public bool HasActiveMissions => _activeMissions.Count != 0;

        public Observable<MissionContex> ActiveMissionSetted => _activeMissionSettedSignal.AsObservable();

        public void AddActiveMission(MissionContex contex)
        {
            contex.OnMissionPrepared += HandlePrerapedMission;
            _activeMissions.Add(contex);
            _activeMissionSettedSignal.OnNext(contex);
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