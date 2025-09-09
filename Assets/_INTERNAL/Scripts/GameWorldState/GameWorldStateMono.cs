using Contex.MissionInfo;
using Core.GameStates;
using Mono.InstanceInitialize;
using UnityEngine;

namespace Entry.Mono
{
    public class GameWorldStateMono : MonoBehaviour
    {
        public static GameWorldStateMono Instance;

        [SerializeField] private BootUniqueDatas _bootDatas;
        [field: SerializeField] public GameWorldState GameWorldState { get; private set; }
        [field: SerializeField] public MissionRuntimeService MissionRuntimeService { get; private set; }


        private void Awake()
        {
            if (_bootDatas == null)
                return;

            GameWorldState = new
                (
                _bootDatas.HeroDatas,
                _bootDatas.PlanetDatas,
                _bootDatas.MissionDatas,
                _bootDatas.RankProgressionConfig,
                _bootDatas.StartLimitsConfig
                );

            if(Instance == null)
                Instance = this;

            MissionRuntimeService.OnActiveMissionSetted += GameWorldState.ImperiumStateController.SetActiveContex;
        }

        private void OnDestroy()
        {
            MissionRuntimeService.OnActiveMissionSetted -= GameWorldState.ImperiumStateController.SetActiveContex;
        }
    }
}