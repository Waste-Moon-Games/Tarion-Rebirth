using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using StateMachine.Base;
using UI.MissionContexUI;
using UnityEngine;

namespace StateMachine.Stages
{
    public class MissionPreparationStage : IStage
    {
        private readonly IGameStageController _controller;
        private MissionContex _missionContex;
        private MissionPreparationUI _missionPreparationUI;

        public MissionPreparationStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _missionContex = dependencies.MissionContex;
            _missionPreparationUI = dependencies.UIDependencies.MissionPreparationUI;
        }

        public void Enter()
        {
            Debug.Log("Mission Preparation Stage: Enter");
            // TODO: подготовка к миссии, расчёт мощи героя и планеты
            float heroPower = _missionContex.SelectedHero.CalculateHeroPower();

            _missionContex.SelectedPlanet.CalculatePlanetPower();
            float planetPower = _missionContex.SelectedPlanet.RuntimeData.PlanetPower;
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