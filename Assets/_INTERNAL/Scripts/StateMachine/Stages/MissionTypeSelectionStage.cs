using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionTypeSelectionStage : IStage
    {
        private readonly GameStageController _controller;

        public MissionTypeSelectionStage(GameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Mission Selection Stage: Enter");
            // TODO: выбор миссии
        }

        public void Exit()
        {
            Debug.Log("Mission Selection Stage: Exit");
        }

        public void Tick()
        {
        }
    }
}