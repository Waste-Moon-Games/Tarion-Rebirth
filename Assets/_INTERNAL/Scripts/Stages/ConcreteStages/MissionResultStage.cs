using Contex.MissionInfo;
using Core.Common;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using StateMachine.Base;
using UI.Result;

namespace StateMachine.Stages
{
    public class MissionResultStage : IStage, IDisposable
    {
        private ImperiumInstancesHolder _instancesHolder;
        private IGameStageController _controller;
        private MissionContex _missionContex;
        private ResultPanel _panelHolder;

        public MissionResultStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
            _instancesHolder = dependencies.InstanceHolder;
            _panelHolder = dependencies.UIDependencies.ResultPanelHolder;
        }

        public void Enter()
        {
            _panelHolder.OnResultAccepted += HandleAcceptedResult;
            _panelHolder.ResultUI.Initialize(_missionContex);

            if (!_panelHolder.gameObject.activeSelf)
                _panelHolder.Show();

            _missionContex.ApplyMissionResults();
        }

        public void RefreshDeps(IDependence dependence)
        {
            StageDependencies currentDeps = dependence as StageDependencies;
            _panelHolder = currentDeps.UIDependencies.ResultPanelHolder;
        }

        public void Tick() { }

        public void Exit()
        {
            _panelHolder.Hide();
            _panelHolder.OnResultAccepted -= HandleAcceptedResult;
        }

        public void Dispose()
        {
            _controller = null;
            _instancesHolder = null;
            _missionContex = null;
            _panelHolder = null;
        }

        private void HandleAcceptedResult()
        {
            _controller.EndCycle();
        }
    }
}