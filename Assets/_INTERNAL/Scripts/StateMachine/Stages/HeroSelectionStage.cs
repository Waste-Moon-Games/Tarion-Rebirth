using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class HeroSelectionStage : IStage
    {
        private readonly IGameStageController _controller;

        public HeroSelectionStage(IGameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Hero Selection Stage: Enter");
            // TODO: здесь будет выбор героев
            _controller.SetStage(new MissionTypeSelectionStage(_controller));
        }

        public void Exit()
        {
            Debug.Log("Hero Selection Stage: Exit");
        }

        public void Tick()
        {
        }
    }
}