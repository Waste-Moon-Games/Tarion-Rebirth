using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance.Main;
using GameEntity.Mission;
using Mono.UI.MissionContexUI;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionTypeSelectionStage : IStage
    {
        private readonly IGameStageController _controller;
        private MissionContex _missionContex;
        private MissionTypeListController _missionTypeListController;
        private InstanceHolder _instanceHolder;

        public MissionTypeSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionTypeListController = dependencies.UIDependencies.MissionTypeListController;
            _instanceHolder = dependencies.InstanceHolder;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            Debug.Log("Mission Selection Stage: Enter");
            if (!_missionTypeListController.gameObject.activeSelf)
            {
                _missionTypeListController.Show();
            }
            _missionTypeListController.Initialize(_instanceHolder);
            _missionTypeListController.OnMissionTypeSelected += HandleSelectedMissionType;
        }

        public void Exit()
        {
            Debug.Log("Mission Selection Stage: Exit");
            _missionTypeListController.Hide();
            _missionTypeListController.OnMissionTypeSelected -= HandleSelectedMissionType;
        }

        public void Tick()
        {
        }

        private void HandleSelectedMissionType(MissionType missionType)
        {
            _missionContex.SetMissionType(missionType);
            //_controller.SetStage();
        }
    }
}