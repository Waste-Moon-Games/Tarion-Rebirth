using Core.GameStates;
using Mono.InstanceInitialize;
using UnityEngine;
using Utils.ModCoroutines;

namespace GameWorldState
{
    public class GameWorldStateMono : MonoBehaviour
    {
        public static GameWorldStateMono Instance;

        [SerializeField] private BootUniqueDatas _bootDatas;
        [field: SerializeField] public GameState GameState { get; private set; }
        [field: SerializeField] public MissionRuntimeService MissionRuntimeService { get; private set; }

        private void Awake()
        {
            //if (_bootDatas == null)
            //    return;
            //var coroutines = Object.FindFirstObjectByType<Coroutines>();

            //GameState = new
            //    (
            //    _bootDatas.HeroDatas,
            //    _bootDatas.PlanetDatas,
            //    _bootDatas.MissionDatas,
            //    _bootDatas.RankProgressionConfig,
            //    _bootDatas.ImperiumConfig,
            //    coroutines
            //    );

            //if(Instance == null)
            //    Instance = this;

            //MissionRuntimeService.OnActiveMissionSetted += GameState.ImperiumStateController.SetActiveContex;
        }

        //private void Start()
        //{
        //    //GameWorldState.ImperiumState.ResourceService.StartExtraction();
        //}

        private void OnDestroy()
        {
            //MissionRuntimeService.OnActiveMissionSetted -= GameState.ImperiumStateController.SetActiveContex;

            //GameWorldState.ImperiumState.ResourceService.StopExtaction();
        }
    }
}