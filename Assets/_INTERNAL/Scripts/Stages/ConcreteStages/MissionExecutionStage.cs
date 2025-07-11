using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionExecutionStage : IStage
    {
        private readonly IGameStageController _controller;

        private MissionContex _missionContex;

        public MissionExecutionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            Debug.Log("Mission Execution Stage: Enter");
            // TODO: выполнение миссии
        }

        public void Exit()
        {
            Debug.Log("Mission Execution Stage: Exir");
        }

        public void Tick()
        {
        }
    }
}