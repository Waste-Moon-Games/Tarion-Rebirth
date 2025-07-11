using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Mono.InstanceInitialize;
using Stages.StageController;
using UnityEngine;

namespace Mono.StateMachine
{
    public class GameStateMachineMono : MonoBehaviour
    {
        [SerializeField] private BootDatas _bootDatas;
        [SerializeField] private StateMachineUIDependencies _uiDependencies;

        private GameStageController _gameStageController;
        private StageDependencies _stageDependencies;

        public StageDependencies StageDependencies => _stageDependencies;

        public void Run()
        {
            _stageDependencies = new StageDependencies(
                _bootDatas.InstanceHolder,
                new MissionContex(),
                _uiDependencies);

            var stageFactory = new StageFactory(_stageDependencies);

            _gameStageController = new GameStageController(stageFactory);

            _gameStageController.Start();
        }
    }
}