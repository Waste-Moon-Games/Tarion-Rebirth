using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionPreparationStage : IStage
    {
        private readonly IGameStageController _controller;

        public MissionPreparationStage(IGameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Mission Preparation Stage: Enter");
            // TODO: подготовка к миссии, расчёт мощи героя и планеты
            _controller.SetStage(new MissionExecutionStage(_controller));
        }

        public void Exit()
        {
            Debug.Log("Mission Preparation Stage: Exir");
        }

        public void Tick()
        {
        }
    }
}