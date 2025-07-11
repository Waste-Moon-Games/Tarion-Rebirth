using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using Mono.UI.PlanetListUI;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class PlanetSelectionStage : IStage
    {
        private readonly IGameStageController _controller;
        private PlanetListController _planetListController;
        private MissionContex _contex;
        private InstanceHolder _instanceHolder;

        public PlanetSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _planetListController = dependencies.UIDependencies.PlanetListController;
            _contex = dependencies.MissionContex;
            _instanceHolder = dependencies.InstanceHolder;
        }

        public void Enter()
        {
            Debug.Log("Planet Selection Stage: Enter");
            if (!_planetListController.isActiveAndEnabled)
            {
                _planetListController.Show();
            }

            _planetListController.Initialize(_instanceHolder);
            _planetListController.OnPlanetSelected += HandleSelectedPlanet;
        }

        public void Exit()
        {
            _planetListController.OnPlanetSelected -= HandleSelectedPlanet;
            _planetListController.Hide();
            Debug.Log("Planet Selection Stage: Exit");
        }

        public void Tick()
        {
            //Обычно пусто (ждём ввода игрока)
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            Debug.Log($"Planet {selectedPlanet.RuntimeData.PlanetName} selected");
            _contex.SetPlanet(selectedPlanet);
            _controller.SetStage(_controller.StageFactory.CreateHeroSelectionStage(_controller));
        }
    }
}