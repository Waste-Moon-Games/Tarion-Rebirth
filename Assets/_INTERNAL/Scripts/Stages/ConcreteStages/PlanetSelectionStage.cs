using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using Mono.UI.PlanetListUI;
using StateMachine.Base;
using Core.Common;
using UnityEngine;

namespace StateMachine.Stages
{
    public class PlanetSelectionStage : IStage, IDisposable
    {
        private IGameStageController _controller;
        private PlanetListController _planetListController;
        private MissionContex _contex;
        private InstanceHolder _instanceHolder;

        public PlanetSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _planetListController = dependencies.UIDependencies.SelectionPanel.PlanetListController;
            _contex = dependencies.MissionContex;
            _instanceHolder = dependencies.InstanceHolder;
        }

        public void Enter()
        {
            _planetListController.Initialize(_instanceHolder);
            _planetListController.OnPlanetSelected += HandleSelectedPlanet;

            if (!_planetListController.isActiveAndEnabled)
                _planetListController.Show();
        }

        public void Tick() { }

        public void Exit()
        {
            _planetListController.OnPlanetSelected -= HandleSelectedPlanet;
            _planetListController.Hide();
        }

        public void Dispose()
        {
            _contex = null;
            _controller = null;
            _planetListController = null;
            _instanceHolder = null;
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            _contex.SetPlanet(selectedPlanet);
            _controller.SetStage(_controller.StageFactory.CreateHeroSelectionStage(_controller));
        }
    }
}