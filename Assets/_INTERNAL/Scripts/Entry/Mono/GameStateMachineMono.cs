using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Entry.Mono.MissionPanel;
using Mono.InstanceInitialize;
using Stages.StageController;
using UnityEngine;

namespace Mono.StateMachine
{
    public class GameStateMachineMono : MonoBehaviour
    {
        [SerializeField] private BootDatas _bootDatas;
        [SerializeField] private StateMachineUIDependencies _uiDependencies;
        [SerializeField] private StartPrepareMission _prepareMissionButton;

        private GameStageController _gameStageController;
        private StageDependencies _stageDependencies;
        private StageFactory _stageFactory;

        public StageDependencies StageDependencies => _stageDependencies;

        public void Run()
        {
            _stageDependencies = new StageDependencies(
                    _bootDatas.InstanceHolder,
                    new MissionContex(),
                    _uiDependencies);

            _stageFactory = new(_stageDependencies);
            _gameStageController = new GameStageController(_stageFactory);

            _gameStageController.OnResultAccepted -= HandleAcceptedResult;
            _gameStageController.OnResultAccepted += HandleAcceptedResult;

            _gameStageController.Start();
        }

        private void Update()
        {
            _gameStageController?.Update();
        }

        private void HandleAcceptedResult()
        {
            _prepareMissionButton.PrepareMissionButton.interactable = true;
        }
    }
}