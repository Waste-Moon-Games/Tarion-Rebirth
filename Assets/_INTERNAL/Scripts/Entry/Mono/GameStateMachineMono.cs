using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Entry;
using Entry.Mono.MissionPanel;
using Stages.StageController;
using System;
using UnityEngine;

namespace Mono.StateMachine
{
    public class GameStateMachineMono : MonoBehaviour
    {
        [SerializeField] private DataHolder _dataHolder;
        [SerializeField] private StateMachineUIDependencies _uiDependencies;
        [SerializeField] private StartPrepareMission _prepareMissionButton;

        private GameStageController _gameStageController;
        private StageDependencies _stageDependencies;
        private StageFactory _stageFactory;

        public StageDependencies StageDependencies => _stageDependencies;
        public GameStageController GameStageController => _gameStageController;

        public event Action<GameStageController> OnGameStageControllerInitialized;

        public void Run()
        {
            _stageDependencies = new StageDependencies(
                    _dataHolder.BootDatas.InstanceHolder,
                    new MissionContex(),
                    _uiDependencies);

            _stageFactory = new(_stageDependencies);
            _gameStageController = new GameStageController(_stageFactory);

            _gameStageController.OnResultAccepted -= HandleAcceptedResult;
            _gameStageController.OnResultAccepted += HandleAcceptedResult;

            _gameStageController.Start();

            OnGameStageControllerInitialized?.Invoke(_gameStageController);
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