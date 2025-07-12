using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionPreparationStage : IStage
    {
        private readonly IGameStageController _controller;
        private MissionContex _missionContex;

        public MissionPreparationStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            Debug.Log("Mission Preparation Stage: Enter");
            // TODO: подготовка к миссии, расчёт мощи героя и планеты
            _missionContex.OnMissionPrepared += HandlePreparedMission;

            MissionInstance mission = _missionContex.TryCreateMissionInstane();

            mission?.PrepareMission();
            _missionContex.SetPreparedMission(mission);
        }

        public void Tick()
        {
        }

        public void Exit()
        {
            Debug.Log("Mission Preparation Stage: Exit");
            _missionContex.OnMissionPrepared -= HandlePreparedMission;
        }

        private void HandlePreparedMission()
        {
            _controller.SetStage(_controller.StageFactory.CreateMissionExecutionStage(_controller));
        }
    }
}