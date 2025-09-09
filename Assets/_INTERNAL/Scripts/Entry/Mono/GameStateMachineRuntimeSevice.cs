using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Entry.Mono;
using Entry.Mono.MissionPanel;
using Stages.StageController;
using System;
using UnityEngine;

namespace Mono.StateMachine
{
    public class GameStateMachineRuntimeSevice : MonoBehaviour
    {
        [SerializeField] private StateMachineUIDependencies _uiDependencies;
        [SerializeField] private StartPrepareMission _prepareMissionButton;
        [SerializeField] private MissionRuntimeService _missionRuntimeService;

        private GameStageController _gameStageController;
        private StageDependencies _stageDependencies;
        private StageFactory _stageFactory;

        private bool _isRunning = false;

        public StageDependencies StageDependencies => _stageDependencies;
        public bool IsRunning => _isRunning;

        public event Action<GameStageController> OnGameStageControllerInitialized;
        public event Action OnMissionStarted;

        private void OnEnable()
        {
            _missionRuntimeService = GameWorldStateMono.Instance.MissionRuntimeService;

            MonoStageMachineDependencies.OnSceneDependenciesReady += HandleMonoStageDeps;

            if (MonoStageMachineDependencies.Current != null)
                HandleMonoStageDeps(MonoStageMachineDependencies.Current);
        }

        private void OnDisable()
        {
            MonoStageMachineDependencies.OnSceneDependenciesReady -= HandleMonoStageDeps;
        }

        public void Run()
        {
            InitCore();

            RefreshSubscribes();

            _gameStageController.StartCycle();
            _missionRuntimeService.SubscribeOnMissionContexEvents();

            OnGameStageControllerInitialized?.Invoke(_gameStageController);
        }

        public void ForceEnd()
        {
            _missionRuntimeService.Dispose();
            _gameStageController?.ForceEnd();
            _isRunning = false;
        }

        private void Update()
        {
            _gameStageController?.Update();
        }

        private void InitCore()
        {
            var intstanceHolder = GameWorldStateMono
                .Instance
                .GameWorldState
                .ImperiumState
                .InstanceHolder;
            var targetList = GameWorldStateMono
                .Instance
                .GameWorldState
                .ImperiumState
                .TargetsListState;

            var contex = new MissionContex();

            _stageDependencies = new StageDependencies
                (
                    intstanceHolder,
                    contex,
                    _uiDependencies,
                    targetList
                );

            _stageFactory = new(_stageDependencies);
            _gameStageController = new GameStageController(_stageFactory);
            _missionRuntimeService.SetMissionContex(contex);
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
            _stageDependencies = null;
            _stageFactory = null;
            _gameStageController = null;

            _missionRuntimeService.Dispose();
            _isRunning = false;
            _prepareMissionButton.PrepareMissionButton.interactable = true;
        }

        private void HandleStartedMission()
        {
            _isRunning = true;
            OnMissionStarted?.Invoke();
        }

        private void HandleMonoStageDeps(MonoStageMachineDependencies deps)
        {
            _uiDependencies = deps.StateMachineUIDependencies;
            _prepareMissionButton = deps.StartPrepareMission;

            if (_uiDependencies == null)
                return;

            _stageDependencies?.RefreshUIDependencies(_uiDependencies);
            _gameStageController?.RefreshDeps(_stageDependencies);
        }
    }
}