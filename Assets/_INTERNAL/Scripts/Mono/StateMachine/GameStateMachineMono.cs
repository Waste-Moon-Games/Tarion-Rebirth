using Mono.InstanceInitialize;
using StateMachine;
using StateMachine.Base;
using UnityEngine;

namespace Mono.StateMachine
{
    public class GameStateMachineMono : MonoBehaviour
    {
        [SerializeField] private BootDatas _bootDatas;
        [SerializeField] private StateMachineUIDependencies _machineUIDependencies;

        private IGameStageController _gameStageController;

        public void Run()
        {
            _gameStageController = new GameStageController(_bootDatas.InstanceHolder, _machineUIDependencies);

            _gameStageController.Start();
        }
    }
}