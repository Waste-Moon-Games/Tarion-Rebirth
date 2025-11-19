using Contex.MissionInfo;
using Core.GameStates;
using Core.MissionSlots;
using Entry.EntryData;
using Entry.Mono;
using SO.Containers.Configs;
using System;
using UnityEngine;

namespace Mono.StateMachine
{
    public class GameStateMachineRuntimeSevice : MonoBehaviour
    {
        [SerializeField] private ImperiumConfig _config;
        [SerializeField] private StateMachineUIDependencies _uiDependencies;

        private ImperiumState _imperiumState;
        private MissionRuntimeService _missionRuntimeService;
        private MissionSlotsController _slotsController;

        public MissionSlotsController SlotsController => _slotsController;
        public MissionContex LastCreatedContex { get; private set; }
        public bool IsPreparationRunning { get; private set; }

        public event Action OnMissionStarted;

        private void OnDestroy()
        {
            MonoStageMachineDependencies.OnSceneDependenciesReady -= HandleMonoStageDeps;
            SlotControllerEventUnsubscribe();
        }

        public void Init(MissionRuntimeService missionRuntimeService, ImperiumState imperiumState)
        {
            MonoStageMachineDependencies.OnSceneDependenciesReady -= HandleMonoStageDeps;
            MonoStageMachineDependencies.OnSceneDependenciesReady += HandleMonoStageDeps;

            if (MonoStageMachineDependencies.Current != null)
                HandleMonoStageDeps(MonoStageMachineDependencies.Current);

            _missionRuntimeService = missionRuntimeService;
            _imperiumState = imperiumState;
            _slotsController ??= new(_config.MissionSlots);

            SlotControllerEventUnsubscribe();
            SlotControllerEventSubscribe();
        }

        public void Run()
        {
            var deps = CreateStageDeps();
            _slotsController.StartPreparateMission(deps);
        }

        public void ForceEnd()
        {
            IsPreparationRunning = false;
            _slotsController?.ForceEnd();
        }

        private void Update()
        {
            _slotsController?.Update();
        }

        private StageDependencies CreateStageDeps()
        {
            var contex = new MissionContex();

            var deps = new StageDependencies
                (
                    _imperiumState.InstanceHolder,
                    contex,
                    _uiDependencies,
                    _imperiumState.TargetsListState
                );

            LastCreatedContex = contex;

            return deps;
        }

        private void SlotControllerEventSubscribe()
        {
            if(_slotsController != null)
            {
                _slotsController.OnMissionFinished += HandleFinishedMission;
                _slotsController.OnMissionPreparationStarted += HandleStartedPreparationMission;
                _slotsController.OnMissionStarted += HandleStartedMission;
            }
        }

        private void SlotControllerEventUnsubscribe()
        {
            if(_slotsController != null)
            {
                _slotsController.OnMissionFinished -= HandleFinishedMission;
                _slotsController.OnMissionPreparationStarted -= HandleStartedPreparationMission;
                _slotsController.OnMissionStarted -= HandleStartedMission;
            }
        }

        private void HandleStartedPreparationMission(MissionContex contex)
        {
            IsPreparationRunning = true;
        }

        private void HandleStartedMission(MissionContex contex)
        {
            IsPreparationRunning = false;
            _missionRuntimeService.AddActiveMission(contex);
            OnMissionStarted?.Invoke();
        }

        private void HandleFinishedMission(int arg1, MissionContex contex)
        {
            _missionRuntimeService.RemoveFinishedMission(contex);
        }

        private void HandleMonoStageDeps(MonoStageMachineDependencies deps)
        {
            _uiDependencies = deps.StateMachineUIDependencies;

            _slotsController?.RefreshDeps(_uiDependencies);
        }
    }
}