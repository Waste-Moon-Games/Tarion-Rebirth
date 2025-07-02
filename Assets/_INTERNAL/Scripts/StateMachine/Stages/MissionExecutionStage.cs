using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionExecutionStage : IStage
    {
        private readonly IGameStageController _controller;

        public MissionExecutionStage(IGameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Mission Execution Stage: Enter");
            // TODO: выполнение миссии
            _controller.SetStage(new ResultStage(_controller));
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