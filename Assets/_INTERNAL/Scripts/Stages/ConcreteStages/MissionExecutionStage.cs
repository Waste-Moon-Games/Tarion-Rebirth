using Contex.MissionInfo;
using Core.Common;
using Core.Factories.Stage_Factory;
using StateMachine.Base;
using UI.MissionExecutionUI;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionExecutionStage : IStage, IDisposable
    {
        private IGameStageController _controller;

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
            _uiController.UpdateTimer();
        }

        public void Exit()
        {
            _uiController.OnTimeEnded -= HandleEndedTime;
            _uiController.Hide();
        }

        public void Dispose()
        {
            _controller = null;
            _missionContex = null;
            _uiController = null;
        }

        private void HandleEndedTime()
        {
            _controller.SetStage(_controller.StageFactory.CreateMissionResultStage(_controller));
        }
    }
}