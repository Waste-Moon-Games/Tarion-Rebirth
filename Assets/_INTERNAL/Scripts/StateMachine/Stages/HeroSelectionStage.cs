using Contex.MissionInfo;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class HeroSelectionStage : IStage
    {
        private readonly GameStageController _controller;
        private readonly MissionContex _missionContex;
        private readonly HeroListController _heroListController;
        private readonly InstanceHolder _instanceHolder;

        public HeroSelectionStage(GameStageController controller, MissionContex missionContex, HeroListController heroListController, InstanceHolder instanceHolder)
        {
            _controller = controller;
            _missionContex = missionContex;
            _heroListController = heroListController;
            _instanceHolder = instanceHolder;
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

            _controller.SetStage(_controller.CreateMissionTypeSelectionStage(_missionContex, _instanceHolder));
        }
    }
}