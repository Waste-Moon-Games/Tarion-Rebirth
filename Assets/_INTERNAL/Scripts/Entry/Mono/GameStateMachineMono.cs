using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Entry;
using Entry.Mono;
using Entry.Mono.MissionPanel;
using Stages.StageController;
using System;
using UnityEngine;

namespace Mono.StateMachine
{
    public class GameStateMachineMono : MonoBehaviour
    {
        [SerializeField] private StateMachineUIDependencies _uiDependencies;
        [SerializeField] private StartPrepareMission _prepareMissionButton;

        private GameStageController _gameStageController;
        private StageDependencies _stageDependencies;
        private StageFactory _stageFactory;

        public StageDependencies StageDependencies => _stageDependencies;
        public GameStageController GameStageController => _gameStageController;

        public event Action<GameStageController> OnGameStageControllerInitialized;
        public event Action OnMissionStarted;

        public void Run()
        {
            var intstanceHolder = GameWorldStateMono.Instance.GameWorldState.ImperiumState.InstanceHolder;

            _stageDependencies ??= new StageDependencies(
                    intstanceHolder,
                    new MissionContex(),
                    _uiDependencies);

            _stageFactory ??= new(_stageDependencies);
            _gameStageController ??= new GameStageController(_stageFactory);
            RefreshSubscribes();

            _gameStageController.StartCycle();

            OnGameStageControllerInitialized?.Invoke(_gameStageController);
        }

        public void ForceEnd()
        {
            _gameStageController?.ForceEnd();
        }

        private void Update()
        {
            _gameStageController?.Update();
        }

        private void RefreshSubscribes()
        {
            _gameStageController.OnResultAccepted -= HandleAcceptedResult;
            _gameStageController.OnMissionStarted -= HandleStartedMission;
            _gameStageController.OnMissionStarted += HandleStartedMission;
            _gameStageController.OnResultAccepted += HandleAcceptedResult;
        }

        private void HandleAcceptedResult()
        {
            _prepareMissionButton.PrepareMissionButton.interactable = true;
        }

        private void HandleStartedMission()
        {
            OnMissionStarted?.Invoke();
        }
    }
}