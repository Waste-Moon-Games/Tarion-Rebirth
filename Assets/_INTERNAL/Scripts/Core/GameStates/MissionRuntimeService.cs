using Context.MissionInfo;
using GameEntity.DataInstance;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GameStates
{
    public class MissionRuntimeService
    {
        private readonly Subject<MissionContext> _activeMissionSettedSignal = new();

        private readonly List<MissionContext> _activeMissions = new();

        public IReadOnlyList<MissionContext> ActiveMissions => _activeMissions;
        public bool HasActiveMissions => _activeMissions.Count != 0;

        public Observable<MissionContext> ActiveMissionSetted => _activeMissionSettedSignal.AsObservable();

        public void AddActiveMission(MissionContext contex)
        {
            contex.OnMissionPrepared += HandlePrerapedMission;
            _activeMissions.Add(contex);
            _activeMissionSettedSignal.OnNext(contex);
        }

        public void RemoveFinishedMission(MissionContext contex)
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