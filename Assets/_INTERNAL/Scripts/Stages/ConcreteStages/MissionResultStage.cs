using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionResultStage : IStage
    {
        private readonly IGameStageController _controller;

        private MissionContex _missionContex;

        public MissionResultStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            Debug.Log("Result Stage: Enter");
            // TODO: результаты миссии
        }

        public void Exit()
        {
            Debug.Log("Result Stage: Exit");
        }

        public void Tick()
        {
        }
    }
}