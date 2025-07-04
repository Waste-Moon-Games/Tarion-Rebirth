using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionExecutionStage : IStage
    {
        private readonly GameStageController _controller;

        public MissionExecutionStage(GameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Mission Execution Stage: Enter");
            // TODO: выполнение миссии
            _controller.SetStage(new MissionResultStage(_controller));
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