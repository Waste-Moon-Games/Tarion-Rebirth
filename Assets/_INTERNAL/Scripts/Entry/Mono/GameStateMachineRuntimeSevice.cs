using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Core.MissionSlots;
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

        private MissionSlotsController _slotsController;

        public MissionSlotsController SlotsController => _slotsController;
        public MissionContex LastCreatedContex { get; private set; }
        public bool IsPreparationRunning { get; private set; }

        public event Action OnMissionStarted;

        private void OnEnable()
        {
            //_missionRuntimeService = GameWorldStateMono.Instance.MissionRuntimeService;

            MonoStageMachineDependencies.OnSceneDependenciesReady += HandleMonoStageDeps;

            if (MonoStageMachineDependencies.Current != null)
                HandleMonoStageDeps(MonoStageMachineDependencies.Current);

            _slotsController ??= new(_config.MissionSlots);
            SlotControllerEventSubscribe();
        }

        private void OnDisable()
        {
            MonoStageMachineDependencies.OnSceneDependenciesReady -= HandleMonoStageDeps;
            SlotControllerEventUnsubscribe();
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
            //var intstanceHolder = GameWorldStateMono
            //    .Instance
            //    .GameWorldState
            //    .ImperiumState
            //    .InstanceHolder;
            //var targetList = GameWorldStateMono
            //    .Instance
            //    .GameWorldState
            //    .ImperiumState
            //    .TargetsListState;

            //var contex = new MissionContex();

            //var deps = new StageDependencies
            //    (
            //        intstanceHolder,
            //        contex,
            //        _uiDependencies,
            //        targetList
            //    );

            //LastCreatedContex = contex;

            return null;
        }

        private void SlotControllerEventSubscribe()
        {
            _slotsController.OnMissionFinished += HandleFinishedMission;
            _slotsController.OnMissionPreparationStarted += HandleStartedPreparationMission;
            _slotsController.OnMissionStarted += HandleStartedMission;
        }

        private void SlotControllerEventUnsubscribe()
        {
            _slotsController.OnMissionFinished -= HandleFinishedMission;
            _slotsController.OnMissionPreparationStarted -= HandleStartedPreparationMission;
            _slotsController.OnMissionStarted -= HandleStartedMission;
        }

        private void HandleStartedPreparationMission(MissionContex contex)
        {
            IsPreparationRunning = true;
        }

        private void HandleStartedMission(MissionContex contex)
        {
            IsPreparationRunning = false;
            //_missionRuntimeService.AddActiveMission(contex);
            OnMissionStarted?.Invoke();
        }

        private void HandleFinishedMission(int arg1, MissionContex contex)
        {
            //_missionRuntimeService.RemoveFinishedMission(contex);
        }

        private void HandleMonoStageDeps(MonoStageMachineDependencies deps)
        {
            _uiDependencies = deps.StateMachineUIDependencies;

            _slotsController?.RefreshDeps(_uiDependencies);
        }
    }
}