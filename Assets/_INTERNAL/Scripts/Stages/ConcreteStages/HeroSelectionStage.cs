using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance.Main;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class HeroSelectionStage : IStage
    {
        private readonly IGameStageController _controller;
        private MissionContex _missionContex;
        private HeroListController _heroListController;
        private InstanceHolder _instanceHolder;

        public HeroSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _heroListController = dependencies.UIDependencies.HeroListController;
            _instanceHolder = dependencies.InstanceHolder;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            Debug.Log("Hero Selection Stage: Enter");

            if (!_heroListController.gameObject.activeSelf)
            {
                _heroListController.Show();
            }

            _heroListController.Initialize(_instanceHolder);
            _heroListController.OnHeroSelected += HandleSelectedHero;
        }

        public void Exit()
        {
            _heroListController.OnHeroSelected -= HandleSelectedHero;
            Debug.Log("Hero Selection Stage: Exit");
        }

        public void Tick()
        {
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            Debug.Log($"Selected {selectedHero.RuntimeData.Name} hero");

            _missionContex.SetHero(selectedHero);

            _heroListController.Hide();

            _controller.SetStage(_controller.StageFactory.CreateMissionTypeSelectionStage(_controller));
        }
    }
}