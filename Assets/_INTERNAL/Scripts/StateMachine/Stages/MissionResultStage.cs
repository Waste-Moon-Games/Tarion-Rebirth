using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionResultStage : IStage
    {
        private readonly GameStageController _controller;

        public MissionResultStage(GameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Result Stage: Enter");
            // TODO: результаты миссии
            //_controller.SetStage(new PlanetSelectionStage(_controller));
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