using Contex.MissionInfo;
using Entry.Mono;
using UnityEngine;

namespace UI.GameUIBridges
{
    public class MissionUIBridge : MonoBehaviour
    {
        [field: SerializeField] public MissionContex CurrentContex { get; private set; }

        public bool HasCurrentContex => CurrentContex != null;

        private void Awake()
        {
            if(GameWorldStateMono.Instance.MissionRuntimeService.HasActiveMission)
                CurrentContex = GameWorldStateMono.Instance.MissionRuntimeService.ActiveMission;
        }
    }
}