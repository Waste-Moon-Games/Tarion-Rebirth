using Contex.MissionInfo;
using Entry.Mono;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.GameUIBridges
{
    public class MissionUIBridge : MonoBehaviour
    {
        [field: SerializeField] public List<MissionContex> CurrentContextes { get; private set; } = new();

        public bool HasCurrentContexes => CurrentContextes != null;

        private void Awake()
        {
            if(GameWorldStateMono.Instance.MissionRuntimeService.HasActiveMissions)
                CurrentContextes = GameWorldStateMono.Instance.MissionRuntimeService.ActiveMissions;
        }

        public MissionContex GetMissionFromIndex(int index)
        {
            var active = CurrentContextes[index];

            if (active == null)
                return null;

            return active;
        }

        public MissionContex GetMission()
        {
            var active = CurrentContextes.FirstOrDefault(cc => cc.PreparedMission != null);

            if (active == null)
                return null;

            return active;
        }

        public bool HasActiveMissions()
        {
            var active = CurrentContextes.FirstOrDefault(cc => cc.PreparedMission != null);

            if (active == null)
                return false;

            return true;
        }
    }
}