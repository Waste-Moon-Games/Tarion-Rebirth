using Contex.MissionInfo;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionPreparationStage : IStage
    {
        private readonly GameStageController _controller;
        private readonly MissionContex _missionContex;

        public MissionPreparationStage(GameStageController controller, MissionContex missionContex)
        {
            _controller = controller;
            _missionContex = missionContex;
        }

        public void Enter()
        {
            Debug.Log("Mission Preparation Stage: Enter");
            // TODO: подготовка к миссии, расчёт мощи героя и планеты
        }

        public void Exit()
        {
            Debug.Log("Mission Preparation Stage: Exir");
        }

        public void Tick()
        {
        }

        private void CalculateMissionDifficulty()
        {

        }

        private void CalculateMissionDuration()
        {

        }
    }
}