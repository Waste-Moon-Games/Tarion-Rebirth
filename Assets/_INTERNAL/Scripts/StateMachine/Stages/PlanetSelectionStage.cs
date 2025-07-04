using Contex.MissionInfo;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using Mono.UI.PlanetListUI;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class PlanetSelectionStage : IStage
    {
        private readonly GameStageController _controller;
        private readonly PlanetListController _planetListController;
        private readonly MissionContex _missionContex;

        private readonly InstanceHolder _instanceHolder;

        public PlanetSelectionStage(GameStageController controller, InstanceHolder instanceHolder, PlanetListController planetListController, MissionContex missionContex)
        {
            _controller = controller;
            _instanceHolder = instanceHolder;
            _planetListController = planetListController;
            _missionContex = missionContex;
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
            Debug.Log("Planet Selectio Stage: Exit");
        }

        public void Tick()
        {
            //Обычно пусто (ждём ввода игрока)
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            Debug.Log($"Planet {selectedPlanet.RuntimeData.PlanetName} selected");
            _missionContex.SetPlanet(selectedPlanet);
            _controller.SetStage(_controller.CreateHeroSelectionStage(_missionContex, _instanceHolder));
        }
    }
}