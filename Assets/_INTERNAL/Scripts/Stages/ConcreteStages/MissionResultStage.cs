using Contex.MissionInfo;
using Core.Common;
using Core.Factories.Stage_Factory;
using StateMachine.Base;
using UI.Result;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionResultStage : IStage, IDisposable
    {
        private IGameStageController _controller;
        private MissionContex _missionContex;
        private ResultPanel _panelHolder;

        public MissionResultStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
            _panelHolder = dependencies.UIDependencies.ResultPanelHolder;
        }

        public void Enter()
        {
            Debug.Log("Result Stage: Enter");
            _panelHolder.OnResultAccepted += HandleAcceptedResult;
            _panelHolder.ResultUI.Initialize(_missionContex);

            if (!_panelHolder.gameObject.activeSelf)
            {
                _panelHolder.Show();
            }

            if (_missionContex.PreparedMission.MissionSuccessful)
            {
                _missionContex.ApplyAcceptedResults();
            }
        }

        public void Tick() { }

        public void Exit()
        {
            Debug.Log("Result Stage: Exit");
            _panelHolder.Hide();
            _panelHolder.OnResultAccepted -= HandleAcceptedResult;
        }

        public void Dispose()
        {
            _controller = null;
            _missionContex = null;
            _panelHolder = null;
        }

        private void HandleAcceptedResult()
        {
            _controller.ExitStage();
        }
    }
}