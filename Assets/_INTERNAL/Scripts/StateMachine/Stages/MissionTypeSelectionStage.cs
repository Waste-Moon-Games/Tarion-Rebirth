using Contex.MissionInfo;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using GameEntity.Mission;
using Mono.UI.MissionContexUI;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionTypeSelectionStage : IStage
    {
        private readonly GameStageController _controller;
        private readonly MissionContex _missionContex;
        private readonly MissionTypeListController _missionTypeListController;

        private readonly InstanceHolder _instanceHolder;

        public MissionTypeSelectionStage(GameStageController controller, MissionContex missionContex, InstanceHolder instanceHolder, MissionTypeListController missionTypeListController)
        {
            _controller = controller;
            _missionContex = missionContex;
            _instanceHolder = instanceHolder;
            _missionTypeListController = missionTypeListController;
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

        private void HandleSelectedMissionType(MissionInstance missionType)
        {
            _missionContex.SetMissionType(missionType.RuntimeData.Type);
            _controller.SetStage(_controller.CreateMissionPreparationStage(_missionContex, _instanceHolder));
        }
    }
}