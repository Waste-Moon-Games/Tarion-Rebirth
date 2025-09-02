using Contex.MissionInfo;
using Core.Common;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance.Main;
using GameEntity.Mission;
using Mono.UI.MissionContexUI;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionTypeSelectionStage : IStage, IDisposable
    {
        private IGameStageController _controller;
        private MissionContex _missionContex;
        private MissionTypeListController _missionTypeListController;
        private ImperiumInstancesHolder _instanceHolder;

        public MissionTypeSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionTypeListController = dependencies.UIDependencies.SelectionPanel.MissionTypeListController;
            _instanceHolder = dependencies.InstanceHolder;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            _missionTypeListController.Initialize(_instanceHolder);
            _missionTypeListController.OnMissionTypeSelected += HandleSelectedMissionType;

            if (!_missionTypeListController.gameObject.activeSelf)
                _missionTypeListController.Show();
        }

        public void Tick() { }

        public void Exit()
        {
            _missionTypeListController.Hide();
            _missionTypeListController.OnMissionTypeSelected -= HandleSelectedMissionType;
        }

        public void Dispose()
        {
            _controller = null;
            _missionContex = null;
            _missionTypeListController = null;
            _instanceHolder = null;
        }


        private void HandleSelectedMissionType(MissionType missionType)
        {
            _missionContex.SetMissionType(missionType);
            _controller.SetStage(_controller.StageFactory.CreateMissionPreparationStage(_controller));
        }
    }
}