using Mono.InstanceInitialize;
using Mono.StateMachine;
using Mono.UI.PlanetListUI;
using UnityEngine;

namespace StateMachine
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Datas")]
        [SerializeField] private BootDatas _bootDatas;

        [Space(5), Header("UI")]
        [SerializeField] private PlanetListController _planetListController;

        [Space(5), Header("State Machine")]
        [SerializeField] private GameStateMachineMono _gameStateMachineMono;

        private void Start()
        {
            _bootDatas.BootGameData();

            _gameStateMachineMono.Run();
        }
    }
}