using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionTypeSelectionStage : IStage
    {
        private readonly IGameStageController _controller;

        public MissionTypeSelectionStage(IGameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Mission Selection Stage: Enter");
            // TODO: выбор миссии
            _controller.SetStage(new MissionPreparationStage(_controller));
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
