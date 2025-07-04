using Contex.MissionInfo;
using GameEntity.DataInstance.Main;
using Mono.StateMachine;
using StateMachine.Base;
using StateMachine.Stages;
using UnityEngine;

namespace StateMachine
{
    public class GameStageController : IGameStageController
    {
        private IStage _currentStage;
        private MissionContex _missionContex;

        private readonly InstanceHolder _instanceHolder;
        private readonly StateMachineUIDependencies _uiDependencies;

        public GameStageController(InstanceHolder instanceHolder, StateMachineUIDependencies uiDependencies)
        {
            _instanceHolder = instanceHolder;
            _uiDependencies = uiDependencies;
        }

        public void SetStage(IStage newStage)
        {
            if (_currentStage == newStage)
            {
                Debug.Log($"Stage already settled: {newStage}");
                return;
            }

            _currentStage?.Exit();
            _currentStage = newStage;
            _currentStage.Enter();
        }

        public void Start()
        {
            _missionContex = new();
            SetStage(CreatePlanetSelectionStage());
        }

        public void Update()
        {
            _currentStage?.Tick();
        }

        private IStage CreatePlanetSelectionStage()
        {
            return new PlanetSelectionStage(this, _instanceHolder, _uiDependencies.PlanetListController, _missionContex);
        }

        public IStage CreateHeroSelectionStage(MissionContex missionContex, InstanceHolder instanceHolder)
        {
            return new HeroSelectionStage(this, missionContex, _uiDependencies.HeroListController, instanceHolder);
        }

        public IStage CreateMissionTypeSelectionStage(/*MissionTypeUI missionType*/)
        {
            return new MissionTypeSelectionStage(this/*, missionType*/);
        }

        public IStage CreateMissionPreparationStage(MissionContex missionContex)
        {
            return new MissionPreparationStage(this, missionContex);
        }
    }
}