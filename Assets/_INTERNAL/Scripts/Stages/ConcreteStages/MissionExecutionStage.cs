using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using StateMachine.Base;
using UI.MissionExecutionUI;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionExecutionStage : IStage
    {
        private readonly IGameStageController _controller;

        private MissionContex _missionContex;
        private MissionExecutionTimer _uiController;

        public MissionExecutionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
            _uiController = dependencies.UIDependencies.MissionExecutionUI;
        }

        public void Enter()
        {
            Debug.Log("Mission Execution Stage: Enter");
            _uiController.OnTimeEnded += HandleEndedTime;

            if (!_uiController.gameObject.activeSelf)
            {
                _uiController.Show();
            }

            _uiController.Initialize(_missionContex);
            _uiController.StartTimer();
        }

        public void Tick()
        {
            // TODO: выполнение миссии
            _uiController.UpdateTimer();
        }

        public void Exit()
        {
            Debug.Log("Mission Execution Stage: Exit");
            _uiController.OnTimeEnded -= HandleEndedTime;
            _uiController.Hide();
        }

        private void HandleEndedTime()
        {
            _controller.SetStage(_controller.StageFactory.CreateMissionResultStage(_controller));
        }
    }
}