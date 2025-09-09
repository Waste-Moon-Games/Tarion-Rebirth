using Contex.MissionInfo;
using Core.Common;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance;
using Mono.UI;
using Mono.UI.MissionContexUI;
using StateMachine.Base;
using UI.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionPreparationStage : IStage, IDisposable
    {
        private IGameStageController _controller;
        private MissionContex _missionContex;

        public MissionPreparationStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            _missionContex.OnMissionPrepared += HandlePreparedMission;

            MissionInstance mission = _missionContex.TryCreateMissionInstane();

            mission?.PrepareMission();
            _missionContex.SetPreparedMission(mission);
        }

        public void RefreshDeps(IDependence _) { }

        public void Tick() { }

        public void Exit()
        {
            _missionContex.OnMissionPrepared -= HandlePreparedMission;
        }

        public void Dispose()
        {
            _controller = null;
            _missionContex = null;
        }

        private void HandlePreparedMission()
        {
            _controller.SetStage(_controller.StageFactory.CreateMissionExecutionStage(_controller));
        }
    }
}