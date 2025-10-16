using Contex.MissionInfo;
using Core.Common;
using Core.Common.SimpleTimer;
using Core.Factories.Stage_Factory;
using StateMachine.Base;
using UI.MainMenu.MissionExecutionUI;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionExecutionStage : IStage, IDisposable
    {
        private ISimpleTimer _timer;
        private IGameStageController _controller;

        private MissionContex _missionContex;
        private MissionExecutionTimer _uiController;

        public MissionExecutionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
            _uiController = dependencies.UIDependencies.MissionExecutionUI[controller.ControllerId];

            _timer = new Timer();
        }

        public void Enter()
        {
            _timer.Initialize(_missionContex.PreparedMission.Duration);
            _timer.OnTimeEnded += HandleEndedTime;

            _timer.Start();
            _uiController.SetTimer(_timer);
            _uiController.StartTimer(_timer.Progress);
        }

        public void RefreshDeps(IDependence dependence)
        {
            StageDependencies currentDeps = dependence as StageDependencies;
            _uiController = currentDeps.UIDependencies.MissionExecutionUI[_controller.ControllerId];
            _uiController.SetTimer(_timer);
        }

        public void Tick()
        {
            _timer?.Tick();
        }

        public void Exit()
        {
            _timer.OnTimeEnded -= HandleEndedTime;
            _uiController.ResetTimer();
        }

        public void Dispose()
        {
            _timer.OnTimeEnded -= HandleEndedTime;

            _timer = null;
            _controller = null;
            _missionContex = null;
            _uiController = null;
        }

        private void HandleEndedTime()
        {
            _timer.Stop();
            _uiController.StopTimer(_timer.Progress);
            _controller.SetStage(_controller.StageFactory.CreateMissionResultStage(_controller));
        }
    }
}